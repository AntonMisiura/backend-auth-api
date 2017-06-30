using AuthAPI.Helpers;
using AuthAPI.Services;
using System.Web.Http;

namespace AuthAPI
{
    public static class WebApiConfig
    {
        public static void Register(HttpConfiguration config)
        {
            config.Filters.Add(new LoggingFilterAttribute());
            JWTService.GetTokensKeyFromFile();
            // Web API routes
            config.MapHttpAttributeRoutes();

            config.Routes.MapHttpRoute(
                name: "DefaultApi",
                routeTemplate: "api/{controller}/{id}",
                defaults: new { id = RouteParameter.Optional }
            );
        }
    }
}
