using Blazored.LocalStorage;
using GreenLebanon.Taxi.Shared.Requests;
using GreenLebanon.Taxi.Web.Infrastructure.Gateways;
using System.Net.Http.Headers;
using System.Net.Http.Json;

namespace GreenLebanon.Taxi.Web.Application.Gateways
{
    public class ClientGateway : IClientGateway
    {
        private readonly HttpClient httpClient;
        private readonly ILocalStorageService localStorageService;

        public ClientGateway( HttpClient client, ILocalStorageService localStorageService )
        {
            this.httpClient = client;

            this.localStorageService = localStorageService;
        }
        private async Task<string> GetAccessTokenAsync()
        {
            return await localStorageService.GetItemAsync<string>("AuthToken");
        }
        public async Task<int> AddNewClientAsync(AddClientRequest request, CancellationToken cancellationToken)
        {
            var result = await httpClient.PostAsJsonAsync<AddClientRequest>("api/Clients", request, cancellationToken);

            return int.Parse(await result.Content.ReadAsStringAsync());
        }

        public async Task<List<AddClientRequest>> GetAllClientsAsync()
        {
            string token = await GetAccessTokenAsync();
            if ( !string.IsNullOrEmpty(token) )
            {
                httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
            }
            var result = await httpClient.GetAsync("/api/Clients");
            if ( result.IsSuccessStatusCode )
            {
                return await result.Content.ReadFromJsonAsync<List<AddClientRequest>>();
            }
            throw new Exception("Error in retriving Clients");
        }

        public async Task<AddClientRequest> GetClientAsync( string id )
        {
            string token = await GetAccessTokenAsync();

            if ( !string.IsNullOrEmpty(token) )
            {
                httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
            }
            var result = await httpClient.GetAsync($"api/Clients/{id}");
            if ( result.IsSuccessStatusCode )
            {
                return await result.Content.ReadFromJsonAsync<AddClientRequest>();
            }
            throw new Exception("Error retrieving client data");
        }
    }
}
