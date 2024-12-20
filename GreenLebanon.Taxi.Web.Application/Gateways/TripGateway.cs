using GreenLebanon.Taxi.Shared.Requests;
using GreenLebanon.Taxi.Web.Infrastructure.Gateways;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Json;
using System.Text;
using System.Threading.Tasks;

namespace GreenLebanon.Taxi.Web.Application.Gateways
{
    public class TripGateway : ITripGateway
    {
        private readonly HttpClient httpClient;
        public TripGateway( HttpClient client)
        {
            this.httpClient = client;
        }
        async Task<int> ITripGateway.AddNewTripAsync( AddTripRequest request, CancellationToken cancellationToken )
        {
           var result = await httpClient.PostAsJsonAsync<AddTripRequest>("api/Trips", request, cancellationToken);

            return int.Parse(await result.Content.ReadAsStringAsync());
        }
    }
}
