using FlyCheap.API.Helpers;
using FlyCheap.API.Interfaces;
using FlyCheap.API.Services;

namespace FlyCheap.API.Extensions
{
    public static class ServiceCollectionExtensions
    {
        public static IServiceCollection AddServices(this IServiceCollection services)
        {
            services.AddTransient<IDbInitializer, DbInitializer>();
            services.AddScoped<IFlightSearchResultService, FlightSearchResultService>();

            return services;
        }
    }
}
