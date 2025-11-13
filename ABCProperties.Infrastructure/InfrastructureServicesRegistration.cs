using ABCProperties.Application.Features.Agents;
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
            return services
                .AddDbContext<AppDbContext>(options => options
                    .UseSqlServer(configuration.GetConnectionString("DefaultConnection"), builder =>
                    {
                        builder.MigrationsHistoryTable("Migrations", "EFCore");
                    }))
                .AddScoped<IAgentService, AgentService>();
        }
    }
}
