using Blazored.LocalStorage;
using GreenLebanon.Taxi.ApplicationCore.Entities;
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

        public Task<List<Trip>> GetTripsAsync(int? TripId, string status = null)
        {
            throw new NotImplementedException();
        }

        private async Task<string> GetAccessTokenAsync()
        {
            return await _localStorageService.GetItemAsync<string>("AuthToken");
        }

        async Task<int> ITripGateway.AddNewTripAsync(AddTripRequest request, CancellationToken cancellationToken)
        {
            string token = await GetAccessTokenAsync();

            if (!string.IsNullOrEmpty(token))
            {
                _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
            }

            var result = await _httpClient.PostAsJsonAsync<AddTripRequest>("/api/Trips", request, cancellationToken);

            return int.Parse(await result.Content.ReadAsStringAsync());
        }

        //async Task<List<Trip>> ITripGateway.GetAllTripsAsync()
        //{
        //    if (!string.IsNullOrEmpty(token))
        //    {
        //        _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
        //    }
        //    var result = await _httpClient.GetAsync("/api/Trips");
        //    if (result.IsSuccessStatusCode)
        //    {
        //        return await result.Content.ReadFromJsonAsync<List<Trip>>();
        //    }
        //    throw new Exception("Error retrieving client data");
        //}
    }
}
