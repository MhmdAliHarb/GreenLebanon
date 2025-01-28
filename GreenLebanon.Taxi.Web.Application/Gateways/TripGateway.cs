using Blazored.LocalStorage;
using GreenLebanon.Taxi.Shared.Requests;
using GreenLebanon.Taxi.Web.Infrastructure.Gateways;
using System.Net.Http.Headers;
using System.Net.Http.Json;

namespace GreenLebanon.Taxi.Web.Application.Gateways
{
    public class TripGateway : ITripGateway
    {
        private readonly HttpClient _httpClient;
        private readonly ILocalStorageService _localStorageService;

        public TripGateway(HttpClient client, ILocalStorageService localStorageService)
        {
            _httpClient = client;

            _localStorageService = localStorageService;
        }

        private async Task<string> GetAccessTokenAsync()
        {
            return await _localStorageService.GetItemAsync<string>("AuthToken");
        }

        public async Task<int>  AddNewTripAsync(AddTripRequest request, CancellationToken cancellationToken)
        {
            string token = await GetAccessTokenAsync();

            if (!string.IsNullOrEmpty(token))
            {
                _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
            }

            var result = await _httpClient.PostAsJsonAsync<AddTripRequest>("/api/Trips", request, cancellationToken);

            return int.Parse(await result.Content.ReadAsStringAsync());
        }

        public async Task<List<TripForView>> GetTripsAsync(int? tripId, string status)
        {
            throw new NotImplementedException();
        }

        public async Task<List<TripForView>> GetAllTripsAsync(string userId)
        {
            var result = await _httpClient.GetAsync($"/api/all/Trips/{userId}");

            throw new NotImplementedException();

        }
    }
}
