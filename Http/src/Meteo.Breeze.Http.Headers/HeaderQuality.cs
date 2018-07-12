using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Meteo.Breeze.Http.Headers
{
    public static class HeaderQuality
    {
        /// <summary>
        /// Quality factor to indicate a perfect match.
        /// </summary>
        public const double Match = 1.0;

        /// <summary>
        /// Quality factor to indicate no match.
        /// </summary>
        public const double NoMatch = 0.0;
    }
}
