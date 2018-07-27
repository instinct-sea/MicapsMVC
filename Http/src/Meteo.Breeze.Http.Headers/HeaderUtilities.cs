/* ***********************************************
 * Copyright (c) 2018 luoshasha. All rights reserved";
 * CLR version: 4.0.30319.42000"
 * File name:   HeaderUtilities.cs"
 * Date:        7/11/2018 1:17:00 PM
 * Author :  sand
 * Email  :  luoshasha@foxmail.com
 * Description: 
	
 * History:  created by sand 7/11/2018 1:17:00 PM
 
 * ***********************************************/
using Meteo.Common.Types;
using System;
using System.Collections.Generic;
using System.Diagnostics.Contracts;
using System.Globalization;
using System.Linq;
using System.Text;

namespace Meteo.Breeze.Http.Headers
{
    public static class HeaderUtilities
    {
        private static readonly int _int64MaxStringLength = 19;
        private static readonly int _qualityValueMaxCharCount = 10; // Little bit more permissive than RFC7231 5.3.1
        private const string QualityName = "q";
        internal const string BytesUnit = "bytes";

        internal static void SetQuality(IList<NameValueHeaderValue> parameters, double? value)
        {
            Contract.Requires(parameters != null);

            var qualityParameter = NameValueHeaderValue.Find(parameters, QualityName);
            if (value.HasValue)
            {
                // Note that even if we check the value here, we can't prevent a user from adding an invalid quality
                // value using Parameters.Add(). Even if we would prevent the user from adding an invalid value
                // using Parameters.Add() he could always add invalid values using HttpHeaders.AddWithoutValidation().
                // So this check is really for convenience to show users that they're trying to add an invalid
                // value.
                if ((value < 0) || (value > 1))
                {
                    throw new ArgumentOutOfRangeException(nameof(value));
                }

                var qualityString = ((double)value).ToString("0.0##", NumberFormatInfo.InvariantInfo);
                if (qualityParameter != null)
                {
                    qualityParameter.Value = qualityString;
                }
                else
                {
                    parameters.Add(new NameValueHeaderValue(QualityName, qualityString));
                }
            }
            else
            {
                // Remove quality parameter
                if (qualityParameter != null)
                {
                    parameters.Remove(qualityParameter);
                }
            }
        }

        internal static double? GetQuality(IList<NameValueHeaderValue> parameters)
        {
            Contract.Requires(parameters != null);

            var qualityParameter = NameValueHeaderValue.Find(parameters, QualityName);
            if (qualityParameter != null)
            {
                // Note that the RFC requires decimal '.' regardless of the culture. I.e. using ',' as decimal
                // separator is considered invalid (even if the current culture would allow it).
                if (TryParseQualityDouble(qualityParameter.Value, 0, out var qualityValue, out var length))

                {
                    return qualityValue;
                }
            }
            return null;
        }

        internal static int GetNextNonEmptyOrWhitespaceIndex(
            StringSegment input,
            int startIndex,
            bool skipEmptyValues,
            out bool separatorFound)
        {
            Contract.Requires(input != null);
            Contract.Requires(startIndex <= input.Length); // it's OK if index == value.Length.

            separatorFound = false;
            var current = startIndex + HttpRuleParser.GetWhitespaceLength(input, startIndex);

            if ((current == input.Length) || (input[current] != ','))
            {
                return current;
            }

            // If we have a separator, skip the separator and all following whitespaces. If we support
            // empty values, continue until the current character is neither a separator nor a whitespace.
            separatorFound = true;
            current++; // skip delimiter.
            current = current + HttpRuleParser.GetWhitespaceLength(input, current);

            if (skipEmptyValues)
            {
                while ((current < input.Length) && (input[current] == ','))
                {
                    current++; // skip delimiter.
                    current = current + HttpRuleParser.GetWhitespaceLength(input, current);
                }
            }

            return current;
        }

        internal static void CheckValidToken(StringSegment value, string parameterName)
        {
            if (StringSegment.IsNullOrEmpty(value))
            {
                throw new ArgumentException("An empty string is not allowed.", parameterName);
            }

            if (HttpRuleParser.GetTokenLength(value, 0) != value.Length)
            {
                throw new FormatException(string.Format(CultureInfo.InvariantCulture, "Invalid token '{0}.", value));
            }
        }

        internal static bool AreEqualCollections<T>(ICollection<T> x, ICollection<T> y)
        {
            return AreEqualCollections(x, y, null);
        }

        internal static bool AreEqualCollections<T>(ICollection<T> x, ICollection<T> y, IEqualityComparer<T> comparer)
        {
            if (x == null)
            {
                return (y == null) || (y.Count == 0);
            }

            if (y == null)
            {
                return (x.Count == 0);
            }

            if (x.Count != y.Count)
            {
                return false;
            }

            if (x.Count == 0)
            {
                return true;
            }

            // We have two unordered lists. So comparison is an O(n*m) operation which is expensive. Usually
            // headers have 1-2 parameters (if any), so this comparison shouldn't be too expensive.
            var alreadyFound = new bool[x.Count];
            var i = 0;
            foreach (var xItem in x)
            {
                Contract.Assert(xItem != null);

                i = 0;
                var found = false;
                foreach (var yItem in y)
                {
                    if (!alreadyFound[i])
                    {
                        if (((comparer == null) && xItem.Equals(yItem)) ||
                            ((comparer != null) && comparer.Equals(xItem, yItem)))
                        {
                            alreadyFound[i] = true;
                            found = true;
                            break;
                        }
                    }
                    i++;
                }

                if (!found)
                {
                    return false;
                }
            }

            // Since we never re-use a "found" value in 'y', we expecte 'alreadyFound' to have all fields set to 'true'.
            // Otherwise the two collections can't be equal and we should not get here.
            Contract.Assert(Contract.ForAll(alreadyFound, value => { return value; }),
                "Expected all values in 'alreadyFound' to be true since collections are considered equal.");

            return true;
        }

