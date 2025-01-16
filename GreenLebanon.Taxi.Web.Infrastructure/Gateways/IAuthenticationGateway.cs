using GreenLebanon.Taxi.Shared.Requests;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GreenLebanon.Taxi.Web.Infrastructure.Gateways
{
    public interface IAuthenticationGateway
    {
        Task<AuthResult> RegisterAsync(RegistrationDto model);
        Task<AuthResult> LoginAsync(LoginDto model);
    }
}
