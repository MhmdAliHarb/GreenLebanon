using GreenLebanon.Taxi.ApplicationCore.Entities;
using GreenLebanon.Taxi.Infrastructure.Data;
using GreenLebanon.Taxi.Shared.Requests;
using Microsoft.AspNetCore.Mvc;

namespace GreenLebanon.Taxi.API.Controllers {
    [ApiController]
    [Route("api/[controller]")]
    public class ClientsController (AppDbContext context) : ControllerBase {
        private readonly AppDbContext _context = context ;
        [HttpPost]
        public async Task<IActionResult> AddClient( [FromBody] AddClientRequest request ) {
            if ( !ModelState.IsValid ) {
                return BadRequest(ModelState);
            }
            var _client = new Client();
            _context.Clients.Add( _client );
            await _context.SaveChangesAsync(); 
            return Ok(new { Message = "Client added successfully", ClientId = _client.Id });
        }
    }
}
