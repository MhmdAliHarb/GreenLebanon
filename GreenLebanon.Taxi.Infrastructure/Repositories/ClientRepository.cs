using GreenLebanon.Taxi.ApplicationCore.Entities;
using GreenLebanon.Taxi.ApplicationCore.Repositories;
using GreenLebanon.Taxi.Infrastructure.Data;

namespace GreenLebanon.Taxi.Infrastructure.Repositories
{
    public class ClientRepository : IClientRepository
    {
        private readonly AppDbContext appDbContext;

        public ClientRepository(AppDbContext appDbContext)
        {
            this.appDbContext = appDbContext;
        }

        public async Task<int> AddClientAsync(Client client)
        {
            appDbContext.Clients.Add(client);
            return await appDbContext.SaveChangesAsync();
        }

        public async Task<IQueryable<Client>> GetAllClientsAsync(string clientId = null)
        {
            if (!string.IsNullOrEmpty(clientId))
            {
                appDbContext.Clients.Where(x => x.ApplicationUserId == clientId);
            }

            return appDbContext.Clients.AsQueryable();
        }

    }
}
