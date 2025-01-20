using GreenLebanon.Taxi.Application.Services;
using GreenLebanon.Taxi.Shared.Requests;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace GreenLebanon.Taxi.API.Controllers
{
    [AllowAnonymous]
    public class AccountController : ControllerBase
    {
        private readonly AccountService accountService;

        public AccountController(AccountService accountService)
        {
            this.accountService = accountService;
        }

        [HttpPost("account/register")]
        public async Task<IActionResult> Register([FromBody] RegistrationDto model)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var result = await this.accountService.Register(model);

            if (result.Equals("-1"))
            {
                return BadRequest("User Not Created :(");
            }

            return Ok(result);
        }

        [HttpPost("account/login")]
        public async Task<IActionResult> Login([FromBody] LoginDto model)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var result = await this.accountService.Login(model);

            if (result.Token.Contains("User name does not exist! :("))
            {
                return NotFound(model.Username + " does not exist");
            }

            return Ok(result);
        }

        [HttpPost("account/role")]
        public async Task<IActionResult> AddRole([FromBody] RolesDto roles)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var result = await this.accountService.CreateRoleToUser(roles);

            return Ok(result);
        }

        [HttpGet("account/roles/all")]
        public async Task<IActionResult> GetAllRoles()
        {
            var result = await this.accountService.GetAllRoles();

            if (result == null)
            {
                return NotFound();
            }

            return Ok(result);
        }
    }
}
