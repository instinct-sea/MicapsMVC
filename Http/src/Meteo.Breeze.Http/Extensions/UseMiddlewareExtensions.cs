/* ***********************************************
 * Copyright (c) 2018 luoshasha. All rights reserved";
 * CLR version: 4.0.30319.42000"
 * File name:   UseMiddlewareExtensions.cs"
 * Date:        7/10/2018 11:20:30 AM
 * Author :  sand
 * Email  :  luoshasha@foxmail.com
 * Description: 
	
 * History:  created by sand 7/10/2018 11:20:30 AM
 
 * ***********************************************/
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Meteo.Breeze.Http.Extensions
{
    public static class UseMiddlewareExtensions
    {
        public static IApplicationBuilder UseMiddleware<TMiddleware>(this IApplicationBuilder app) where TMiddleware : IMiddleware
        {
            return app.Use(next=>
            {
                IMiddlewareFactory middlewareFactory = (IMiddlewareFactory)app.ApplicationServices.GetService(typeof(IMiddlewareFactory));
                var middleware = middlewareFactory.Create(typeof(TMiddleware));
                if (middleware == null)
                {
                    // The factory returned null, it's a broken implementation
                    throw new InvalidOperationException(string.Format("Create middleware failed, middleware factory:{0},{1}", 
                        middlewareFactory.GetType(), typeof(TMiddleware)));
                }

                return async context =>
                {
                    try
                    {
                        await middleware.InvokeAsync(context, next);
                    }
                    finally
                    {
                        middlewareFactory.Release(middleware);
                    }
                };
            });
        }
    }
}
