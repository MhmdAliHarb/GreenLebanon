using GreenLebanon.Taxi.ApplicationCore.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GreenLebanon.Taxi.ApplicationCore.Repositories
{
    public interface IDriverRepository
    {
        Task<int> AddDriverAsync( Driver driver );
        Task<IQueryable<Driver>> GetAllDriversAsync(int? DriverId = null);
    }
}
