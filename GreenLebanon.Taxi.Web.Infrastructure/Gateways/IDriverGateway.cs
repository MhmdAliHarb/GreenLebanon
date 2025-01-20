using GreenLebanon.Taxi.ApplicationCore.Entities;
using GreenLebanon.Taxi.Shared.Requests;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GreenLebanon.Taxi.Web.Infrastructure.Gateways
{
  public  interface IDriverGateway
    {
        Task<int> AddNewDriverAsync( RegistrationDto request);
       Task<int> DeleteDriverAsync( RegistrationDto request);
       // Task<List<Driver>> GetAllDriversAsync();
    }
}
