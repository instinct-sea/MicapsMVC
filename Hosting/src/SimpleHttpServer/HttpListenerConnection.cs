/* ***********************************************
 * Copyright (c) 2018 luoshasha. All rights reserved";
 * CLR version: 4.0.30319.42000"
 * File name:   HttpListenConnection.cs"
 * Date:        7/3/2018 5:53:57 PM
 * Author :  sand
 * Email  :  luoshasha@foxmail.com
 * Description: 
	
 * History:  created by sand 7/3/2018 5:53:57 PM
 
 * ***********************************************/
using Meteo.Breeze.Http.Features;
using Meteo.Breeze.Server.Features;
using Meteo.Breeze.Server.Http;
using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Meteo.Breeze.Server.Simple
{
    class HttpListenerConnection : TransportConnection, 
        IHttpConnectionFeature,
        IConnectionLifetimeFeature,
        IDisposable
    {
        private readonly HttpListenerContext _context;
        private IFeatureCollection _features;
        private CancellationTokenSource _connectionClosedCts = new CancellationTokenSource();
        private int _closed;

        public HttpListenerConnection(HttpListenerContext context)
        {
            _context = context;
            _features = new FeatureCollection
            {
                [context.GetType()] = context
            };
            _features.Set<IHttpConnectionFeature>(this);

            LocalAddress = _context.Request.LocalEndPoint.Address;
            LocalPort = _context.Request.LocalEndPoint.Port;
            RemoteAddress = _context.Request.RemoteEndPoint.Address;
            RemotePort = _context.Request.RemoteEndPoint.Port;
        }

        public override IFeatureCollection Features => _features;

        public CancellationToken ConnectionClosed
        {
            get => _connectionClosedCts.Token;
            set => throw new NotImplementedException();
        }

        public void Abort()
        {
            Abort(null);
        }

        public override Task CloseAsync()
        {
            if (Interlocked.Exchange(ref _closed, 1) == 1)
            {
                return _connectionClosedCts.Token.AsTask();
            }
            _context.Response.Close();
            _connectionClosedCts.Cancel();
            return Task.FromResult<object>(null);
        }

        public void Dispose()
        {
            if (_closed == 0)
            {
                //this should not happen.
                //connection should close when reach here.
                //if not closed, wait for close before dispose.
                CloseAsync().GetAwaiter().GetResult();
            }
            _connectionClosedCts.Dispose();
        }

        protected override void DoAbort(ConnectionAbortedException abortReason)
        {
            _context.Response.Abort();
        }
    }
}
