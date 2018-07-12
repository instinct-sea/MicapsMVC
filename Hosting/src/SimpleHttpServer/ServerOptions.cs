/* ***********************************************
 * Copyright (c) 2018 luoshasha. All rights reserved";
 * CLR version: 4.0.30319.42000"
 * File name:   ServerOptions.cs"
 * Date:        7/3/2018 4:16:07 PM
 * Author :  sand
 * Email  :  luoshasha@foxmail.com
 * Description: 
	
 * History:  created by sand 7/3/2018 4:16:07 PM
 
 * ***********************************************/
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;

namespace Meteo.Breeze.Server.Simple
{
    public sealed class ServerOptions
    {
        internal List<ListenOptions> ListenOptions { get; } = new List<ListenOptions>();

        /// <summary>
        /// Bind to given IP address and port.
        /// The callback configures endpoint-specific settings.
        /// </summary>
        public void Listen(IPAddress address, int port, Action<ListenOptions> configure)
        {
            if (address == null)
            {
                throw new ArgumentNullException(nameof(address));
            }

            Listen(new IPEndPoint(address, port), configure);
        }

        /// <summary>
        /// Bind to given IP endpoint.
        /// </summary>
        public void Listen(IPEndPoint endPoint)
        {
            Listen(endPoint, _ => { });
        }

        /// <summary>
        /// Bind to given IP address and port.
        /// The callback configures endpoint-specific settings.
        /// </summary>
        public void Listen(IPEndPoint endPoint, Action<ListenOptions> configure)
        {
            if (endPoint == null)
            {
                throw new ArgumentNullException(nameof(endPoint));
            }
            if (configure == null)
            {
                throw new ArgumentNullException(nameof(configure));
            }

            var listenOptions = new ListenOptions(endPoint);
            configure(listenOptions);
            ListenOptions.Add(listenOptions);
        }

        /// <summary>
        /// Listens on ::1 and 127.0.0.1 with the given port. Requesting a dynamic port by specifying 0 is not supported
        /// for this type of endpoint.
        /// </summary>
        public void ListenLocalhost(int port) => ListenLocalhost(port, options => { });

        /// <summary>
        /// Listens on ::1 and 127.0.0.1 with the given port. Requesting a dynamic port by specifying 0 is not supported
        /// for this type of endpoint.
        /// </summary>
        public void ListenLocalhost(int port, Action<ListenOptions> configure)
        {
            if (configure == null)
            {
                throw new ArgumentNullException(nameof(configure));
            }

            var listenOptions = new ListenOptions(port);
            configure(listenOptions);
            ListenOptions.Add(listenOptions);
        }
    }
}
