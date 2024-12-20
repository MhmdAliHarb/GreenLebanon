using GreenLebanon.Taxi.ApplicationCore.Repositories;
using GreenLebanon.Taxi.Shared.Requests;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


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
    }
}