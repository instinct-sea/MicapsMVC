using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Meteo.Breeze.Server
{
    /// <summary>
    /// Indicates a delegate to handle new connection.
    /// </summary>
    /// <param name="context"></param>
    /// <returns></returns>
    public delegate Task ConnectionDelegate(ConnectionContext context);
}
