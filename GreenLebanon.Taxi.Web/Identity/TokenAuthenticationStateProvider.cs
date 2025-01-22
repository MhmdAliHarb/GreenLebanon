using Blazored.LocalStorage;
using GreenLebanon.Taxi.Shared.Requests;
using GreenLebanon.Taxi.Web.Models;
using Microsoft.AspNetCore.Components.Authorization;
using System.IdentityModel.Tokens.Jwt;
using System.Net.Http.Headers;
using System.Net.Http.Json;
using System.Security.Claims;
using System.Text.Json;
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
                return new AuthenticationState(new ClaimsPrincipal(new ClaimsIdentity()));
            }

            try
            {
                var handler = new JwtSecurityTokenHandler();
                var jwtSecurityToken = handler.ReadJwtToken(token);

                var expClaim = jwtSecurityToken.Claims.FirstOrDefault(x => x.Type == "exp")?.Value;
                if (expClaim != null && DateTime.UtcNow > DateTimeOffset.FromUnixTimeSeconds(long.Parse(expClaim)).UtcDateTime)
                {
                    throw new UnauthorizedAccessException();
                }

                var userId = jwtSecurityToken.Claims.FirstOrDefault(x => x.Type == "sub")?.Value;
                _httpClient.DefaultRequestHeaders.Add("Authorization", $"Bearer {token}");
                var userResponseMessage = await _httpClient.GetAsync($"/api/Users/{userId}");
                userResponseMessage.EnsureSuccessStatusCode();

                var userResponseMessageAsString = await userResponseMessage.Content.ReadAsStringAsync();
                var options = new JsonSerializerOptions
                {
                    PropertyNamingPolicy = JsonNamingPolicy.CamelCase,
                    PropertyNameCaseInsensitive = true
                };
                var jsonResponse = JsonSerializer.Deserialize<ApplicationUserForView>(userResponseMessageAsString, options);

                var identity = new ClaimsIdentity(new[]
                {
            new Claim(ClaimTypes.Name, jsonResponse.FirstName),
            new Claim(ClaimTypes.Email, jsonResponse.Email),
            new Claim(ClaimTypes.Role, jsonResponse.Role ?? "Guest")
        }, "jwt");

                var user = new ClaimsPrincipal(identity);
                return new AuthenticationState(user);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error in GetAuthenticationStateAsync: {ex.Message}");
                throw new UnauthorizedAccessException(ex.ToString());
            }
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
