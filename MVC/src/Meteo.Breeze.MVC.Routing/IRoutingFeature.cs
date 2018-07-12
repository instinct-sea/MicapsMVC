using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Meteo.Breeze.MVC.Routing
{
    public interface IRoutingFeature
    {
        /// <summary>
        /// Gets or sets the <see cref="Routing.RouteData"/> associated with the current request.
        /// </summary>
        RouteData RouteData { get; set; }
    }
}
