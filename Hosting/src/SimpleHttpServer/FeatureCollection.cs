/* ***********************************************
 * Copyright (c) 2018 luoshasha. All rights reserved";
 * CLR version: 4.0.30319.42000"
 * File name:   FeatureCollection.cs"
 * Date:        7/3/2018 3:55:59 PM
 * Author :  sand
 * Email  :  luoshasha@foxmail.com
 * Description: 
	
 * History:  created by sand 7/3/2018 3:55:59 PM
 
 * ***********************************************/
using Meteo.Breeze.Http.Features;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Meteo.Breeze.Server.Simple
{
    class FeatureCollection : IFeatureCollection
    {
        private IDictionary<Type, object> _features = new Dictionary<Type, object>();

        public object this[Type key]
        {
            get => _features[key];
            set => _features[key] = value;
        }

        public bool IsReadOnly => true;

        public TFeature Get<TFeature>() where TFeature : class
        {
            if (_features.TryGetValue(typeof(TFeature), out object feature))
                return feature as TFeature;
            return null;
        }

        public IEnumerator<KeyValuePair<Type, object>> GetEnumerator()
        {
            return _features.GetEnumerator();
        }

        public object GetService(Type serviceType)
        {
            _features.TryGetValue(serviceType, out object f);
            return f;
        }

        public void Set<TFeature>(TFeature feature) where TFeature : class
        {
            _features[typeof(TFeature)] = feature;
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return _features.GetEnumerator();
        }
    }
}
