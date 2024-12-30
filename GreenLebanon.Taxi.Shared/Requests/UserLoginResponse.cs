using System.Collections.Generic;

namespace GreenLebanon.Taxi.Shared.Requests
{
    public class UserLoginResponse
    {
        public string Token { get; set; }

        public List<string> Roles { get; set; }
    }
}
