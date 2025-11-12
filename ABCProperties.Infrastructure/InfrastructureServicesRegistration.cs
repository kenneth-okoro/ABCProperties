using ABCProperties.Infrastructure.Contexts;
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
            // Infrastructure service registrations go here
            return services
                .AddDbContext<AppDbContext>(options => options
                    .UseSqlServer(configuration.GetConnectionString("DefaultConnection"), builder =>
                    {
                        builder.MigrationsHistoryTable("Migrations", "EFCore");
                    }));
        }
    }
}
