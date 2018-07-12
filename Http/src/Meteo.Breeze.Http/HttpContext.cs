using Meteo.Breeze.Http.Features;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;

namespace Meteo.Breeze.Http
{
    public abstract class HttpContext
    {
        /// <summary>
        ///  Gets the collection of HTTP features provided by the server and middleware available on this request.
        /// </summary>
        public abstract IFeatureCollection Features { get; }

        /// <summary>
        ///  Gets the <see cref="HttpRequest"/> object for this request.
        /// </summary>
        public abstract HttpRequest Request { get; }

        /// <summary>
        ///  Gets the <see cref="HttpResponse"/> object for this request.
        /// </summary>
        public abstract HttpResponse Response { get; }

        /// <summary>
        /// Notifies when the connection underlying this request is aborted and thus request operations should be
        /// cancelled.
        /// </summary>
        public abstract CancellationToken RequestAborted { get; set; }

        /// <summary>
        /// Gets or sets the <see cref="IServiceProvider"/> that provides access to the request's service container.
        /// </summary>
        public abstract IServiceProvider RequestServices { get; set; }

        /// <summary>
        ///  Gets or sets the object used to manage user session data for this request.
        /// </summary>
        public abstract ISession Session { get; set; }

        /// <summary>
        /// Aborts the connection underlying this request.
        /// </summary>
        public abstract void Abort();
    }
}
