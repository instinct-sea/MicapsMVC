using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Meteo.Breeze.MVC.Routing
{
    public interface IRouter
    {
        Task RouteAsync(RouteContext context);

        VirtualPathData GetVirtualPath(VirtualPathContext context);
    }
}
