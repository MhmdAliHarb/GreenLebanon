using Blazored.LocalStorage;
using GreenLebanon.Taxi.ApplicationCore.Entities;
using GreenLebanon.Taxi.Shared.Requests;
using GreenLebanon.Taxi.Web.Infrastructure.Gateways;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Net.Http.Json;
using System.Text;
using System.Threading.Tasks;

namespace GreenLebanon.Taxi.Web.Application.Gateways
{
    public class TripGateway : ITripGateway
    {
        private readonly HttpClient httpClient;
        private readonly ILocalStorageService localStorageService;

        private string token = string.Empty;

        public TripGateway(HttpClient client, ILocalStorageService localStorageService)
        {
            this.httpClient = client;

            this.localStorageService = localStorageService;

            token = localStorageService.GetItemAsync<string>("authToken").Result ?? string.Empty;
        }
        async Task<int> ITripGateway.AddNewTripAsync(AddTripRequest request, CancellationToken cancellationToken)
        {
            if (!string.IsNullOrEmpty(token))
            {
                httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
            }

            var result = await httpClient.PostAsJsonAsync<AddTripRequest>("api/Trips", request, cancellationToken);

            return int.Parse(await result.Content.ReadAsStringAsync());
        }

    }
}
