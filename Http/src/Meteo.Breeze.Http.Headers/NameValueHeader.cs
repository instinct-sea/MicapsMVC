/* ***********************************************
 * Copyright (c) 2018 luoshasha. All rights reserved";
 * CLR version: 4.0.30319.42000"
 * File name:   NameValueHeader.cs"
 * Date:        7/11/2018 1:13:52 PM
 * Author :  sand
 * Email  :  luoshasha@foxmail.com
 * Description: 
	
 * History:  created by sand 7/11/2018 1:13:52 PM
 
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
    public class NameValueHeader
    {
        private StringSegment _name;
        private StringSegment _value;
        private bool _isReadonly;

        public NameValueHeader(StringSegment name)
            : this(name, null)
        {

        }

        public NameValueHeader(StringSegment name, StringSegment value)
        {
            _name = name;
            _value = value;
        }

        public bool IsReadOnly => _isReadonly;

        public StringSegment Name
        {
            get { return _name; }
        }

        public StringSegment Value
        {
            get { return _value; }
            set
            {
                HeaderUtilities.ThrowIfReadOnly(IsReadOnly);
                CheckValueFormat(value);
                _value = value;
            }
        }

        public override string ToString()
        {
            if (!StringSegment.IsNullOrEmpty(_value))
            {
                return _name + "=" + _value;
            }
            return _name.ToString();
        }

        public override int GetHashCode()
        {
            Contract.Assert(_name != null);

            var nameHashCode = StringSegmentComparer.OrdinalIgnoreCase.GetHashCode(_name);

            if (!StringSegment.IsNullOrEmpty(_value))
            {
                // If we have a quoted-string, then just use the hash code. If we have a token, convert to lowercase
                // and retrieve the hash code.
                if (_value[0] == '"')
                {
                    return nameHashCode ^ _value.GetHashCode();
                }

                return nameHashCode ^ StringSegmentComparer.OrdinalIgnoreCase.GetHashCode(_value);
            }

            return nameHashCode;
        }

        internal static int GetHashCode(IList<NameValueHeader> values)
        {
            if ((values == null) || (values.Count == 0))
            {
                return 0;
            }

            var result = 0;
            for (var i = 0; i < values.Count; i++)
            {
                result = result ^ values[i].GetHashCode();
            }
            return result;
        }

        internal static int GetValueLength(StringSegment input, int startIndex)
        {
            Contract.Requires(input != null);

            if (startIndex >= input.Length)
            {
                return 0;
            }

            var valueLength = HttpRuleParser.GetTokenLength(input, startIndex);

            if (valueLength == 0)
            {
                // A value can either be a token or a quoted string. Check if it is a quoted string.
                if (HttpRuleParser.GetQuotedStringLength(input, startIndex, out valueLength) != HttpParseResult.Parsed)
                {
                    // We have an invalid value. Reset the name and return.
                    return 0;
                }
            }
            return valueLength;
        }

        private static void CheckValueFormat(StringSegment value)
        {
            // Either value is null/empty or a valid token/quoted string
            if (!(StringSegment.IsNullOrEmpty(value) || (GetValueLength(value, 0) == value.Length)))
            {
                throw new FormatException(string.Format(CultureInfo.InvariantCulture, "The header value is invalid: '{0}'", value));
            }
        }
    }
}
