using Microsoft.Extensions.DependencyInjection;
using WebApi.DataAccess;


namespace WebApi.Infrastructure
{
    public static class ServiceRegistrationExtensions
    {
        public static void ConfigureDependencyInjection(this IServiceCollection services)
        {
            services.AddTransient<IRouteStopDa, RouteStopDa>();
        }
    }
}
