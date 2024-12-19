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

        public async Task<IQueryable<Client>> GetAllClientsAsync(int? clientId = null)
        {
            return appDbContext.Clients.Where(x => clientId.HasValue && x.Id == clientId.Value);
        }
    }
}
