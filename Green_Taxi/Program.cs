using Green_Taxi.Identity;
using Microsoft.AspNetCore.Components.Authorization;
using Microsoft.AspNetCore.Components.Web;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;

namespace Green_Taxi {
    public class Program {
        public static async Task Main( string[] args ) {
            var builder = WebAssemblyHostBuilder.CreateDefault(args);
            builder.RootComponents.Add<App>("#app");
            builder.RootComponents.Add<HeadOutlet>("head::after");
            builder.Services.AddAuthorizationCore();
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
