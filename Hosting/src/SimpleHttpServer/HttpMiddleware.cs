/* ***********************************************
 * Copyright (c) 2018 luoshasha. All rights reserved";
 * CLR version: 4.0.30319.42000"
 * File name:   HttpMiddleware.cs"
 * Date:        7/3/2018 5:04:50 PM
 * Author :  sand
 * Email  :  luoshasha@foxmail.com
 * Description: 
	
 * History:  created by sand 7/3/2018 5:04:50 PM
 
 * ***********************************************/
using Meteo.Breeze.Http.Features;
using Meteo.Breeze.Server.Features;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace Meteo.Breeze.Server.Simple
{
    class HttpMiddleware<TContext>
    {
        private readonly IHttpApplication<TContext> _application;
        private readonly ServerContext _serverContext;
        private readonly HttpVersion _protocolVersion;

        public HttpMiddleware(ServerContext context, IHttpApplication<TContext> application, HttpVersion protocolVersion)
        {
            _serverContext = context;
            _application = application;
            _protocolVersion = protocolVersion;
        }

        public async Task OnConnectionAsync(ConnectionContext connectionContext)
        {
            var httpConnectionContext = new HttpConnectionContext
            {
                ConnectionId = connectionContext.ConnectionId,
                ConnectionContext = connectionContext,
                HttpConnectionId = connectionContext.ConnectionId.GetHashCode(),
                Protocols = _protocolVersion,
                ConnectionFeatures = connectionContext.Features,
            };

            var httpConnectionFeature = connectionContext.Features.Get<IHttpConnectionFeature>();

            if (httpConnectionFeature != null)
            {
                httpConnectionContext.RemoteEndPoint = new IPEndPoint(httpConnectionFeature.RemoteAddress, httpConnectionFeature.RemotePort);
                httpConnectionContext.LocalEndPoint = new IPEndPoint(httpConnectionFeature.LocalAddress, httpConnectionFeature.LocalPort);
            }

            var connection = new HttpConnection(httpConnectionContext);
            _serverContext.ConnectionManager.AddConnection(httpConnectionContext.HttpConnectionId, connection);

            //start process request.
            await connection.StartProcessRequest(_application);

            _serverContext.ConnectionManager.RemoveConnection(httpConnectionContext.HttpConnectionId);
        }
    }
}
