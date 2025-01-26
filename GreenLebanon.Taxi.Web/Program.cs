using Blazored.LocalStorage;
using GreenLebanon.Taxi.Web.Application.Gateways;
using GreenLebanon.Taxi.Web.Identity;
using GreenLebanon.Taxi.Web.Infrastructure.Gateways;
using Microsoft.AspNetCore.Components.Authorization;
using Microsoft.AspNetCore.Components.Web;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;

namespace GreenLebanon.Taxi.Web
{
    public class Program
    {
        public static async Task Main(string[] args)
        {
            var builder = WebAssemblyHostBuilder.CreateDefault(args);

            builder.RootComponents.Add<App>("#app");

            builder.RootComponents.Add<HeadOutlet>("head::after");

            builder.Services.AddScoped(sp => new HttpClient()
            {
                BaseAddress = new Uri("https://localhost:44383")
            });

            builder.Services.AddScoped<IAuthenticationGateway, AuthenticationGateway>();
            builder.Services.AddScoped<IClientGateway, ClientGateway>();
            builder.Services.AddScoped<ITripGateway, TripGateway>();
            builder.Services.AddBlazoredLocalStorageAsSingleton();
            builder.Services.AddAuthorizationCore();
            builder.Services.AddScoped<AuthenticationStateProvider, TokenAuthenticationStateProvider>();
            builder.Services.AddScoped<TokenAuthenticationStateProvider>();

            await builder.Build().RunAsync();
        }
    }
}
