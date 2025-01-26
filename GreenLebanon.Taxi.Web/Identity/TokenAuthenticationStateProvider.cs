using Blazored.LocalStorage;
using GreenLebanon.Taxi.Shared.Requests;
using Microsoft.AspNetCore.Components.Authorization;
using System.IdentityModel.Tokens.Jwt;
using System.Net.Http.Headers;
using System.Net.Http.Json;
using System.Security.Claims;

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
                return new AuthenticationState(new ClaimsPrincipal(new ClaimsIdentity()));
            }

            var handler = new JwtSecurityTokenHandler();

            var jwtSecurityToken = handler.ReadJwtToken(token);

            var expClaim = jwtSecurityToken.Claims.FirstOrDefault(x => x.Type == "exp")?.Value;

            if (expClaim != null && DateTime.UtcNow > DateTimeOffset.FromUnixTimeSeconds(long.Parse(expClaim)).UtcDateTime)
            {
                throw new UnauthorizedAccessException();
            }

            var userId = jwtSecurityToken.Claims.FirstOrDefault(x => x.Type == "sub")?.Value;

            var jsonResponse = await GetUserInformation(userId, token);

            return new AuthenticationState(new ClaimsPrincipal(new ClaimsIdentity(new[]
            {
                new Claim(ClaimTypes.Name, jsonResponse.FirstName),
                new Claim(ClaimTypes.Surname, jsonResponse.LastName),
                new Claim(ClaimTypes.Email, jsonResponse.Email),
                new Claim(ClaimTypes.Role, jsonResponse.Role)
            }, "jwt")));
        }

        private async Task<ApplicationUserForView> GetUserInformation(string userId, string token)
        {
            // Add the JWT token to the Authorization header
            _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);

            try
            {
                // Send a GET request
                HttpResponseMessage response = await _httpClient.GetAsync($"/api/Users/{userId}");

                // Ensure the request was successful
                response.EnsureSuccessStatusCode();

                // Read and process the response content
                ApplicationUserForView responseContent = await response.Content.ReadFromJsonAsync<ApplicationUserForView>();

                return responseContent;
            }
            catch (HttpRequestException ex)
            {
                throw ex;
            }
        }
    }
}
