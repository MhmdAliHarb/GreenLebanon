using GreenLebanon.Taxi.ApplicationCore.Entities;
using GreenLebanon.Taxi.Shared.Requests;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace GreenLebanon.Taxi.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsersController(UserManager<ApplicationUser> userManager) : ControllerBase
    {
        private readonly UserManager<ApplicationUser> userManager = userManager;

        [HttpGet("{userId}")]
        public async Task<IActionResult> GetUser(string userId)
        {
            var user = await userManager.FindByIdAsync(userId);
            if (user == null)
            {
                return NotFound(new { Message = "User not found" });
            }

            var roles = await userManager.GetRolesAsync(user);
            var role = roles.FirstOrDefault() ?? "NoRoleAssigned";

            return Ok(new ApplicationUserForView
            {
                Email = user.Email,
                FirstName = user.FirstName,
                LastName = user.LastName,
                UserId = user.Id,
                Role = role
            });
        }
    }
}
