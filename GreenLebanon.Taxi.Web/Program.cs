using GreenLebanon.Taxi.Web.Application.Gateways;
using GreenLebanon.Taxi.Web.Identity;
using GreenLebanon.Taxi.Web.Infrastructure.Gateways;
using GreenLebanon.Taxi.Web.Pages.Auth;
using Microsoft.AspNetCore.Components.Authorization;
using Microsoft.AspNetCore.Components.Web;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using System.Security.Cryptography.Xml;
using System.Security.Principal;

namespace GreenLebanon.Taxi.Web
{
    public class Program {
        public static async Task Main( string[] args ) {
            var builder = WebAssemblyHostBuilder.CreateDefault(args);
            builder.RootComponents.Add<App>("#app");
            builder.RootComponents.Add<HeadOutlet>("head::after");
            builder.Services.AddAuthorizationCore();
            builder.Services.AddScoped<IAuthenticationGateway, AuthenticationGateway>();  
            builder.Services.AddHttpClient<IAuthenticationGateway, AuthenticationGateway>(
               client =>
               {
                   client.BaseAddress = new Uri("https://localhost:7241/");
               });

            builder.Services.AddScoped<IClientGateway, ClientGateway>();
            builder.Services.AddHttpClient<IClientGateway, ClientGateway>(client =>
            {
                client.BaseAddress = new Uri("https://localhost:7241/");
            });
            builder.Services.AddScoped<ITripGateway, TripGateway>();
            builder.Services.AddHttpClient<ITripGateway, TripGateway>(trip =>
            {
                trip.BaseAddress = new Uri("https://localhost:7241/");
            });
            builder.Services.AddTransient<CookieHandler>();
            builder.Services.AddScoped(sp => new HttpClient { BaseAddress = new Uri(builder.HostEnvironment.BaseAddress) });
           builder.Services.AddScoped(sp => (IAccountManagement)sp.GetRequiredService<AuthenticationStateProvider>());
            builder.Services.AddScoped<AuthenticationStateProvider, CookiesAuthenticationStateProvider>();
            builder.Services.AddHttpClient("Auth", options =>
                  options.BaseAddress = new Uri("https://localhost:7003")
            ).AddHttpMessageHandler<CookieHandler>();


            await builder.Build().RunAsync();
        }
    }
}
