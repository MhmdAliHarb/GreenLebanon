using GreenLebanon.Taxi.ApplicationCore.Entities;
using GreenLebanon.Taxi.ApplicationCore.Repositories;
using GreenLebanon.Taxi.Infrastructure.Data;

namespace GreenLebanon.Taxi.Infrastructure.Repositories
{
    public class ClientRepository(AppDbContext appDbContext) : IClientRepository
    {
        private readonly AppDbContext appDbContext = appDbContext;

        public async Task<IQueryable<ApplicationUser>> GetAllClientsAsync(string clientId = null)
        {
            if (!string.IsNullOrEmpty(clientId))
            {
                appDbContext.ApplicationUsers.Where(x => x.Id == clientId);
            }

            return appDbContext.ApplicationUsers.AsQueryable();
        }

    }
}
