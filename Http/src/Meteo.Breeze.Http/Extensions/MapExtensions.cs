/* ***********************************************
 * Copyright (c) 2018 luoshasha. All rights reserved";
 * CLR version: 4.0.30319.42000"
 * File name:   MapExtensions.cs"
 * Date:        7/10/2018 11:00:22 AM
 * Author :  sand
 * Email  :  luoshasha@foxmail.com
 * Description: 
	
 * History:  created by sand 7/10/2018 11:00:22 AM
 
 * ***********************************************/
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Meteo.Breeze.Http.Extensions
{
    public static class MapExtensions
    {
        public static IApplicationBuilder Map(this IApplicationBuilder app, PathString pathMatch, Action<IApplicationBuilder> configuration)
        {
            if (app == null)
            {
                throw new ArgumentNullException(nameof(app));
            }

            if (configuration == null)
            {
                throw new ArgumentNullException(nameof(configuration));
            }

            if (pathMatch.HasValue && pathMatch.Value.EndsWith("/", StringComparison.Ordinal))
            {
                throw new ArgumentException("The path must not end with a '/'", nameof(pathMatch));
            }

            var branchApp = app.New();
            configuration(branchApp);
            var branch = app.Build();

            return app.Use(next =>
            {
                return async context =>
                {
                    if (context.Request.Path.StartsWithSegments(pathMatch, out PathString matchedPath, out PathString remainingPath))
                    {
                        // Update the path
                        var path = context.Request.Path;
                        var pathBase = context.Request.PathBase;
                        context.Request.PathBase = pathBase.Add(matchedPath);
                        context.Request.Path = remainingPath;

                        try
                        {
                            await branch(context);
                        }
                        finally
                        {
                            context.Request.PathBase = pathBase;
                            context.Request.Path = path;
                        }
                    }
                    else
                    {
                        await next(context);
                    }
                };
            });
        }
    }
}
