using Green_Taxi.Models;

namespace Green_Taxi.Identity {
    public interface IAccountManagement {
        Task<AuthResult> LoginAsync( LoginModel credentials );
        Task<AuthResult> RegisterAsync(string Email , string Password );
        Task LogoutAsync();
    }
}
