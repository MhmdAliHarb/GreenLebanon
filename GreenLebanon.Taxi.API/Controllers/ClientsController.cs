﻿using GreenLebanon.Taxi.ApplicationCore.Entities;
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

        [HttpPut("{id}")]
        public IActionResult UpdateClient( int id, [FromBody] UpdateClientRequest request ) {
            if ( request == null || !ModelState.IsValid ) {
                return BadRequest("Invalid client data.");
            }

            var client = _context.Clients.FirstOrDefault(c => c.Id == id);
            if ( client == null ) {
                return NotFound("Client not found.");
            }

            _context.Clients.Update(client);
            _context.SaveChanges();

            return Ok(new { message = "Client updated successfully", client });
        }
    }
}
