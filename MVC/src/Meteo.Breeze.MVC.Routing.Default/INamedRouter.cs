using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Meteo.Breeze.MVC.Routing.Default
{
    public interface INamedRouter : IRouter
    {
        string Name { get; }
    }
}
