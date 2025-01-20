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
    class DriverGateway : IDriverGateway
    {
        private readonly HttpClient _httpClient;
        private readonly ILocalStorageService _localStorageService;

        private string token = string.Empty;

        public DriverGateway( HttpClient client, ILocalStorageService localStorageService )
        {
            _httpClient = client;

            _localStorageService = localStorageService;

            token = _localStorageService.GetItemAsync<string>("AuthToken").Result ?? string.Empty;
        }

        public async Task<int> AddNewDriverAsync( RegistrationDto request)
        {
            var result = await _httpClient.PostAsJsonAsync<RegistrationDto>("api/Drivers", request);

            return int.Parse(await result.Content.ReadAsStringAsync());
        }

        public Task<int> DeleteDriverAsync( RegistrationDto request )
        {
            throw new NotImplementedException();
        }

        //public async Task<List<GreenLebanon.Taxi.Shared.Requests.Driver>> GetAllDriversAsync()
        //{
        //    if ( !string.IsNullOrEmpty(token) )
        //    {
        //        httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
        //    }
        //    var result = await httpClient.GetAsync("/api/Drivers");
        //    if ( result.IsSuccessStatusCode )
        //    {
        //        return await result.Content.ReadFromJsonAsync<List<Shared.Requests.Driver>>();
        //    }
        //    throw new Exception("Error retrieving client data");
        //}
    }
}
