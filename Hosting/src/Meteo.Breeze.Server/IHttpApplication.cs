/* ***********************************************
 * Copyright (c) 2018 luoshasha. All rights reserved";
 * CLR version: 4.0.30319.42000"
 * File name:   IHttpApplication.cs"
 * Date:        6/26/2018 1:04:13 PM
 * Author :  sand
 * Email  :  luoshasha@foxmail.com
 * Description: 
	
 * History:  created by sand 6/26/2018 1:04:13 PM
 
 * ***********************************************/
using Meteo.Breeze.Http.Features;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Meteo.Breeze.Server
{
    public interface IHttpApplication<TContext>
    {
         /// <summary>
        /// Create a TContext given a collection of HTTP features.
        /// </summary>
        /// <param name="contextFeatures">A collection of HTTP features to be used for creating the TContext.</param>
        /// <returns>The created TContext.</returns>
        TContext CreateContext(IFeatureCollection contextFeatures);

        /// <summary>
        /// Asynchronously processes an TContext.
        /// </summary>
        /// <param name="context">The TContext that the operation will process.</param>
        Task ProcessRequestAsync(TContext context);

        /// <summary>
        /// Dispose a given TContext.
        /// </summary>
        /// <param name="context">The TContext to be disposed.</param>
        /// <param name="exception">The Exception thrown when processing did not complete successfully, otherwise null.</param>
        void DisposeContext(TContext context, Exception exception);
    }
}
