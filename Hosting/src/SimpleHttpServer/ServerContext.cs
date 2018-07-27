/* ***********************************************
 * Copyright (c) 2018 luoshasha. All rights reserved";
 * CLR version: 4.0.30319.42000"
 * File name:   ServiceContext.cs"
 * Date:        7/5/2018 11:03:54 AM
 * Author :  sand
 * Email  :  luoshasha@foxmail.com
 * Description: 
	
 * History:  created by sand 7/5/2018 11:03:54 AM
 
 * ***********************************************/
using Meteo.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Meteo.Breeze.Server.Simple
{
    class ServerContext
    {
        public HttpConnectionManager ConnectionManager { get; set; }

        public ServerOptions ServerOptions { get; set; }

        public ILoggingService Logger { get; set; }

        internal IServiceProvider ApplicationServices
        {
            get;
            set;
        }
    }
}
