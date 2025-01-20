using GreenLebanon.Taxi.ApplicationCore.Entities;
using GreenLebanon.Taxi.ApplicationCore.Repositories;
using GreenLebanon.Taxi.Shared.Requests;


namespace GreenLebanon.Taxi.Application.Services
{
    public class TripService(ITripRepository tripRepository, IDriverRepository driverRepository)
    {
        private readonly ITripRepository tripRepository = tripRepository;
        
        private readonly IDriverRepository driverRepository = driverRepository;

        public async Task<int> AddNewTripAsync(AddTripRequest request)
        {
            var availableDriver = await driverRepository.GetAvailableDriverAsync(request.Region);

            if (availableDriver == default)
            {
                throw new InvalidProgramException("No driver available at this time");
            }

            return await tripRepository.AddTripAsync(new Trip()
            {
                Destination = request.Destination,
                StartingPoint = request.StartingPoint,
                Region = request.Region,
                Timing = TimeOnly.FromDateTime(request.Timing),
                DriverId = availableDriver.UserId,
                Status = Status.Pending,
                ClientId = request.ClientId,
            });
        }

        public async Task<IQueryable<Trip>> GetAllTripsAsync(int? tripId = null)
        {
            return await tripRepository.GetAllTripsAsync(tripId);
        }
    }
}