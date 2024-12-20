﻿using GreenLebanon.Taxi.Shared.Requests;
using GreenLebanon.Taxi.Web.Infrastructure.Gateways;
using System.Net.Http.Json;

namespace GreenLebanon.Taxi.Web.Application.Gateways
{
    public class ClientGateway : IClientGateway
    {
        private readonly HttpClient httpClient;

        public ClientGateway(HttpClient httpClient)
        {
            this.httpClient = httpClient;
        }
        public async Task<int> AddNewClientAsync(AddClientRequest request, CancellationToken cancellationToken)
        {
            var result = await httpClient.PostAsJsonAsync<AddClientRequest>("api/Clients", request, cancellationToken);

            return int.Parse(await result.Content.ReadAsStringAsync());
        }
        
    }
}
