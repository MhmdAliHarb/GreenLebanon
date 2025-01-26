using GreenLebanon.Taxi.ApplicationCore.Entities;
using GreenLebanon.Taxi.ApplicationCore.Repositories;
using GreenLebanon.Taxi.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace GreenLebanon.Taxi.Infrastructure.Repositories
{
    public class TripRepository : ITripRepository
    {
        private readonly AppDbContext appDbContext;

        public TripRepository(AppDbContext appDbContext)
        {
            this.appDbContext = appDbContext;
        }
        public async Task<int> AddTripAsync(Trip trip)
        {
            appDbContext.Trips.Add(trip);
            return await appDbContext.SaveChangesAsync();
        }

        public async Task<Trip> GetTripByIdAsync(int tripId, string userId)
        {
            return await appDbContext.Trips.FirstOrDefaultAsync(x => x.Id == tripId && x.ClientId == userId);
        }

        public async Task<IQueryable<Trip>> GetAllTripsAsync(string userId)
        {
            return appDbContext.Trips.Where(x => x.ClientId == userId);
        }
    }
}
