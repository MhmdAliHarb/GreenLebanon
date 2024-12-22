using GreenLebanon.Taxi.ApplicationCore.Entities;
using GreenLebanon.Taxi.Shared.Requests;

namespace GreenLebanon.Taxi.Web.Infrastructure.Gateways
{
    public interface ITripGateway
    {
        Task<int> AddNewTripAsync(AddTripRequest request , CancellationToken cancellationToken);
       // Task<IQueryable<Trip>> GetTripsAsync( int? TripId, string status = null);
    }
}
