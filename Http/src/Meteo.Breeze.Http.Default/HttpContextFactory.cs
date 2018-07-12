/* ***********************************************
 * Copyright (c) 2018 luoshasha. All rights reserved";
 * CLR version: 4.0.30319.42000"
 * File name:   HttpContextFactory.cs"
 * Date:        7/10/2018 3:37:41 PM
 * Author :  sand
 * Email  :  luoshasha@foxmail.com
 * Description: 
	
 * History:  created by sand 7/10/2018 3:37:41 PM
 
 * ***********************************************/
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Meteo.Breeze.Http.Features;

namespace Meteo.Breeze.Http.Default
{
    class HttpContextFactory : IHttpContextFactory
    {
        private IHttpContextAccessor _httpContextAccessor;

        public HttpContextFactory(IHttpContextAccessor httpContextAccessor)
        {
            _httpContextAccessor = httpContextAccessor;
        }

        public HttpContext Create(IFeatureCollection featureCollection)
        {
            var context = new DefaultHttpContext(featureCollection);
            if (_httpContextAccessor != null)
                _httpContextAccessor.HttpContext = context;
            return context;
        }

        public void Dispose(HttpContext httpContext)
        {
            if (_httpContextAccessor != null)
                _httpContextAccessor.HttpContext = null;
        }
    }
}