        public static bool TryParseDate(StringSegment input, out DateTimeOffset result)
        {
            return HttpRuleParser.TryStringToDate(input, out result);
        }

        public static string FormatDate(DateTimeOffset dateTime)
        {
            return FormatDate(dateTime, false);
        }

        public static string FormatDate(DateTimeOffset dateTime, bool quoted)
        {
            return dateTime.ToRfc1123String(quoted);
        }

        public static StringSegment RemoveQuotes(StringSegment input)
        {
            if (IsQuoted(input))
            {
                input = input.Subsegment(1, input.Length - 2);
            }
            return input;
        }

        public static bool IsQuoted(StringSegment input)
        {
            return !StringSegment.IsNullOrEmpty(input) && input.Length >= 2 && input[0] == '"' && input[input.Length - 1] == '"';
        }

        internal static void ThrowIfReadOnly(bool isReadOnly)
        {
            if (isReadOnly)
            {
                throw new InvalidOperationException("The object cannot be modified because it is read-only.");
            }
        }

        // Strict and fast RFC7231 5.3.1 Quality value parser (and without memory allocation)
        // See https://tools.ietf.org/html/rfc7231#section-5.3.1
        // Check is made to verify if the value is between 0 and 1 (and it returns False if the check fails).
        internal static bool TryParseQualityDouble(StringSegment input, int startIndex, out double quality, out int length)
        {
            quality = 0;
            length = 0;

            var inputLength = input.Length;
            var current = startIndex;
            var limit = startIndex + _qualityValueMaxCharCount;

            var intPart = 0;
            var decPart = 0;
            var decPow = 1;

            if (current >= inputLength)
            {
                return false;
            }

            var ch = input[current];

            if (ch >= '0' && ch <= '1') // Only values between 0 and 1 are accepted, according to RFC
            {
                intPart = ch - '0';
                current++;
            }
            else
            {
                // The RFC doesn't allow decimal values starting with dot. I.e. value ".123" is invalid. It must be in the
                // form "0.123".
                return false;
            }

            if (current < inputLength)
            {
                ch = input[current];

                if (ch >= '0' && ch <= '9')
                {
                    // The RFC accepts only one digit before the dot
                    return false;
                }

                if (ch == '.')
                {
                    current++;

                    while (current < inputLength)
                    {
                        ch = input[current];
                        if (ch >= '0' && ch <= '9')
                        {
                            if (current >= limit)
                            {
                                return false;
                            }

                            decPart = decPart * 10 + ch - '0';
                            decPow *= 10;
                            current++;
                        }
                        else
                        {
                            break;
                        }
                    }
                }
            }

            if (decPart != 0)
            {
                quality = intPart + decPart / (double)decPow;
            }
            else
            {
                quality = intPart;
            }

            if (quality > 1)
            {
                // reset quality
                quality = 0;
                return false;
            }

            length = current - startIndex;
            return true;
        }

        /// <summary>
        /// Try to convert a <see cref="StringSegment"/> representation of a positive number to its 64-bit signed
        /// integer equivalent. A return value indicates whether the conversion succeeded or failed.
        /// </summary>
        /// <param name="value">
        /// A <see cref="StringSegment"/> containing a number to convert.
        /// </param>
        /// <param name="result">
        /// When this method returns, contains the 64-bit signed integer value equivalent of the number contained
        /// in the string, if the conversion succeeded, or zero if the conversion failed. The conversion fails if
        /// the <see cref="StringSegment"/> is null or String.Empty, is not of the correct format, is negative, or
        /// represents a number greater than Int64.MaxValue. This parameter is passed uninitialized; any value
        /// originally supplied in result will be overwritten.
        /// </param>
        /// <returns><code>true</code> if parsing succeeded; otherwise, <code>false</code>.</returns>
        public static unsafe bool TryParseNonNegativeInt64(StringSegment value, out long result)
        {
            if (string.IsNullOrEmpty(value.Buffer) || value.Length == 0)
            {
                result = 0;
                return false;
            }

            result = 0;
            fixed (char* ptr = value.Buffer)
            {
                var ch = (ushort*)ptr + value.Offset;
                var end = ch + value.Length;

                ushort digit = 0;
                while (ch < end && (digit = (ushort)(*ch - 0x30)) <= 9)
                {
                    // Check for overflow
                    if ((result = result * 10 + digit) < 0)
                    {
                        result = 0;
                        return false;
                    }

                    ch++;
                }

                if (ch != end)
                {
                    result = 0;
                    return false;
                }
                return true;
            }
        }

        /// <summary>
        /// Converts the non-negative 64-bit numeric value to its equivalent string representation.
        /// </summary>
        /// <param name="value">
        /// The number to convert.
        /// </param>
        /// <returns>
        /// The string representation of the value of this instance, consisting of a sequence of digits ranging from 0 to 9 with no leading zeroes.
        /// </returns>
        public unsafe static string FormatNonNegativeInt64(long value)
        {
            if (value < 0)
            {
                throw new ArgumentOutOfRangeException(nameof(value), value, "The value to be formatted must be non-negative.");
            }

            var position = _int64MaxStringLength;
            char* charBuffer = stackalloc char[_int64MaxStringLength];

            do
            {
                // Consider using Math.DivRem() if available
                var quotient = value / 10;
                charBuffer[--position] = (char)(0x30 + (value - quotient * 10)); // 0x30 = '0'
                value = quotient;
            }
            while (value != 0);

            return new string(charBuffer, position, _int64MaxStringLength - position);
        }

    }
}
