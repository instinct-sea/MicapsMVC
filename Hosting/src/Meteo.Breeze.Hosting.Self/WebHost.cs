/* ***********************************************
 * Copyright (c) 2018 luoshasha. All rights reserved";
 * CLR version: 4.0.30319.42000"
 * File name:   WebHost.cs"
 * Date:        7/3/2018 3:45:10 PM
 * Author :  sand
 * Email  :  luoshasha@foxmail.com
 * Description: 
	
 * History:  created by sand 7/3/2018 3:45:10 PM
 
 * ***********************************************/
using Meteo.Breeze.Http;
using Meteo.Breeze.Http.Features;
using Meteo.Breeze.Server;
using Meteo.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Meteo.Breeze.Hosting.Self
{
    class WebHost : IWebHost
    {
        public WebHost(IServiceCollection services)
        {
            Services = services;
        }

        private IServer Server
        {
            get;
            set;
        }

        public IServiceCollection Services
        {
            get;
        }

        public IFeatureCollection ServerFeatures => Server?.Features;

        public Task StartAsync(IServiceProvider services, CancellationToken cancellationToken = default(CancellationToken))
        {
            Server = services.GetService<IServer>();

            var app = BuildApplication(services);
            var application = new HostingApplication(app, services.GetService<IHttpContextFactory>());

            return Server.StartAsync(application, cancellationToken);
        }

        public async Task StopAsync(CancellationToken cancellationToken = default(CancellationToken))
        {
            if (Server != null)
            {
                await Server.StopAsync(cancellationToken).ConfigureAwait(false);
            }
        }

        private RequestDelegate BuildApplication(IServiceProvider services)
        {
            var applicationBuilder = new ApplicationBuilder(services)
            {
                ServerFeatures = ServerFeatures
            };

            try
            {
                var startup = services.GetService<IStartup>();
                startup?.Configure(applicationBuilder);
            }
            catch
            {
               
            }
            return applicationBuilder.Build();
        }
    }
}
