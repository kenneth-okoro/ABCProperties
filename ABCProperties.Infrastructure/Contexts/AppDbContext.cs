using ABCProperties.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace ABCProperties.Infrastructure.Contexts
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {
        }

        public DbSet<Agent> Agents { get; set; }
        public DbSet<Property> Properties { get; set; }
    }
}
