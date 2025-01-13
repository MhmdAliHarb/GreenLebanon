using GreenLebanon.Taxi.ApplicationCore.Entities;
using GreenLebanon.Taxi.ApplicationCore.Repositories;
using GreenLebanon.Taxi.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace GreenLebanon.Taxi.Infrastructure.Repositories
{
    public class DriverRepository(AppDbContext appDbContext) : IDriverRepository
    {
        private readonly AppDbContext appDbContext = appDbContext;

        public async Task<IQueryable<ApplicationUser>> GetAllDriversAsync(string driverId = null)
        {
            var result = appDbContext.ApplicationUsers
                      .Join(appDbContext.AspNetUserRoles, x => x.Id, c => c.UserId, (x, c) => x);

            if (!string.IsNullOrEmpty(driverId))
            {
                result = result.Where(x => x.Id == driverId);
            }

            return result;
        }

        //AsNoTracking mnesta3mela lamma ma bdna na3mel update 3l state taba3 l entity
        //ya3ne bs lamma bde kon 3m 2o2ra minna
        public async Task<Driver> GetAvailableDriverAsync(string region)
        {
            return appDbContext.Trips
                .AsNoTracking()
                .Where(x => x.Status == Status.Done && x.Region == region)
                .Join(appDbContext.Drivers, t => t.DriverId, x => x.UserId, (t, x) => x)
                .FirstOrDefault() ?? new Driver();
        }
    }
}
