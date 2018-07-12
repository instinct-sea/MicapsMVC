/* ***********************************************
 * Copyright (c) 2018 luoshasha. All rights reserved";
 * CLR version: 4.0.30319.42000"
 * File name:   MiddlewareFactory.cs"
 * Date:        7/10/2018 3:38:20 PM
 * Author :  sand
 * Email  :  luoshasha@foxmail.com
 * Description: 
	
 * History:  created by sand 7/10/2018 3:38:20 PM
 
 * ***********************************************/
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Meteo.Breeze.Http.Default
{
    class MiddlewareFactory : IMiddlewareFactory
    {
        private readonly IServiceProvider _services;

        public MiddlewareFactory(IServiceProvider services) => _services = services;

        public IMiddleware Create(Type middlewareType)
        {
            return _services.GetService(middlewareType) as IMiddleware;
        }

        public void Release(IMiddleware middleware)
        {
            if (middleware is IDisposable d)
                d.Dispose();
        }
    }
}
