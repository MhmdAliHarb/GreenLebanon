using Blazored.LocalStorage;
using GreenLebanon.Taxi.Shared.Requests;
using GreenLebanon.Taxi.Web.Infrastructure.Gateways;
using System.Diagnostics.CodeAnalysis;
using System.Net.Http.Json;
using System.Text.Json;

namespace GreenLebanon.Taxi.Web.Application.Gateways
{
    public class AuthenticationGateway : IAuthenticationGateway
    {
        private readonly ILocalStorageService localStorageService;
        public readonly HttpClient _httpClient;
        public AuthenticationGateway(ILocalStorageService localStorageService, HttpClient httpClient)
        {
            this.localStorageService = localStorageService;
            _httpClient = httpClient;
        }
        public async Task<AuthResult> LoginAsync(LoginDto model)
        {
            try
            {
                var result = await _httpClient.PostAsJsonAsync("/account/login", model);

                if (result.IsSuccessStatusCode)
                {
                    var responseAsString = await result.Content.ReadAsStringAsync();

                    var token = JsonSerializer.Deserialize<UserLoginResponse>(responseAsString, JsonSerializerOptions.Default);

                    await localStorageService.SetItemAsync("AuthToken", token.Token);

                    return new AuthResult { Succeeded = true };
                }

                return new AuthResult
                {
                    Succeeded = false,
                };
            }
            catch (Exception ex)
            {
                return new AuthResult
                {
                    Succeeded = false,
                    ErrorList = ex.ToString()
                };
            }
        }

        public async Task<AuthResult> RegisterAsync(RegistrationDto model)
        {
            try
            {
                var result = await _httpClient.PostAsJsonAsync("/account/register", model);

                if (result.IsSuccessStatusCode)
                {
                    return new AuthResult { Succeeded = true };
                }

                return new AuthResult
                {
                    Succeeded = false,
                };
            }
            catch (Exception ex) 
            {
                return new AuthResult
                {
                    Succeeded = false,
                    ErrorList = ex.ToString()
                };
            }
        }
    }
}

