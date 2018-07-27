using Meteo.Breeze.Http.Features;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Meteo.Breeze.Http
{
    public abstract class HttpResponse
    {
        /// <summary>
        /// Gets the <see cref="HttpContext"/> for this response.
        /// </summary>
        public abstract HttpContext HttpContext { get; }

        /// <summary>
        /// Gets or sets the HTTP response code.
        /// </summary>
        public abstract int StatusCode { get; set; }

        /// <summary>
        /// Gets the response headers.
        /// </summary>
        public abstract IHeaderDictionary Headers { get; }

        /// <summary>
        /// Gets or sets the response body <see cref="Stream"/>.
        /// </summary>
        public abstract Stream Body { get; set; }

        /// <summary>
        /// Gets or sets the value for the <c>Content-Length</c> response header.
        /// </summary>
        public abstract long? ContentLength { get; set; }

        /// <summary>
        /// Gets or sets the value for the <c>Content-Type</c> response header.
        /// </summary>
        public abstract string ContentType { get; set; }

        /// <summary>
        /// Gets an object that can be used to manage cookies for this response.
        /// </summary>
        public abstract IResponseCookies Cookies { get; }

        /// <summary>
        /// Gets a value indicating whether response headers have been sent to the client.
        /// </summary>
        public abstract bool HasStarted { get; }

        /// <summary>
        /// Adds a delegate to be invoked just before response headers will be sent to the client.
        /// </summary>
        /// <param name="callback">The delegate to execute.</param>
        /// <param name="state">A state object to capture and pass back to the delegate.</param>
        public abstract void OnStarting(Func<object, Task> callback, object state);

        /// <summary>
        /// Adds a delegate to be invoked just before response headers will be sent to the client.
        /// </summary>
        /// <param name="callback">The delegate to execute.</param>
        public virtual void OnStarting(Func<Task> callback) => OnStarting(_=>callback(), null);

        /// <summary>
        /// Registers an object for disposal by the host once the request has finished processing.
        /// </summary>
        /// <param name="disposable">The object to be disposed.</param>
        public virtual void RegisterForDispose(IDisposable disposable) => OnCompleted(state=>
        {
            ((IDisposable)state).Dispose();
            return Task.FromResult<object>(null);
        }, disposable);

        /// <summary>
        /// Adds a delegate to be invoked after the response has finished being sent to the client.
        /// </summary>
        /// <param name="callback">The delegate to invoke.</param>
        /// <param name="state">A state object to capture and pass back to the delegate.</param>
        public abstract void OnCompleted(Func<object, Task> callback, object state);

        /// <summary>
        /// Returns a temporary redirect response (HTTP 302) to the client.
        /// </summary>
        /// <param name="location">The URL to redirect the client to. This must be properly encoded for use in http headers
        /// where only ASCII characters are allowed.</param>
        public virtual void Redirect(string location) => Redirect(location, permanent: false);

        /// <summary>
        /// Returns a redirect response (HTTP 301 or HTTP 302) to the client.
        /// </summary>
        /// <param name="location">The URL to redirect the client to. This must be properly encoded for use in http headers
        /// where only ASCII characters are allowed.</param>
        /// <param name="permanent"><c>True</c> if the redirect is permanent (301), otherwise <c>false</c> (302).</param>
        public abstract void Redirect(string location, bool permanent);
    }
}
