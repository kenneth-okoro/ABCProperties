using Microsoft.Extensions.DependencyInjection;
using System.Reflection;

namespace ABCProperties.Application
{
    public static class ApplicationServicesRegistration
    {
        public static IServiceCollection AddApplicationServices(this IServiceCollection services)
        {
            var assembly = Assembly.GetExecutingAssembly();

            return services
                .AddMediatR(config =>
                {
                    config.RegisterServicesFromAssembly(assembly);
                });
        }
    }
}
