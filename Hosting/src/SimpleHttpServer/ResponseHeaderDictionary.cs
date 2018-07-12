/* ***********************************************
 * Copyright (c) 2018 luoshasha. All rights reserved";
 * CLR version: 4.0.30319.42000"
 * File name:   ReponseHeaderDictionary.cs"
 * Date:        7/4/2018 1:51:07 PM
 * Author :  sand
 * Email  :  luoshasha@foxmail.com
 * Description: 
	
 * History:  created by sand 7/4/2018 1:51:07 PM
 
 * ***********************************************/
using Meteo.Breeze.Http.Features;
using Meteo.Common.Types;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;

namespace Meteo.Breeze.Server.Simple
{
    class ResponseHeaderDictionary : RequestHeaderDictionary
    {
        private readonly HttpListenerResponse _response;

        public ResponseHeaderDictionary(HttpListenerResponse response)
            : base(response.Headers, 0)
        {
            _response = response;
        }

        public override StringValues this[string key]
        {
            get => base[key];
            set => _response.Headers.Set(key, value);
        }

        public override void Add(KeyValuePair<string, StringValues> item)
        {
            _response.AddHeader(item.Key, item.Value);
        }

        public override void Add(string key, StringValues value)
        {
            _response.AddHeader(key, value);
        }

        public override bool Remove(string key)
        {
            _response.Headers.Remove(key);
            return true;
        }

        public override bool Remove(KeyValuePair<string, StringValues> item)
        {
            if (Contains(item))
            {
                _response.Headers.Remove(item.Key);
                return true;
            }
            return false;
        }

        public override long? ContentLength
        {
            get => _response.ContentLength64;
            set => _response.ContentLength64 = value.Value;
        }

        public override void Clear()
        {
            _response.Headers.Clear();
        }

        public override bool IsReadOnly => false;
    }
}
