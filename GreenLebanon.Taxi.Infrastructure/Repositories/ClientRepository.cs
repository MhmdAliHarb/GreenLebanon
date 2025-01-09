using GreenLebanon.Taxi.ApplicationCore.Entities;
using GreenLebanon.Taxi.ApplicationCore.Repositories;
using GreenLebanon.Taxi.Infrastructure.Data;

namespace GreenLebanon.Taxi.Infrastructure.Repositories
{
    public class ClientRepository(AppDbContext appDbContext) : IClientRepository
    {
        private readonly AppDbContext appDbContext = appDbContext;

        public async Task<IQueryable<ApplicationUser>> GetClientsAsync(string clientId = null)
        {
            var result = appDbContext.ApplicationUsers
                    .Join(appDbContext.AspNetUserRoles, x => x.Id, c => c.UserId, (x, c) => x);

            if (!string.IsNullOrEmpty(clientId))
            {
                result = result.Where(x => x.Id == clientId);
            }

            return result;
        }

    }
}
