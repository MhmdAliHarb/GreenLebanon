using Blazored.LocalStorage;
using Microsoft.AspNetCore.Components.Authorization;
using System.Security.Claims;

namespace GreenLebanon.Taxi.Web.Identity
{
    public class TokenAuthenticationStateProvider : AuthenticationStateProvider
    {
        private readonly ILocalStorageService _localStorageService;

        public TokenAuthenticationStateProvider( ILocalStorageService localStorageService )
        {
            localStorageService = _localStorageService;
        }

        public override async Task<AuthenticationState> GetAuthenticationStateAsync()
        {
            var token = await _localStorageService.GetItemAsync<string>("AuthToken");
            if ( string.IsNullOrWhiteSpace(token))
            {
                return new AuthenticationState(new ClaimsPrincipal(new  ClaimsIdentity()));
            }
            var claims = ParseClaimsFromJwt(token); 
        }
    }
}
