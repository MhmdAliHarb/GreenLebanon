using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace GreenLebanon.Taxi.Shared.Requests
{
    public class UserLoginResponse
    {
        [JsonPropertyName("token")]
        public string Token { get; set; }

        [JsonPropertyName("roles")]
        public List<string> Roles { get; set; }
    }
}
