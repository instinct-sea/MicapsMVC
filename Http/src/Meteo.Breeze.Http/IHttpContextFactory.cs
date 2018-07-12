using Meteo.Breeze.Http.Features;

namespace Meteo.Breeze.Http
{
    public interface IHttpContextFactory
    {
        HttpContext Create(IFeatureCollection featureCollection);
        void Dispose(HttpContext httpContext);
    }
}
