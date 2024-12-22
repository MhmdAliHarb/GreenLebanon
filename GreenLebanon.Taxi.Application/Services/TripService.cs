using GreenLebanon.Taxi.ApplicationCore.Entities;
using GreenLebanon.Taxi.ApplicationCore.Repositories;
using GreenLebanon.Taxi.Shared.Requests;


namespace GreenLebanon.Taxi.Application.Services
{
    public class TripService( ITripRepository tripRepository )
    {

        private readonly ITripRepository tripRepository = tripRepository;

        public async Task<int> AddNewTripAsync( AddTripRequest request )
        {
            return await tripRepository.AddTripAsync(new ApplicationCore.Entities.Trip()
            {
                Destination = request.Destination,
                StartingPoint = request.StartingPoint,
                Region = request.Region,
                Timing = request.Timing
            });
        }
        public async Task<IQueryable<Trip>> GetAllTripsAsync( int? TripId = null )
        {
            return await tripRepository.GetAllTripsAsync(TripId);
        }


    }
}