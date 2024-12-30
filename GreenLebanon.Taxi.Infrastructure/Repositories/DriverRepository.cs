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
    public class DriverRepository : IDriverRepository
    {
        private readonly AppDbContext appDbContext;

        public DriverRepository(AppDbContext appDbContext)
        {
            this.appDbContext = appDbContext;
        }

        public async Task<int> AddDriverAsync(Driver driver)
        {
            appDbContext.Drivers.Add(driver);
            return await appDbContext.SaveChangesAsync();
        }

        public async Task<IQueryable<Driver>> GetAllDriversAsync(string driverId = null)
        {
            if (!string.IsNullOrEmpty(driverId))
            {
                appDbContext.Drivers.Where(x => x.ApplicationUserId == driverId);
            }
            return appDbContext.Drivers.AsQueryable();
        }
    }
}
