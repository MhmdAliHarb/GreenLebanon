using GreenLebanon.Taxi.ApplicationCore.Entities;
using GreenLebanon.Taxi.ApplicationCore.Repositories;

namespace GreenLebanon.Taxi.Application.Services
{
    public class ClientService(IClientRepository clientRepository)
    {
        private readonly IClientRepository clientRepository = clientRepository;

        public async Task<IQueryable<ApplicationUser>> GetAllClientsAsync()
        {
            return await clientRepository.GetAllClientsAsync();
        }

    }
}
