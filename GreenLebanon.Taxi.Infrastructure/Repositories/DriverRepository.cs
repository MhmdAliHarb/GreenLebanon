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

        public DriverRepository(AppDbContext appDbContext )
        {
            this.appDbContext = appDbContext;
        }

        async Task<int> IDriverRepository.AddDriverAsync( Driver driver )
        {
            appDbContext.Drivers.Add(driver);
            return await appDbContext.SaveChangesAsync();
        }

        async Task<IQueryable<Driver>> IDriverRepository.GetAllDriversAsync( int? DriverId )
        {
            if ( DriverId.HasValue )
            {
                appDbContext.Drivers.Where(x => DriverId.HasValue && x.Id == DriverId.Value);
            }
            return appDbContext.Drivers.AsQueryable();
        }
    }
}
