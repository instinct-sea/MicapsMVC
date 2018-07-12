using Meteo.Breeze.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Meteo.Breeze.MVC.Routing
{
    public interface IRouteHandler
    {
        RequestDelegate GetRequestHandler(HttpContext httpContext, RouteData routeData);
    }
}
