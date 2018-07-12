/* ***********************************************
 * Copyright (c) 2018 luoshasha. All rights reserved";
 * CLR version: 4.0.30319.42000"
 * File name:   HeaderDictionary.cs"
 * Date:        7/4/2018 1:24:28 PM
 * Author :  sand
 * Email  :  luoshasha@foxmail.com
 * Description: 
	
 * History:  created by sand 7/4/2018 1:24:28 PM
 
 * ***********************************************/
using Meteo.Breeze.Http.Features;
using Meteo.Common.Types;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Linq;
using System.Text;

namespace Meteo.Breeze.Server.Simple
{
    class RequestHeaderDictionary : IHeaderDictionary
    {
        private NameValueCollection _headers;
        private long _contentLength;

        public RequestHeaderDictionary(NameValueCollection headers, long contentLength)
        {
            _headers = headers;
            _contentLength = contentLength;
        }

        public virtual StringValues this[string key]
        {
            get => _headers.GetValues(key);
            set => throw new NotImplementedException();
        }

        public virtual long? ContentLength
        {
            get => _contentLength;
            set => throw new NotImplementedException();
        }

        public ICollection<string> Keys => _headers.AllKeys;

        public ICollection<StringValues> Values
        {
            get
            {
                IEnumerable<StringValues> allValues()
                {
                    for (int i = 0, len = Count; i < len; i++)
                    {
                        yield return _headers.GetValues(i);
                    }
                }
                return allValues().ToArray();
            }
        }

        public int Count => _headers.Count;

        public virtual bool IsReadOnly => true;

        public virtual void Add(string key, Meteo.Common.Types.StringValues value)
        {
            throw new NotImplementedException();
        }

        public virtual void Add(KeyValuePair<string, Meteo.Common.Types.StringValues> item)
        {
            throw new NotImplementedException();
        }

        public virtual void Clear()
        {
            throw new NotImplementedException();
        }

        public bool Contains(KeyValuePair<string, Meteo.Common.Types.StringValues> item)
        {
            return _headers.Get(item.Key) == item.Value;
        }

        public bool ContainsKey(string key)
        {
            return _headers.Get(key) != null;
        }

        public void CopyTo(KeyValuePair<string, Meteo.Common.Types.StringValues>[] array, int arrayIndex)
        {
            foreach (var kv in this)
            {
                array[arrayIndex++] = kv;
            }
        }

        public IEnumerator<KeyValuePair<string, Meteo.Common.Types.StringValues>> GetEnumerator()
        {
            return _headers.AllKeys.Select(key => new KeyValuePair<string, StringValues>(key, this[key])).GetEnumerator();
        }

        public virtual bool Remove(string key)
        {
            throw new NotImplementedException();
        }

        public virtual bool Remove(KeyValuePair<string, Meteo.Common.Types.StringValues> item)
        {
            throw new NotImplementedException();
        }

        public bool TryGetValue(string key, out Meteo.Common.Types.StringValues value)
        {
            value = _headers.GetValues(key);
            return value.Count != 0;
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }
    }
}
