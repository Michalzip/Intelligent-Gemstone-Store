using IntelligentStore.Domain;
using Microsoft.EntityFrameworkCore;
using IntelligentStore.Infrastructure.EF.Configuration;

namespace IntelligentStore.Infrastructure.EF.Context
{
    public class ApplicationDbContext : DbContext
    {
        public DbSet<Admin> Admins { get; set; }

        public DbSet<User> Users { get; set; }
        public DbSet<Address> Addresses { get; set; }

        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options) { }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfiguration(new AddressConfiguration());
            modelBuilder.ApplyConfiguration(new UserConfiguration());
            modelBuilder.ApplyConfiguration(new AdminConfiguration());
        }
    }
}
