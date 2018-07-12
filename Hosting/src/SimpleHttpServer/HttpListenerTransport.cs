/* ***********************************************
 * Copyright (c) 2018 luoshasha. All rights reserved";
 * CLR version: 4.0.30319.42000"
 * File name:   HttpListenerChannel.cs"
 * Date:        7/4/2018 10:02:41 AM
 * Author :  sand
 * Email  :  luoshasha@foxmail.com
 * Description: 
	
 * History:  created by sand 7/4/2018 10:02:41 AM
 
 * ***********************************************/
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace Meteo.Breeze.Server.Simple
{
    class HttpListenerTransport : ITransport
    {
        private readonly HttpListener _listener;
        private readonly IConnectionHandler _connectionHandler;

        public HttpListenerTransport(IEndPoint endPoint, IConnectionHandler handler)
        {
            _connectionHandler = handler;

            string prefix = $"http://+:{endPoint.EndPoint.Port}/";

            UrlAclHelper.RemoveAddress(prefix);
            UrlAclHelper.AddAddress(prefix);

            _listener = new HttpListener();
            _listener.Prefixes.Add(prefix);
            //_listener.Prefixes.Add("http://+:80/");
        }

        public async Task StartAsync()
        {
            _listener.Start();

            async Task startListener()
            {
                while (true)
                {
                    try
                    {
                        var context = await _listener.GetContextAsync().ConfigureAwait(false);

                        //a connection has got
                        var connection = new HttpListenerConnection(context);

                        //post connection handler into threadpool to execute
                        _ = Task.Run(async () =>
                         {
                             //await middleware task.
                             await _connectionHandler.OnConnection(connection).ConfigureAwait(false);

                             //dispose connection.
                             connection.Dispose();
                         });
                    }
                    catch
                    {
                        break;
                    }
                }
            };

            await startListener().ConfigureAwait(false);
        }

        public async Task StopAsync()
        {
            if (_listener.IsListening)
            {
                await Task.Run(() =>
                {
                    _listener.Stop();
                }).ConfigureAwait(false);
            }
        }

        public async Task CloseAsync()
        {
            await Task.Run(() =>
            {
                _listener.Close();
            }).ConfigureAwait(false);
        }
    }
}
