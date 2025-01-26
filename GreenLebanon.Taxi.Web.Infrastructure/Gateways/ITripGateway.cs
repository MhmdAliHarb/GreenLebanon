using GreenLebanon.Taxi.ApplicationCore.Entities;
using GreenLebanon.Taxi.Shared.Requests;

namespace GreenLebanon.Taxi.Web.Infrastructure.Gateways
{
    public interface ITripGateway
    {
        Task<int> AddNewTripAsync(AddTripRequest request, CancellationToken cancellationToken);
        Task<List<TripForView>> GetTripsAsync(int? tripId, string status = null);
        Task<List<TripForView>> GetAllTripsAsync(string userId);
    }
}
