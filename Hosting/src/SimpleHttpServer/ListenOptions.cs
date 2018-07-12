/* ***********************************************
 * Copyright (c) 2018 luoshasha. All rights reserved";
 * CLR version: 4.0.30319.42000"
 * File name:   ListenOption.cs"
 * Date:        7/3/2018 4:17:45 PM
 * Author :  sand
 * Email  :  luoshasha@foxmail.com
 * Description: 
	
 * History:  created by sand 7/3/2018 4:17:45 PM
 
 * ***********************************************/
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace Meteo.Breeze.Server.Simple
{
    using ConnectionMiddleware = Func<ConnectionDelegate, ConnectionDelegate>;

    public sealed class ListenOptions : IConnectionBuilder, IEndPoint
    {
        private List<ConnectionMiddleware> _connectionDelegates = new List<ConnectionMiddleware>();

        public ListenOptions(int port)
            : this(new IPEndPoint(IPAddress.Loopback, port))
        {
        }

        public ListenOptions(IPEndPoint endpoint)
        {
            EndPoint = endpoint;
        }

        public IPEndPoint EndPoint
        {
            get;
            set;
        }

        public string UriPath
        {
            get;
            set;
        }

        public ConnectionDelegate Build()
        {
            ConnectionDelegate app = context =>
            {
                return Task.FromResult<object>(null);
            };
            for (int i = _connectionDelegates.Count - 1; i >= 0; i--)
            {
                var component = _connectionDelegates[i];
                app = component(app);
            }
            return app;
        }

        public IConnectionBuilder Use(Func<ConnectionDelegate, ConnectionDelegate> connectionMiddleware)
        {
            _connectionDelegates.Add(connectionMiddleware);
            return this;
        }
    }
}
