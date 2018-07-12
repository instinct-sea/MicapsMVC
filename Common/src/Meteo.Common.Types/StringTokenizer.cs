﻿/* ***********************************************
 * Copyright (c) 2018 luoshasha. All rights reserved";
 * CLR version: 4.0.30319.42000"
 * File name:   StringSegment.cs"
 * Date:        7/2/2018 2:48:24 PM
 * Author :  sand
 * Email  :  luoshasha@foxmail.com
 * Description: 
	
 * History:  created by sand 7/2/2018 2:48:24 PM
 
 * ***********************************************/
using System.Collections;
using System.Collections.Generic;

namespace Meteo.Common.Types
{
    /// <summary>
    /// Tokenizes a <c>string</c> into <see cref="StringSegment"/>s.
    /// </summary>
    public readonly struct StringTokenizer : IEnumerable<StringSegment>
    {
        private readonly StringSegment _value;
        private readonly char[] _separators;

        /// <summary>
        /// Initializes a new instance of <see cref="StringTokenizer"/>.
        /// </summary>
        /// <param name="value">The <c>string</c> to tokenize.</param>
        /// <param name="separators">The characters to tokenize by.</param>
        public StringTokenizer(string value, char[] separators)
        {
            if (value == null)
            {
                ThrowHelper.ThrowArgumentNullException(ExceptionArgument.value);
            }

            if (separators == null)
            {
                ThrowHelper.ThrowArgumentNullException(ExceptionArgument.separators);
            }

            _value = value;
            _separators = separators;
        }

        /// <summary>
        /// Initializes a new instance of <see cref="StringTokenizer"/>.
        /// </summary>
        /// <param name="value">The <c>StringSegment</c> to tokenize.</param>
        /// <param name="separators">The characters to tokenize by.</param>
        public StringTokenizer(StringSegment value, char[] separators)
        {
            if (!value.HasValue)
            {
                ThrowHelper.ThrowArgumentNullException(ExceptionArgument.value);
            }

            if (separators == null)
            {
                ThrowHelper.ThrowArgumentNullException(ExceptionArgument.separators);
            }

            _value = value;
            _separators = separators;
        }

        public Enumerator GetEnumerator() => new Enumerator(in _value, _separators);

        IEnumerator<StringSegment> IEnumerable<StringSegment>.GetEnumerator() => GetEnumerator();

        IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();

        public struct Enumerator : IEnumerator<StringSegment>
        {
            private readonly StringSegment _value;
            private readonly char[] _separators;
            private int _index;

            internal Enumerator(in StringSegment value, char[] separators)
            {
                _value = value;
                _separators = separators;
                Current = default;
                _index = 0;
            }

            public Enumerator(ref StringTokenizer tokenizer)
            {
                _value = tokenizer._value;
                _separators = tokenizer._separators;
                Current = default(StringSegment);
                _index = 0;
            }

            public StringSegment Current { get; private set; }

            object IEnumerator.Current => Current;

            public void Dispose()
            {
            }

            public bool MoveNext()
            {
                if (!_value.HasValue || _index > _value.Length)
                {
                    Current = default(StringSegment);
                    return false;
                }

                var next = _value.IndexOfAny(_separators, _index);
                if (next == -1)
                {
                    // No separator found. Consume the remainder of the string.
                    next = _value.Length;
                }

                Current = _value.Subsegment(_index, next - _index);
                _index = next + 1;

                return true;
            }

            public void Reset()
            {
                Current = default(StringSegment);
                _index = 0;
            }
        }
    }
}