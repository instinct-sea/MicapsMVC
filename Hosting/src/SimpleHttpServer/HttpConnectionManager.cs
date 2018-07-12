/* ***********************************************
 * Copyright (c) 2018 luoshasha. All rights reserved";
 * CLR version: 4.0.30319.42000"
 * File name:   HttpConnectionManager.cs"
 * Date:        7/5/2018 11:10:19 AM
 * Author :  sand
 * Email  :  luoshasha@foxmail.com
 * Description: 
	
 * History:  created by sand 7/5/2018 11:10:19 AM
 
 * ***********************************************/
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Meteo.Breeze.Server.Simple
{
    class HttpConnectionManager
    {
        private readonly ConcurrentDictionary<long, HttpConnectionReference> _connectionReferences = new ConcurrentDictionary<long, HttpConnectionReference>();
        public HttpConnectionManager()
        {

        }

        public void AddConnection(long id, HttpConnection connection)
        {
            if (!_connectionReferences.TryAdd(id, new HttpConnectionReference(connection)))
            {
                throw new ArgumentException(nameof(id));
            }
        }

        public void RemoveConnection(long id)
        {
            if (!_connectionReferences.TryRemove(id, out _))
            {
                throw new ArgumentException(nameof(id));
            }
        }

        public void Walk(Action<HttpConnection> callback)
        {
            foreach (var kvp in _connectionReferences)
            {
                var reference = kvp.Value;

                if (reference.TryGetConnection(out var connection))
                {
                    callback(connection);
                }
                else if (_connectionReferences.TryRemove(kvp.Key, out reference))
                {
                    // It's safe to modify the ConcurrentDictionary in the foreach.
                    // The connection reference has become unrooted because the application never completed.
                }
                // If both conditions are false, the connection was removed during the heartbeat.
            }
        }
    }
}
