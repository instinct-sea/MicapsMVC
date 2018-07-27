/* ***********************************************
 * Copyright (c) 2018 luoshasha. All rights reserved";
 * CLR version: 4.0.30319.42000"
 * File name:   Server.cs"
 * Date:        7/3/2018 3:44:47 PM
 * Author :  sand
 * Email  :  luoshasha@foxmail.com
 * Description: 
	
 * History:  created by sand 7/3/2018 3:44:47 PM
 
 * ***********************************************/
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Meteo.Breeze.Http.Features;
using Meteo.Extensions.Logging;

namespace Meteo.Breeze.Server.Simple
{
    class Server : IServer, IDisposable
    {
        private ListenOptions _listenOption;
        private bool _hasStarted;
        private int _stopping;
        private readonly ITransportFactory _transportFactory;
        private readonly ServerContext _context;
        private readonly List<ITransport> _transports = new List<ITransport>();
        private TaskCompletionSource<object> _stoppedTcs = new TaskCompletionSource<object>();

        public Server(ServerOptions options, ITransportFactory transportFactory, IServiceProvider services, ILoggingService logger)
        {
            _listenOption = options.ListenOptions[0];
            _transportFactory = transportFactory;
            _context = new ServerContext() {
                ConnectionManager = new HttpConnectionManager(),
                ServerOptions = options,
                Logger = logger,
                ApplicationServices = services};
        }

        public IFeatureCollection Features
        {
            get;
        }

        public HttpConnectionManager ConnectionManager => _context.ConnectionManager;

        public void Dispose()
        {
            CancellationTokenSource cancellation = new CancellationTokenSource();
            cancellation.Cancel();
            StopAsync(cancellation.Token).GetAwaiter().GetResult();
        }

        public async Task StartAsync<TContext>(IHttpApplication<TContext> application, CancellationToken cancellationToken)
        {
            if (_hasStarted)
            {
                throw new InvalidOperationException("Server has started already!");
            }
            _hasStarted = true;
            try
            {
                _listenOption.UseHttpServer(_context, application, HttpVersion.Http1);

                var connectionDelegate = _listenOption.Build();
                var transport = _transportFactory.Create(_listenOption, new ConnectionHandler(connectionDelegate));
                _transports.Add(transport);

                _context.Logger.Log($"Server start listening at {_listenOption.EndPoint}", LoggingLevel.Debug);

                await transport.StartAsync().ConfigureAwait(false);
            }
            catch(Exception e)
            {
                _context.Logger.Log($"Start server failed", LoggingLevel.Debug, e);
                Dispose();
                throw;
            }
        }

        public async Task StopAsync(CancellationToken cancellationToken)
        {
            if (Interlocked.Exchange(ref _stopping, 1) == 1)
            {
                await _stoppedTcs.Task.ConfigureAwait(false);
                return;
            }

            try
            {
                //Stop transports to accept incoming connections.
                var tasks = new Task[_transports.Count];
                for (int i = 0; i < _transports.Count; i++)
                {
                    tasks[i] = _transports[i].StopAsync();
                }
                await Task.WhenAll(tasks).ConfigureAwait(false);

                if (!await CloseAllConnectionsAsync(cancellationToken).ConfigureAwait(false))
                {
                    //not all connection closed, try to about.
                    if (!await AbortAllConnectionsAsync().ConfigureAwait(false))
                    {
                        //Not all connection aborted.
                    }
                }

                //Close all transports.
                for (int i = 0; i < _transports.Count; i++)
                {
                    tasks[i] = _transports[i].CloseAsync();
                }
                await Task.WhenAll(tasks).ConfigureAwait(false);
            }
            catch(Exception e)
            {
                _stoppedTcs.TrySetException(e);
            }
            _stoppedTcs.TrySetResult(null);
        }

        public async Task<bool> CloseAllConnectionsAsync(CancellationToken token)
        {
            var closeTasks = new List<Task>();

            ConnectionManager.Walk(connection =>
            {
                closeTasks.Add(connection.StopProcessingNextRequestAsync());
            });

            var allClosedTask = Task.WhenAll(closeTasks.ToArray());
            return await Task.WhenAny(allClosedTask, token.AsTask()).ConfigureAwait(false) == allClosedTask;
        }

        public async Task<bool> AbortAllConnectionsAsync()
        {
            var abortTasks = new List<Task>();
            var canceledException = new ConnectionAbortedException("Connection aborted while server shutdown!");

            ConnectionManager.Walk(connection =>
            {
                abortTasks.Add(connection.AbortAsync(canceledException));
            });

            var allAbortedTask = Task.WhenAll(abortTasks.ToArray());
            return await Task.WhenAny(allAbortedTask, Task.Delay(1000)).ConfigureAwait(false) == allAbortedTask;
        }
    }
}
