/* ***********************************************
 * Copyright (c) 2018 luoshasha. All rights reserved";
 * CLR version: 4.0.30319.42000"
 * File name:   WebHostBuilderExtensions.cs"
 * Date:        7/10/2018 10:36:09 AM
 * Author :  sand
 * Email  :  luoshasha@foxmail.com
 * Description: 
	
 * History:  created by sand 7/10/2018 10:36:09 AM
 
 * ***********************************************/
using Meteo.Breeze.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Meteo.Extensions.DependencyInjection;

namespace Meteo.Breeze.Hosting.Self
{
    public static class WebHostBuilderExtensions
    {
        public static IWebHostBuilder Configure(this IWebHostBuilder hostBuilder, Action<IApplicationBuilder> configureApp)
        {
            if (configureApp == null)
            {
                throw new ArgumentNullException(nameof(configureApp));
            }
            return hostBuilder
                .ConfigureServices(services =>
                {
                    services.AddTransient<IStartup>(sp =>
                    {
                        return new DelegateStartup(configureApp);
                    });
                });
        }
    }
}
