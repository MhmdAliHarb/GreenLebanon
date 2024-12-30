using GreenLebanon.Taxi.ApplicationCore.Entities;
using GreenLebanon.Taxi.ApplicationCore.Repositories;
using GreenLebanon.Taxi.Infrastructure.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GreenLebanon.Taxi.Infrastructure.Repositories
{
    public class DriverRepository(AppDbContext appDbContext) : IDriverRepository
    {
        private readonly AppDbContext appDbContext = appDbContext;

        public async Task<IQueryable<ApplicationUser>> GetAllDriversAsync(string driverId = null)
        {
            if (!string.IsNullOrEmpty(driverId))
            {
                appDbContext.ApplicationUsers.Where(x => x.Id == driverId);
            }
            return appDbContext.ApplicationUsers.AsQueryable();
        }
    }
}
