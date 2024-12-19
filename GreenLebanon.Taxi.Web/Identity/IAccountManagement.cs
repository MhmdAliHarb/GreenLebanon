using GreenLebanon.Taxi.Web.Models;
namespace GreenLebanon.Taxi.Web.Identity {
    public interface IAccountManagement {
        Task<AuthResult> LoginAsync( LoginModel credentials );
        Task<AuthResult> RegisterAsync( string Email, string Password );
        Task LogoutAsync();
    }
}
