/* ***********************************************
 * Copyright (c) 2018 luoshasha. All rights reserved";
 * CLR version: 4.0.30319.42000"
 * File name:   FormCollection.cs"
 * Date:        7/10/2018 3:37:12 PM
 * Author :  sand
 * Email  :  luoshasha@foxmail.com
 * Description: 
	
 * History:  created by sand 7/10/2018 3:37:12 PM
 
 * ***********************************************/
using Meteo.Breeze.Http.Features;
using Meteo.Common.Types;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Meteo.Breeze.Http.Default
{
    class FormCollection : IFormCollection
    {
        private IDictionary<string, StringValues> _store;
        private readonly IFormFileCollection _files;

        public FormCollection(IDictionary<string, StringValues> fields, IFormFileCollection files = null)
        {
            _store = fields;
            _files = files;
        }

        public StringValues this[string key] => _store[key];

        public int Count => _store.Count;

        public ICollection<string> Keys => _store.Keys;

        public IFormFileCollection Files => _files;

        public bool ContainsKey(string key)
        {
            return _store.ContainsKey(key);
        }

        public IEnumerator<KeyValuePair<string, Meteo.Common.Types.StringValues>> GetEnumerator()
        {
            throw new NotImplementedException();
        }

        public bool TryGetValue(string key, out Meteo.Common.Types.StringValues value)
        {
            throw new NotImplementedException();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            throw new NotImplementedException();
        }
    }
}
