using GreenLebanon.Taxi.ApplicationCore.Entities;
using GreenLebanon.Taxi.Shared.Requests;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace GreenLebanon.Taxi.Application.Services
{
    public class AccountService(
       UserManager<ApplicationUser> userManager,
       SignInManager<ApplicationUser> signInManager,
       RoleManager<IdentityRole> roleManager,
       IConfiguration configuration)
    {
        private readonly UserManager<ApplicationUser> userManager = userManager;
        private readonly SignInManager<ApplicationUser> signInManager = signInManager;
        private readonly RoleManager<IdentityRole> roleManager = roleManager;
        private readonly IConfiguration configuration = configuration;

        public async Task<string> Register(RegistrationDto model)
        {
            var user = new ApplicationUser
            {
                Email = model.Email,
                UserName = model.Username,
                PhoneNumber = model.MobileNumber,
                FirstName = model.FirstName,
                LastName = model.LastName,
            };

            var createUser = await this.userManager.CreateAsync(user, model.Password);

            if (createUser.Succeeded)
            {
                await userManager.AddToRoleAsync(user, Enum.GetName(model.UserType) ?? string.Empty);

                await signInManager.SignInAsync(user, false);

                return "User Registered :)";
            }

            return "-1";
        }

        public async Task<UserLoginResponse> Login(LoginDto model)
        {
            var result = await signInManager.PasswordSignInAsync(model.Username, model.Password, model.RememberMe, lockoutOnFailure: false);

            if (result.Succeeded)
            {
                var appUser = this.userManager.Users.FirstOrDefault(x => x.UserName.ToLower() == model.Username.ToLower());
               
                var identityUser = await userManager.FindByNameAsync(model.Username);
                
                var roles = await userManager.GetRolesAsync(identityUser);

                var token = GenerateJwtToken(appUser, roles[0]);

                return new UserLoginResponse() { Roles = roles.ToList(), Token = token.ToString() };
            }

            return new UserLoginResponse() { Token = "User name does not exist! :(" };
        }

        public async Task<string> CreateRoleToUser(RolesDto roles)
        {
            foreach (var role in roles.Roles)
            {
                await this.roleManager.CreateAsync(new IdentityRole() { Name = role });
            }

            return "Created";
        }

        public async Task<IEnumerable<UserRole>> GetAllRoles()
        {
            var userRoles = new List<UserRole>();

            var roles = await this.roleManager.Roles.ToListAsync();

            if (roles != null && roles.Count > 0)
            {
                foreach (var role in roles)
                {
                    userRoles.Add(new UserRole()
                    {
                        Id = role.Id,
                        Name = role.Name
                    });
                }
            }

            return userRoles;
        }

        private string GenerateJwtToken(ApplicationUser user, string userRole)
        {
            var userClaims = new List<Claim>
            {
                new Claim(JwtRegisteredClaimNames.Sub, user.Id.ToString()),
                new Claim(JwtRegisteredClaimNames.Name, user.FirstName),
                new Claim(JwtRegisteredClaimNames.FamilyName, user.LastName),
                new Claim(JwtRegisteredClaimNames.Email, user.Email),
                new Claim(ClaimTypes.Role, userRole)
            };

            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(configuration["JwtIssuerOptions:SecretKey"]));
            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);
           // var expires = DateTime.UtcNow.AddMinutes(Convert.ToDouble(configuration["JwtIssuerOptions:Expires"]));
            var expires = DateTime.UtcNow.AddDays(1);
            var iss = configuration["JwtIssuerOptions:Issuer"];
            var aud = configuration["JwtIssuerOptions:Audience"];
            var token = new JwtSecurityToken(
                iss,
                aud,
                userClaims,
                expires: expires,
                signingCredentials: creds
            );

            string result = new JwtSecurityTokenHandler().WriteToken(token);

            return result;
        }
    }
}
