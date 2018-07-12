/* ***********************************************
 * Copyright (c) 2018 luoshasha. All rights reserved";
 * CLR version: 4.0.30319.42000"
 * File name:   IFeatureCollection.cs"
 * Date:        6/26/2018 1:07:31 PM
 * Author :  sand
 * Email  :  luoshasha@foxmail.com
 * Description: 
	
 * History:  created by sand 6/26/2018 1:07:31 PM
 
 * ***********************************************/
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Meteo.Breeze.Http.Features
{
    public interface IFeatureCollection : IEnumerable<KeyValuePair<Type, object>>
    {
        /// <summary>
        /// Indicates if the collection can be modified.
        /// </summary>
        bool IsReadOnly { get; }

        /// <summary>
        /// Sets the given feature in the collection.
        /// </summary>
        /// <typeparam name="TFeature"></typeparam>
        /// <param name="feature"></param>
        void Set<TFeature>(TFeature feature) where TFeature : class;

        /// <summary>
        /// Gets feature.
        /// </summary>
        /// <typeparam name="TFeature"></typeparam>
        /// <returns></returns>
        TFeature Get<TFeature>() where TFeature : class;

        /// <summary>
        /// Gets or sets a given feature. Setting a null value removes the feature.
        /// </summary>
        /// <param name="key"></param>
        /// <returns>The requested feature, or null if it is not present.</returns>
        object this[Type key] { get; set; }
    }
}
