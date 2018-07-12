using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace Meteo.Breeze.Server.Simple
{
    readonly struct HttpListenerConnectionContext
    {
        public readonly HttpListenerContext httpContext;
        public readonly HttpListener httpListener;

        public HttpListenerConnectionContext(HttpListener listener,
            HttpListenerContext context)
        {
            httpListener = listener;
            httpContext = context;
        }
    }
}
