using GreenLebanon.Taxi.ApplicationCore.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GreenLebanon.Taxi.ApplicationCore.Repositories
{
    public interface IClientRepository
    {
        Task<int> AddClientAsync(Client client);

        Task<IQueryable<Client>> GetAllClientsAsync(int? clientId = null);
    }
}
