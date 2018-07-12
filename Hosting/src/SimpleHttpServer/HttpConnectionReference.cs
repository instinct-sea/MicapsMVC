/* ***********************************************
 * Copyright (c) 2018 luoshasha. All rights reserved";
 * CLR version: 4.0.30319.42000"
 * File name:   HttpConnectionReference.cs"
 * Date:        7/5/2018 11:14:11 AM
 * Author :  sand
 * Email  :  luoshasha@foxmail.com
 * Description: 
	
 * History:  created by sand 7/5/2018 11:14:11 AM
 
 * ***********************************************/
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Meteo.Breeze.Server.Simple
{
    class HttpConnectionReference
    {
        private readonly WeakReference<HttpConnection> _weakReference;

        public HttpConnectionReference(HttpConnection connection)
        {
            _weakReference = new WeakReference<HttpConnection>(connection);
            ConnectionId = connection.ConnectionId;
        }

        public string ConnectionId { get; }

        public bool TryGetConnection(out HttpConnection connection)
        {
            return _weakReference.TryGetTarget(out connection);
        }
    }
}
