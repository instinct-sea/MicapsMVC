
namespace Meteo.Breeze.Http
{
    public interface IHttpContextAccessor
    {
        HttpContext HttpContext { get; set; }
    }
}