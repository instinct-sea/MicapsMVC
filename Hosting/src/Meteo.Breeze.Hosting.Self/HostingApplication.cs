/* ***********************************************
 * Copyright (c) 2018 luoshasha. All rights reserved";
 * CLR version: 4.0.30319.42000"
 * File name:   HostingApplication.cs"
 * Date:        7/10/2018 10:22:39 AM
 * Author :  sand
 * Email  :  luoshasha@foxmail.com
 * Description: 
	
 * History:  created by sand 7/10/2018 10:22:39 AM
 
 * ***********************************************/
using Meteo.Breeze.Http;
using Meteo.Breeze.Http.Features;
using Meteo.Breeze.Server;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Meteo.Breeze.Hosting.Self
{
    class HostingApplication : IHttpApplication<HttpContext>
    {
        private readonly IHttpContextFactory _httpContextFactory;
        private readonly RequestDelegate _application;

        public HostingApplication(RequestDelegate application, 
            IHttpContextFactory httpContextFactory)
        {
            _application = application;
            _httpContextFactory = httpContextFactory;
        }

        public HttpContext CreateContext(IFeatureCollection contextFeatures)
        {
            var httpContext = _httpContextFactory.Create(contextFeatures);
            return httpContext;
        }

        public void DisposeContext(HttpContext context, Exception exception)
        {
            _httpContextFactory.Dispose(context);
        }

        public Task ProcessRequestAsync(HttpContext context)
        {
            return _application(context);
        }
    }
}
