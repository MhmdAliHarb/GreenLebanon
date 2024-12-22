using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GreenLebanon.Taxi.ApplicationCore.Entities;

namespace GreenLebanon.Taxi.Infrastructure.Data
{
    public class AppDbContext: DbContext
    {
        public AppDbContext()
        {
            
        }
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {
            
        }
        public DbSet<Client> Clients { get; set; }
        public DbSet<Driver> Drivers { get; set; }
        public DbSet<Trip> Trips { get; set; }


        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)

        {

            optionsBuilder.UseSqlServer("");

        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.Entity<Client>()
                .HasMany(c => c.OrderedTrips)
                .WithOne(t => t.Client)
                .HasForeignKey(t => t.ClientId);

           modelBuilder.Entity<Driver>()
               .HasMany(d => d.Trips)
               .WithOne(t => t.Driver)
               .HasForeignKey(t => t.DriverId);
        }
    }
}
