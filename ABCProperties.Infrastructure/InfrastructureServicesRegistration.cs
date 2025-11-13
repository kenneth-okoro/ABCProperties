using ABCProperties.Application.Features.Agents;
using ABCProperties.Application.Features.Properties;
using ABCProperties.Infrastructure.Contexts;
using ABCProperties.Infrastructure.Services;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace ABCProperties.Infrastructure
{
    public static class InfrastructureServicesRegistration
    {
        public static IServiceCollection AddInfrastructureServices(this IServiceCollection services, 
            IConfiguration configuration)
        {
            services.AddDbContext<AppDbContext>(options => options
                .UseSqlServer(configuration.GetConnectionString("DefaultConnection"), builder =>
                {
                    builder.MigrationsHistoryTable("Migrations", "EFCore");
                }));
            services.AddScoped<IAgentService, AgentService>();
            services.AddScoped<IPropertyService, PropertyService>();

            return services;
        }
    }
}
