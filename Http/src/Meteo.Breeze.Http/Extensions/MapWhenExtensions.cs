/* ***********************************************
 * Copyright (c) 2018 luoshasha. All rights reserved";
 * CLR version: 4.0.30319.42000"
 * File name:   MapWhenExtensions.cs"
 * Date:        7/10/2018 11:00:33 AM
 * Author :  sand
 * Email  :  luoshasha@foxmail.com
 * Description: 
	
 * History:  created by sand 7/10/2018 11:00:33 AM
 
 * ***********************************************/
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Meteo.Breeze.Http.Extensions
{
    public static class MapWhenExtensions
    {
        public static IApplicationBuilder MapWhen(this IApplicationBuilder app, Predicate<HttpContext> predicate, Action<IApplicationBuilder> configuration)
        {
            if (app == null)
            {
                throw new ArgumentNullException(nameof(app));
            }

            if (predicate == null)
            {
                throw new ArgumentNullException(nameof(predicate));
            }

            if (configuration == null)
            {
                throw new ArgumentNullException(nameof(configuration));
            }

            var branchApp = app.New();
            configuration(branchApp);
            var branch = app.Build();

            return app.Use(next =>
            {
                return context =>
                {
                    if (predicate(context))
                    {
                        return branch(context);
                    }
                    else
                    {
                        return next(context);
                    }
                };
            });
        }
    }
}
