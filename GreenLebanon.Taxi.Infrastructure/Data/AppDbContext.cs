using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
        public DbSet<ApplicationCore.Entities.Client> Clients { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)

        {

            optionsBuilder.UseSqlServer("Data Source=DESKTOP-D4GC2UT;Initial Catalog=GreenLebanon;Integrated Security=True;Encrypt=True;Trust Server Certificate=True");

        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
        }
    }
}
