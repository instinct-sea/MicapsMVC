/* ***********************************************
 * Copyright (c) 2018 luoshasha. All rights reserved";
 * CLR version: 4.0.30319.42000"
 * File name:   ListenOptionsHttpExtensions.cs"
 * Date:        7/5/2018 1:57:19 PM
 * Author :  sand
 * Email  :  luoshasha@foxmail.com
 * Description: 
	
 * History:  created by sand 7/5/2018 1:57:19 PM
 
 * ***********************************************/
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Meteo.Breeze.Server.Simple
{
    static class ListenOptionsHttpExtensions
    {
        public static IConnectionBuilder UseHttpServer<TContext>(this IConnectionBuilder builder, 
            ServerContext serverContext, 
            IHttpApplication<TContext> application,
            HttpVersion protocolVersion)
        {
            var middleware = new HttpMiddleware<TContext>(serverContext, application, protocolVersion);
            return builder.Use(next =>
            {
                return middleware.OnConnectionAsync;
            });
        }
    }
}
