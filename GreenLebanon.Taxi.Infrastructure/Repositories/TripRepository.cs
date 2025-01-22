using GreenLebanon.Taxi.ApplicationCore.Entities;
using GreenLebanon.Taxi.ApplicationCore.Repositories;
using GreenLebanon.Taxi.Infrastructure.Data;

namespace GreenLebanon.Taxi.Infrastructure.Repositories
{
    public class TripRepository : ITripRepository
    {
        private readonly AppDbContext appDbContext;

        public TripRepository( AppDbContext appDbContext )
        {
            this.appDbContext = appDbContext;
        }
        public async Task<int> AddTripAsync( Trip trip )
        {
            appDbContext.Trips.Add(trip);
            return await appDbContext.SaveChangesAsync();
        }

        public async Task<IQueryable<Trip>> GetAllTripsAsync( int? tripId = null )
        {
            if ( tripId.HasValue )
            {
                return appDbContext.Trips.Where(x => tripId.HasValue && x.Id == tripId.Value);
            }
            return appDbContext.Trips.AsQueryable();
        }

       
    }
}
