using GreenLebanon.Taxi.ApplicationCore.Entities;
using GreenLebanon.Taxi.ApplicationCore.Repositories;
using GreenLebanon.Taxi.Shared.Requests;

namespace GreenLebanon.Taxi.Application.Services
{
    public class ClientService(IClientRepository clientRepository)
    {
        private readonly IClientRepository clientRepository = clientRepository;

        public async Task<int> AddNewClientAsync(AddClientRequest request)
        {
            return await clientRepository.AddClientAsync(new Client()
            {
                ApplicationUser = new ApplicationUser()
                {
                    FirstName = request.FirstName,
                    LastName = request.LastName,
                    PhoneNumber = request.PhoneNumber,
                }
            });
        }

        public async Task<IQueryable<Client>> GetAllClientsAsync()
        {
            return await clientRepository.GetAllClientsAsync();
        }

    }
}
