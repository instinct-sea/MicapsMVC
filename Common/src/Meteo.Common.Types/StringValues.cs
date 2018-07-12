using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Meteo.Common.Types
{
    public readonly struct StringValues : IReadOnlyList<string>, IEquatable<StringValues>, IEquatable<string>, IEquatable<string[]>
    {
        private readonly string[] _values;

        public static readonly StringValues Null = new StringValues((string[])null);
        public static readonly StringValues Empty = new StringValues(new string[0]);

        public StringValues(string value)
        {
            _values = new string[] { value };
        }

        public StringValues(string[] values) => _values = values;

        public static implicit operator StringValues(string value)
        {
            return new StringValues(value);
        }

        public static implicit operator StringValues(string[] values)
        {
            return new StringValues(values);
        }

        public static implicit operator string(StringValues values)
        {
            return values.GetStringValue();
        }

        public static implicit operator string[] (StringValues value)
        {
            return value.GetArrayValue();
        }

        private string GetStringValue()
        {
            switch (_values.Length)
            {
                case 0: return null;
                case 1: return _values[0];
                default: return string.Join(",", _values);
            }
        }

        private string[] GetArrayValue()
        {
            return _values;
        }

        public string this[int index] => _values?[index];

        public int Count => _values?.Length ?? 0;

        public static bool Equals(StringValues left, StringValues right)
        {
            var count = left.Count;

            if (count != right.Count)
            {
                return false;
            }

            for (var i = 0; i < count; i++)
            {
                if (left[i] != right[i])
                {
                    return false;
                }
            }

            return true;
        }

        public bool Equals(StringValues other)
        {
            return Equals(this, other);
        }

        public bool Equals(string other)
        {
            return Equals(this, new StringValues(other));
        }

        public bool Equals(string[] other)
        {
            return Equals(this, new StringValues(other));
        }

        public static bool operator ==(StringValues left, string right)
        {
            return Equals(left, new StringValues(right));
        }

        public static bool operator !=(StringValues left, string right)
        {
            return !Equals(left, new StringValues(right));
        }

        public static bool operator ==(string left, StringValues right)
        {
            return Equals(new StringValues(left), right);
        }

        public static bool operator !=(string left, StringValues right)
        {
            return !Equals(new StringValues(left), right);
        }

        public static bool operator ==(StringValues left, string[] right)
        {
            return Equals(left, new StringValues(right));
        }

        public static bool operator !=(StringValues left, string[] right)
        {
            return !Equals(left, new StringValues(right));
        }

        public static bool operator ==(string[] left, StringValues right)
        {
            return Equals(new StringValues(left), right);
        }

        public static bool operator !=(string[] left, StringValues right)
        {
            return !Equals(new StringValues(left), right);
        }

        public static bool operator ==(StringValues left, object right)
        {
            return left.Equals(right);
        }

        public static bool operator !=(StringValues left, object right)
        {
            return !left.Equals(right);
        }
        public static bool operator ==(object left, StringValues right)
        {
            return right.Equals(left);
        }

        public static bool operator !=(object left, StringValues right)
        {
            return !right.Equals(left);
        }

        public static bool IsNullOrEmpty(StringValues value)
        {
            switch (value._values.Length)
            {
                case 0: return true;
                case 1: return string.IsNullOrEmpty(value._values[0]);
                default: return false;
            }
        }

        public static StringValues Concat(StringValues values1, StringValues values2)
        {
            var count1 = values1.Count;
            var count2 = values2.Count;

            if (count1 == 0)
            {
                return values2;
            }

            if (count2 == 0)
            {
                return values1;
            }

            var combined = new string[count1 + count2];
            values1.CopyTo(combined, 0);
            values2.CopyTo(combined, count1);
            return new StringValues(combined);
        }

        public static StringValues Concat(in StringValues values, string value)
        {
            if (value == null)
            {
                return values;
            }

            var count = values.Count;
            if (count == 0)
            {
                return new StringValues(value);
            }

            var combined = new string[count + 1];
            values.CopyTo(combined, 0);
            combined[count] = value;
            return new StringValues(combined);
        }

        public static StringValues Concat(string value, in StringValues values)
        {
            if (value == null)
            {
                return values;
            }

            var count = values.Count;
            if (count == 0)
            {
                return new StringValues(value);
            }

            var combined = new string[count + 1];
            combined[0] = value;
            values.CopyTo(combined, 1);
            return new StringValues(combined);
        }

        private void CopyTo(string[] array, int arrayIndex)
        {
            if (_values != null)
            {
                Array.Copy(_values, 0, array, arrayIndex, _values.Length);
                return;
            }
        }

        public override bool Equals(object obj)
        {
            if (obj == null)
            {
                return  _values == null;
            }

            if (obj is string)
            {
                return Equals(this, (string)obj);
            }

            if (obj is string[])
            {
                return Equals(this, (string[])obj);
            }

            if (obj is StringValues)
            {
                return Equals(this, (StringValues)obj);
            }

            return false;
        }

        public override int GetHashCode()
        {
            if (_values == null)
            {
                int hashCombind(int seed, int h2)
                {
                    // this is where the magic happens
                    return (((seed << 5) + seed) ^ h2);
                }

                var hcc = 0;
                for (var i = 0; i < _values.Length; i++)
                {
                    hcc = hashCombind(hcc, _values[i].GetHashCode());
                }
                return hcc;
            }
            return 0;
        }

        public IEnumerator<string> GetEnumerator() => new Enumerator(_values);

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }

        struct Enumerator : IEnumerator<string>
        {
            private readonly string[] _values;
            private string _current;
            private int _index;

            public Enumerator(string[] values)
            {
                _values = values;
                _current = null;
                _index = 0;
            }

            public bool MoveNext()
            {
                if (_index < 0)
                {
                    return false;
                }

                if (_values != null)
                {
                    if (_index < _values.Length)
                    {
                        _current = _values[_index];
                        _index++;
                        return true;
                    }

                    _index = -1;
                    return false;
                }

                _index = -1; // sentinel value
                return _current != null;
            }

            public string Current => _current;

            object IEnumerator.Current => _current;

            void IEnumerator.Reset()
            {
                throw new NotSupportedException();
            }

            public void Dispose()
            {
            }
        }
    }
}

