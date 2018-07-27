/* ***********************************************
 * Copyright (c) 2018 luoshasha. All rights reserved";
 * CLR version: 4.0.30319.42000"
 * File name:   IWebHost.cs"
 * Date:        7/2/2018 4:20:23 PM
 * Author :  sand
 * Email  :  luoshasha@foxmail.com
 * Description: 
	
 * History:  created by sand 7/2/2018 4:20:23 PM
 
 * ***********************************************/
using Meteo.Breeze.Http.Features;
using Meteo.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Meteo.Breeze.Hosting
{
    public interface IWebHost
    {
        /// <summary>
        /// Gets server's features.
        /// </summary>
        IFeatureCollection ServerFeatures { get; }

        /// <summary>
        /// Gets services for the host.
        /// We use IServiceCollection instead of IServiceProvider here because IWebHost is used as a Service in MICAPS system. 
        /// intead of run itself.
        /// </summary>
        IServiceCollection Services { get; }

        /// <summary>
        /// Starts webhost with given services.
        /// </summary>
        Task StartAsync(IServiceProvider applicationServices, CancellationToken cancellationToken = default(CancellationToken));

        /// <summary>
        /// Attempt to gracefully stop the host.
        /// </summary>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        Task StopAsync(CancellationToken cancellationToken = default(CancellationToken));
    }
}
