/* ***********************************************
 * Copyright (c) 2018 luoshasha. All rights reserved";
 * CLR version: 4.0.30319.42000"
 * File name:   Constants.cs"
 * Date:        7/10/2018 5:06:55 PM
 * Author :  sand
 * Email  :  luoshasha@foxmail.com
 * Description: 
	
 * History:  created by sand 7/10/2018 5:06:55 PM
 
 * ***********************************************/
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Meteo.Breeze.Http.Default.Internal
{
    internal static class Constants
    {
        internal const string Http = "http";
        internal const string Https = "https";
        internal const string UnixPipeHostPrefix = "unix:/";

        internal static class BuilderProperties
        {
            internal static string ServerFeatures = "server.Features";
            internal static string ApplicationServices = "application.Services";
        }
    }
}
