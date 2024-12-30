using GreenLebanon.Taxi.ApplicationCore.Entities;
using GreenLebanon.Taxi.Shared.Requests;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
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
                await this.userManager.AddToRoleAsync(user, await this.GetRoleName(model.UserType));

                await this.signInManager.SignInAsync(user, false);

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
                var token = await this.GenerateJwtToken( appUser);
                var identityUser = await userManager.FindByNameAsync(model.Username);
                var roles = await this.userManager.GetRolesAsync(identityUser);

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

        private async Task<string> GetRoleName(UserType userType)
        {
            string? role = string.Empty;

            switch (userType)
            {
                case UserType.Admin:
                    role = (await roleManager.Roles.FirstOrDefaultAsync(x => x.Name.Equals("admin"))).Name;
                    break;
                case UserType.Client:
                    role = (await roleManager.Roles.FirstOrDefaultAsync(x => x.Name.Equals("client"))).Name;
                    break;
                case UserType.Driver:
                    role = (await roleManager.Roles.FirstOrDefaultAsync(x => x.Name.Equals("driver"))).Name;
                    break;
                default:
                    break;
            }

            return role;
        }

        private async Task<object> GenerateJwtToken(ApplicationUser user)
        {
            var userClaims = await this.userManager.GetClaimsAsync(user);
            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(configuration.GetSection("JwtIssuerOptions:SecretKey").Value));
            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);
            var expires = DateTime.Now.AddDays(Convert.ToDouble(configuration.GetSection("JwtIssuerOptions:Expires").Value));
            var token = new JwtSecurityToken(
                configuration.GetSection("JwtIssuerOptions:Issuer").Value,
                configuration.GetSection("JwtIssuerOptions:Issuer").Value,
                userClaims,
                expires: expires,
                signingCredentials: creds
            );

            return new JwtSecurityTokenHandler().WriteToken(token);
        }
    }
}
