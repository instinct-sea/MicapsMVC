/* ***********************************************
 * Copyright (c) 2018 luoshasha. All rights reserved";
 * CLR version: 4.0.30319.42000"
 * File name:   RunExtensions.cs"
 * Date:        7/10/2018 11:00:42 AM
 * Author :  sand
 * Email  :  luoshasha@foxmail.com
 * Description: 
	
 * History:  created by sand 7/10/2018 11:00:42 AM
 
 * ***********************************************/
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Meteo.Breeze.Http.Extensions
{
    public static class RunExtensions
    {
        public static void Run(this IApplicationBuilder app, RequestDelegate handler)
        {
            app.Use(_ => handler);
        }
    }
}
