using GreenLebanon.Taxi.ApplicationCore.Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace GreenLebanon.Taxi.Infrastructure.Data
{
    public class AppDbContext(DbContextOptions<AppDbContext> options) : IdentityDbContext(options)
    {
        public DbSet<ApplicationUser> ApplicationUsers { get; set; }

        public DbSet<IdentityUserRole<string>> AspNetUserRoles { get; set; }

        public DbSet<IdentityRole> AspNetRoles { get; set; }

        public DbSet<Trip> Trips { get; set; }

        public DbSet<Driver> Drivers { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<Driver>().HasKey(x => x.UserId);

            modelBuilder.Entity<ApplicationUser>()
                .HasMany(d => d.Trips)
                .WithOne(t => t.Client)
                .HasForeignKey(t => t.ClientId)
                .OnDelete(DeleteBehavior.NoAction);

            modelBuilder.Entity<Trip>().Property(x => x.DriverId).IsRequired(false);
            
            modelBuilder.Entity<Trip>()
                .HasOne(d => d.Driver)
                .WithMany()
                .HasForeignKey(x => x.DriverId)
                .OnDelete(DeleteBehavior.NoAction);

            modelBuilder.Entity<Driver>()
                .HasOne(d => d.ApplicationUser)
                .WithMany()
                .HasForeignKey(t => t.UserId)
                .OnDelete(DeleteBehavior.NoAction);

            modelBuilder.Entity<IdentityRole>().HasData(
                new IdentityRole() { Name = "Admin", NormalizedName = "ADMIN" },
                new IdentityRole() { Name = "Client", NormalizedName = "CLIENT" },
                new IdentityRole() { Name = "Driver", NormalizedName = "DRIVER" }
                );
        }
    }
}
