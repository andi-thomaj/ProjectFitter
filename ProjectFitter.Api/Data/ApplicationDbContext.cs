using Microsoft.EntityFrameworkCore;
using ProjectFitter.Api.Data.Entities;

namespace ProjectFitter.Api.Data
{
    public class ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : DbContext(options)
    {
        public DbSet<Customer?> Customers { get; set; }
        public DbSet<ICNumber> ICNumbers { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.ApplyConfigurationsFromAssembly(typeof(ApplicationDbContext).Assembly);
        }
    }
}
