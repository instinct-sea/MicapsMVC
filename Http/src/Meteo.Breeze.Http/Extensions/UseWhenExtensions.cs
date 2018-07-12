/* ***********************************************
 * Copyright (c) 2018 luoshasha. All rights reserved";
 * CLR version: 4.0.30319.42000"
 * File name:   UseExtensions.cs"
 * Date:        7/10/2018 11:01:01 AM
 * Author :  sand
 * Email  :  luoshasha@foxmail.com
 * Description: 
	
 * History:  created by sand 7/10/2018 11:01:01 AM
 
 * ***********************************************/
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Meteo.Breeze.Http.Extensions
{
    public static class UseWhenExtensions
    {
        public static IApplicationBuilder UseWhen(this IApplicationBuilder app, Predicate<HttpContext> predicate, Action<IApplicationBuilder> configuration)
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

            // Create and configure the branch builder right away; otherwise,
            // we would end up running our branch after all the components
            // that were subsequently added to the main builder.
            var branchBuilder = app.New();
            configuration(branchBuilder);

            return app.Use(main =>
            {
                // This is called only when the main application builder 
                // is built, not per request.
                branchBuilder.Run(main);
                var branch = branchBuilder.Build();

                return context =>
                {
                    if (predicate(context))
                    {
                        return branch(context);
                    }
                    else
                    {
                        return main(context);
                    }
                };
            });
        }
    }
}
