using GreenLebanon.Taxi.ApplicationCore.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GreenLebanon.Taxi.ApplicationCore.Repositories
{
    public interface ITripRepository
    {
        Task<int> AddTripAsync(Trip trip);

        Task<Trip> GetTripByIdAsync(int tripId, string userId);

        Task<IQueryable<Trip>> GetAllTripsAsync(string userId);
     }
}
