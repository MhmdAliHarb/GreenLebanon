using GreenLebanon.Taxi.ApplicationCore.Entities;
using GreenLebanon.Taxi.ApplicationCore.Repositories;
using GreenLebanon.Taxi.Shared.Responses;

namespace GreenLebanon.Taxi.Application.Services
{
    public class ClientService(IClientRepository clientRepository)
    {
        private readonly IClientRepository clientRepository = clientRepository;

        public async Task<IEnumerable<ApplicationUser>> GetAllClientsAsync()
        {
            return (await clientRepository.GetClientsAsync()).ToList();
        }

        public async Task<ApplicationUser> GetClientByIdAsync(string clientId)
        {
            return (await clientRepository.GetClientsAsync(clientId)).First();
        }

        public async Task<IEnumerable<ClientTripsForView>> GetClientTripsAsync(string clientId)
        {
            return (await clientRepository.GetClientTripsAsync(clientId))
                .Select(x => new ClientTripsForView()
                {
                    Destination = x.Destination,
                    DriverName = x.Driver.ApplicationUser.FirstName + " " + x.Driver.ApplicationUser.LastName,
                    Region = x.Region,
                    StartingPoint = x.StartingPoint,
                    Timing = x.Timing,
                    TripStatus = x.Status.ToString()
                });
        }
    }
}
