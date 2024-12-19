using GreenLebanon.Taxi.ApplicationCore.Repositories;
using GreenLebanon.Taxi.Shared.Requests;

namespace GreenLebanon.Taxi.Application.Services
{
    public class ClientService(IClientRepository clientRepository)
    {
        private readonly IClientRepository clientRepository = clientRepository;

        public async Task<int> AddNewClientAsync(AddClientRequest request)
        {
            return await clientRepository.AddClientAsync(new ApplicationCore.Entities.Client()
            {
                FirstName = request.FirstName,
                LastName = request.LastName,
            });
        }
    }
}
