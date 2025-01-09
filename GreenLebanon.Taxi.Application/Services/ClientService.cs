using GreenLebanon.Taxi.ApplicationCore.Entities;
using GreenLebanon.Taxi.ApplicationCore.Repositories;

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

    }
}
