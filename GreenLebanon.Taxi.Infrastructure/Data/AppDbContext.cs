using GreenLebanon.Taxi.ApplicationCore.Entities;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace GreenLebanon.Taxi.Infrastructure.Data
{
    public class AppDbContext(DbContextOptions<AppDbContext> options) : IdentityDbContext(options)
    {
        public DbSet<Client> Clients { get; set; }
        public DbSet<Driver> Drivers { get; set; }
        public DbSet<Trip> Trips { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<Driver>()
                .HasMany(d => d.Trips)
                .WithOne(t => t.Driver)
                .HasForeignKey(t => t.DriverId)
                .OnDelete(DeleteBehavior.NoAction);
        }
    }
}
