/* ***********************************************
 * Copyright (c) 2018 luoshasha. All rights reserved";
 * CLR version: 4.0.30319.42000"
 * File name:   HttpConnectionContext.cs"
 * Date:        7/5/2018 10:04:15 AM
 * Author :  sand
 * Email  :  luoshasha@foxmail.com
 * Description: 
	
 * History:  created by sand 7/5/2018 10:04:15 AM
 
 * ***********************************************/
using Meteo.Breeze.Http.Features;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;

namespace Meteo.Breeze.Server.Simple
{
    public class HttpConnectionContext
    {
        public string ConnectionId { get; set; }
        public long HttpConnectionId { get; set; }
        public HttpVersion Protocols { get; set; }
        public ConnectionContext ConnectionContext { get; set; }
        public IFeatureCollection ConnectionFeatures { get; set; }
        public IPEndPoint LocalEndPoint { get; set; }
        public IPEndPoint RemoteEndPoint { get; set; }
    }
}
