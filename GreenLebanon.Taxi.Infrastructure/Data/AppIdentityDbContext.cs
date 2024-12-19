using GreenLebanon.Taxi.ApplicationCore.Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GreenLebanon.Taxi.Infrastructure.Data
{
    public class AppIdentityDbContext(DbContextOptions<AppIdentityDbContext> options) : IdentityDbContext <IdentityUser> (options)
    {
        public DbSet<Client> clients { get; set; }
    }
}
