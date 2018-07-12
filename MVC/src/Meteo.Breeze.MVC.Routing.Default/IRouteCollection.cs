using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Meteo.Breeze.MVC.Routing.Default
{
    public interface IRouteCollection:IRouter
    {
        void Add(IRouter router);
    }
}
