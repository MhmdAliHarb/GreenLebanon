using GreenLebanon.Taxi.ApplicationCore.Entities;
using GreenLebanon.Taxi.ApplicationCore.Repositories;
using GreenLebanon.Taxi.Shared.Requests;

namespace GreenLebanon.Taxi.Application.Services
{
    public class ClientService( IClientRepository clientRepository )
    {
        private readonly IClientRepository clientRepository = clientRepository;

        public async Task<int> AddNewClientAsync( AddClientRequest request )
        {
            return await clientRepository.AddClientAsync(new ApplicationCore.Entities.Client()
            {
                FirstName = request.FirstName,
                LastName = request.LastName,
                PhoneNumber = request.PhoneNumber,
                Address = request.Address,


            });
        }

        public async Task<IQueryable<Client>> GetAllClientsAsync()
        {
            return await clientRepository.GetAllClientsAsync();
        }

    }
}
