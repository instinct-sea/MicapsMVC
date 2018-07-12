using Meteo.Common.Types;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Meteo.Breeze.Http.Features
{
    public interface IHeaderDictionary : IDictionary<string,StringValues>
    {
        new StringValues this[string key] { get;set; }
        long? ContentLength { get; set; }
    }
}
