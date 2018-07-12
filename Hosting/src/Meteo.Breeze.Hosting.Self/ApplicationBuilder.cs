using Meteo.Breeze.Http;
using Meteo.Breeze.Http.Features;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Meteo.Breeze.Hosting.Self
{
    using Middleware = Func<RequestDelegate, RequestDelegate>;

    public class ApplicationBuilder : IApplicationBuilder
    {
        private List<Middleware> _applications = new List<Middleware>();

        public ApplicationBuilder(IServiceProvider services)
        {
            ApplicationServices = services;
            Properties = new Dictionary<string, object>();
        }

        private ApplicationBuilder(ApplicationBuilder parent)
        {
            ApplicationServices = parent.ApplicationServices;
            Properties = new Dictionary<string, object>(parent.Properties);
            ServerFeatures = parent.ServerFeatures;
        }

        public IServiceProvider ApplicationServices
        {
            get;
            set;
        }

        public IFeatureCollection ServerFeatures
        {
            get;
            set;
        }

        public IDictionary<string, object> Properties
        {
            get;
        }

        public RequestDelegate Build()
        {
            RequestDelegate app = context =>
            {
                context.Response.StatusCode = 404;
                return Task.FromResult<object>(null);
            };
            for (int i = _applications.Count - 1; i >=  0; i--)
            {
                app = _applications[i](app);
            }
            return app;
        }

        public IApplicationBuilder New()
        {
            return new ApplicationBuilder(this);
        }

        public IApplicationBuilder Use(Func<RequestDelegate, RequestDelegate> middleware)
        {
            _applications.Add(middleware);
            return this;
        }
    }
}
