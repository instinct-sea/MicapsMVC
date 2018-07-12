/* ***********************************************
 * Copyright (c) 2018 luoshasha. All rights reserved";
 * CLR version: 4.0.30319.42000"
 * File name:   ConnectionInfo.cs"
 * Date:        6/28/2018 11:16:41 AM
 * Author :  sand
 * Email  :  luoshasha@foxmail.com
 * Description: 
	
 * History:  created by sand 6/28/2018 11:16:41 AM
 
 * ***********************************************/
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Meteo.Breeze.Http
{
    public abstract class ConnectionInfo
    {
        /// <summary>
        /// Gets or sets a unique identifier to represent this connection.
        /// </summary>
        public string Id { get; set; }

        public IPAddress RemoteIpAddress { get; set; }

        public int RemotePort { get; set; }

        public IPAddress LocalIpAddress { get; set; }

        public int LocalPort { get; set; }

        public abstract Task<X509Certificate2> GetClientCertificateAsync(CancellationToken cancellationToken = new CancellationToken());
    }
}
