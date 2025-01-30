using GreenLebanon.Taxi.Shared.Requests;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GreenLebanon.Taxi.Web.Infrastructure.Gateways
{
    public interface IClientGateway
    {
        Task<int> AddNewClientAsync(AddClientRequest request, CancellationToken cancellationToken);
        Task<AddClientRequest> GetClientAsync(string id);
        Task<List<AddClientRequest>> GetAllClientsAsync();
    }
}
