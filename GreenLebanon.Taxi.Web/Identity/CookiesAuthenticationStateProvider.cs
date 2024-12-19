using GreenLebanon.Taxi.Web.Models;
using Microsoft.AspNetCore.Components.Authorization;
using System.ComponentModel;
using System.Net.Http.Json;
using System.Security.Claims;
using System.Text;
using System.Text.Json;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace GreenLebanon.Taxi.Web.Identity {

    public class CookiesAuthenticationStateProvider( IHttpClientFactory httpCLientFactory ) : AuthenticationStateProvider, IAccountManagement {

        private readonly HttpClient _httpClient = httpCLientFactory.CreateClient("Auth");
        private bool _authenticated;
        private readonly ClaimsPrincipal _unauthenticated = new ClaimsPrincipal(new ClaimsIdentity());
        private readonly JsonSerializerOptions _jsonSerializerOptions = new() {
            PropertyNamingPolicy = JsonNamingPolicy.CamelCase
        };

        public override async Task<AuthenticationState> GetAuthenticationStateAsync()
        {

            _authenticated = false;
            var user = _unauthenticated;

            try
            {
                var UserResponse = await _httpClient.GetAsync("manage/info");
                UserResponse.EnsureSuccessStatusCode();
                var UserJson = await UserResponse.Content.ReadAsStringAsync();
                var UserInfo = JsonSerializer.Deserialize<User>(UserJson, _jsonSerializerOptions);

                if (UserInfo is not null)
                {
                    _authenticated = true;
                    var claims = new List<Claim> {
                        new(ClaimTypes.Name,UserInfo.Email)
                        ,new(ClaimTypes.Email, UserInfo.Email)
                    };
                    var ClaimsIdentity = new ClaimsIdentity(claims, nameof(CookiesAuthenticationStateProvider));
                    user = new ClaimsPrincipal(ClaimsIdentity);
                }
            }
            catch
            {
                //
            }
            return new AuthenticationState(user);

        }

        public async Task<AuthResult> LoginAsync( LoginModel credentials ) {
            var result = await _httpClient.PostAsJsonAsync("login?useCookies=true",
                new {
                    email = credentials.Email,
                    password = credentials.Password
                });
            if ( result.IsSuccessStatusCode ) {
                NotifyAuthenticationStateChanged(GetAuthenticationStateAsync());
                return new AuthResult { Succeeded = true };
            }
            return new AuthResult { Succeeded = false, ErrorList = ["Invalid Email Or Password"] };
        }
        public async Task<AuthResult> RegisterAsync( string Email, string Password ) {
            string[] DefaultErrors = ["Unknown Error! Prevented Registiration"];
            try {
                var result = await _httpClient.PostAsJsonAsync("register", new {
                    Email,
                    Password
                });
                if ( result.IsSuccessStatusCode ) {
                    return new AuthResult { Succeeded = true };
                }
                var details = await result.Content.ReadAsStringAsync();
                var problemDetails = JsonDocument.Parse(details);

                var Errors = new List<string>();
                var ErrorList = problemDetails.RootElement.GetProperty("errors");
                foreach ( var error in ErrorList.EnumerateObject() ) {

                    if ( error.Value.ValueKind == JsonValueKind.String ) {
                        Errors.Add(error.Value.GetString()!);
                    }
                    else if ( error.Value.ValueKind == JsonValueKind.Array ) {
                        var AllErrors = error.Value
                            .EnumerateArray()
                            .Select(e => e.GetString() ?? string.Empty)
                            .Where(e => !string.IsNullOrEmpty(e));

                        Errors.AddRange(AllErrors);
                    }
                }

                return new AuthResult {
                    Succeeded = false,
                    ErrorList = [.. Errors]
                };
            }
            catch {
            }
            return new AuthResult {
                Succeeded = false,
                ErrorList = [.. DefaultErrors]
            };
        }
        public async Task LogoutAsync() {
            var emptyContent = new StringContent("{}", Encoding.UTF8, "application/json");
            await _httpClient.PostAsync("auth/logout", emptyContent);
            NotifyAuthenticationStateChanged(GetAuthenticationStateAsync());
        }
    }
}
