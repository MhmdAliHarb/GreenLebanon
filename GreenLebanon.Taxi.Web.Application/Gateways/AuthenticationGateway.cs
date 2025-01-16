using GreenLebanon.Taxi.Shared.Requests;
using GreenLebanon.Taxi.Web.Infrastructure.Gateways;
using System.Diagnostics.CodeAnalysis;
using System.Net.Http.Json;
using System.Text.Json;

namespace GreenLebanon.Taxi.Web.Application.Gateways
{
    public class AuthenticationGateway : IAuthenticationGateway
    {
        public readonly HttpClient _httpClient;
        public AuthenticationGateway( HttpClient httpClient )
        {
            httpClient = _httpClient;
        }
        public Task<AuthResult> LoginAsync(LoginDto model )
        {
           throw new NotImplementedException();
        }

        public async Task<AuthResult> RegisterAsync( RegistrationDto model )
        {
            string[] DefaultErrors = ["Unknown Error! Prevented Registiration"];
            try
            {
                var result = await _httpClient.PostAsJsonAsync("account/register", new
                {
                    Email = model.Email,
                    FirstName = model.FirstName,
                    LastName = model.LastName,
                    Password = model.Password,
                    ConfirmPassword = model.ConfirmPassword,
                    MobileNumber = model.MobileNumber,
                    Location = model.Location,
                    UserType = 2
                });
                if ( result.IsSuccessStatusCode )
                {
                    return new AuthResult { Succeeded = true };
                }
                var details = await result.Content.ReadAsStringAsync();
                var problemDetails = JsonDocument.Parse(details);

                var Errors = new List<string>();
                var ErrorList = problemDetails.RootElement.GetProperty("errors");
                foreach ( var error in ErrorList.EnumerateObject() )
                {

                    if ( error.Value.ValueKind == JsonValueKind.String )
                    {
                        Errors.Add(error.Value.GetString()!);
                    }
                    else if ( error.Value.ValueKind == JsonValueKind.Array )
                    {
                        var AllErrors = error.Value
                            .EnumerateArray()
                            .Select(e => e.GetString() ?? string.Empty)
                            .Where(e => !string.IsNullOrEmpty(e));

                        Errors.AddRange(AllErrors);
                    }
                }
                return new AuthResult
                {
                    Succeeded = false,
                    ErrorList = [.. Errors]
                };
            }
            catch
            {
            }
            return new AuthResult
            {
                Succeeded = false,
                ErrorList = [.. DefaultErrors]
            };
        }
    }
}
  
