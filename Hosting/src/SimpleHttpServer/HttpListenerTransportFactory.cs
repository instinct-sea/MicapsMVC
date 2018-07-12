/* ***********************************************
 * Copyright (c) 2018 luoshasha. All rights reserved";
 * CLR version: 4.0.30319.42000"
 * File name:   HttpListenerTransportFactory.cs"
 * Date:        7/5/2018 6:03:48 PM
 * Author :  sand
 * Email  :  luoshasha@foxmail.com
 * Description: 
	
 * History:  created by sand 7/5/2018 6:03:48 PM
 
 * ***********************************************/
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Meteo.Breeze.Server.Simple
{
    class HttpListenerTransportFactory : ITransportFactory
    {
        public ITransport Create(IEndPoint endpoint, IConnectionHandler connectionHandler)
        {
            return new HttpListenerTransport(endpoint, connectionHandler);
        }
    }
}
