using Meteo.Breeze.Http.Features;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Meteo.Breeze.Server
{
    /// <summary>
    /// Represents a http server.
    /// </summary>
    public interface IServer
    {
        /// <summary>
        /// A collection of HTTP features of the server.
        /// </summary>
        IFeatureCollection Features { get; }

        /// <summary>
        /// Start server with application.
        /// </summary>
        /// <typeparam name="TContext"></typeparam>
        /// <param name="application"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        Task StartAsync<TContext>(IHttpApplication<TContext> application, CancellationToken cancellationToken);

        /// <summary>
        /// Stop processing requests and shut down the server.
        /// </summary>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        Task StopAsync(CancellationToken cancellationToken);
    }
}
