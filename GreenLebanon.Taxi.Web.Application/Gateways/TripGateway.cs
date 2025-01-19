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
        private readonly HttpClient httpClient;
        private readonly ILocalStorageService localStorageService;

        private string token = string.Empty;

        public TripGateway( HttpClient client, ILocalStorageService localStorageService )
        {
            this.httpClient = client;

            this.localStorageService = localStorageService;

            token = localStorageService.GetItemAsync<string>("authToken").Result ?? string.Empty;
        }

        public Task<List<Trip>> GetTripsAsync( int? TripId, string status = null )
        {
            throw new NotImplementedException();
        }

        async Task<int> ITripGateway.AddNewTripAsync( AddTripRequest request, CancellationToken cancellationToken )
        {
            if ( !string.IsNullOrEmpty(token) )
            {
                httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
            }

            var result = await httpClient.PostAsJsonAsync<AddTripRequest>("/api/Trips", request, cancellationToken);

            return int.Parse(await result.Content.ReadAsStringAsync());
        }

        async Task<List<Trip>> ITripGateway.GetAllTripsAsync()
        {
            if ( !string.IsNullOrEmpty(token) )
            {
                httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
            }
            var result = await httpClient.GetAsync("/api/Trips");
            if ( result.IsSuccessStatusCode )
            {
                return await result.Content.ReadFromJsonAsync<List<Trip>>();
            }
            throw new Exception("Error retrieving client data"); 
        }
    }
}
