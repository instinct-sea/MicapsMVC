/* ***********************************************
 * Copyright (c) 2018 luoshasha. All rights reserved";
 * CLR version: 4.0.30319.42000"
 * File name:   ServerFactory.cs"
 * Date:        7/5/2018 6:03:29 PM
 * Author :  sand
 * Email  :  luoshasha@foxmail.com
 * Description: 
	
 * History:  created by sand 7/5/2018 6:03:29 PM
 
 * ***********************************************/
using Meteo.Breeze.Hosting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using Meteo.Extensions.DependencyInjection;

namespace Meteo.Breeze.Server.Simple
{
    //For test use.
    public static class SimpleServerExtensions
    {
        public static IWebHostBuilder UseSimpleHttpServer(this IWebHostBuilder builder, IPAddress address, int port)
        {
            return builder.ConfigureServices(services =>
            {
                var serverOptions = new ServerOptions();
                serverOptions.Listen(new IPEndPoint(address, port));
                var transportFactory = new HttpListenerTransportFactory();
                services.AddSingleton<IServer>(new Server(serverOptions, transportFactory));
            });
        }
    }
}
