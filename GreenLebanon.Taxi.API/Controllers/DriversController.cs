using GreenLebanon.Taxi.Infrastructure.Data;
using GreenLebanon.Taxi.Shared.Requests;
using Microsoft.AspNetCore.Mvc;
using GreenLebanon.Taxi.ApplicationCore.Entities;

namespace GreenLebanon.Taxi.API.Controllers {
    [ApiController]
    [Route("api/[controller]")]
    public class DriversController( AppDbContext context ) : ControllerBase {
        private readonly AppDbContext _context = context;

        [HttpPost]
        public async Task<IActionResult> AddDriver( [FromBody] RegistrationDto request )
        {
            if ( !ModelState.IsValid )
            {
                return BadRequest(ModelState);
            }
            var _driver = new RegistrationDto();
           // _context.Drivers.Add(_driver);
            await _context.SaveChangesAsync();
            return Ok(new { Message = "Driver added successfully" });
        }
    }
}
