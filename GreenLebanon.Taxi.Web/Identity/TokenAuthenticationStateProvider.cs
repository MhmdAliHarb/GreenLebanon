using Blazored.LocalStorage;
using GreenLebanon.Taxi.Shared.Requests;
using GreenLebanon.Taxi.Web.Models;
using Microsoft.AspNetCore.Components.Authorization;
using System.IdentityModel.Tokens.Jwt;
using System.Net.Http.Headers;
using System.Net.Http.Json;
using System.Security.Claims;
using System.Text.Json.Nodes;

namespace GreenLebanon.Taxi.Web.Identity
{
    public class TokenAuthenticationStateProvider : AuthenticationStateProvider
    {
        private readonly ILocalStorageService _localStorageService;
        private readonly HttpClient _httpClient;

        public TokenAuthenticationStateProvider(ILocalStorageService localStorageService, HttpClient httpClient)
        {
            _localStorageService = localStorageService;
            _httpClient = httpClient;
        }

        public override async Task<AuthenticationState> GetAuthenticationStateAsync()
        {
            var token = await _localStorageService.GetItemAsync<string>("AuthToken");

            if (string.IsNullOrWhiteSpace(token))
            {
                var user = new ClaimsPrincipal(new ClaimsIdentity());

                return new AuthenticationState(user);
            }

            //var claims = ParseClaimsFromJwt(token); 
            var handler = new JwtSecurityTokenHandler();
            var jwtSecurityToken = handler.ReadJwtToken(token);

            return new AuthenticationState(new ClaimsPrincipal(new ClaimsIdentity()));
        }

        public async Task<UserLoginResponse> Login(LoginDto loginDto)
        {
            try
            {
                var loginResponse = await _httpClient.PostAsJsonAsync("/account/login", loginDto);

                if (loginResponse.IsSuccessStatusCode)
                {
                    var responsAsString = await loginResponse.Content.ReadAsStringAsync();

                    var jsonResponse = JsonNode.Parse(responsAsString);

                    var token = jsonResponse["access_token"]?.ToString();

                    _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);

                    await _localStorageService.SetItemAsStringAsync("AuthToken", data: token);

                    NotifyAuthenticationStateChanged(GetAuthenticationStateAsync());

                    return new UserLoginResponse() { Token = token };
                }

                return null;
            }
            catch (Exception)
            {

                throw;
            }
        }
    }
}
