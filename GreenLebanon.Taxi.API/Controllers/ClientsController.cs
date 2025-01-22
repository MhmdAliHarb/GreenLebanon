using GreenLebanon.Taxi.Application.Services;
using GreenLebanon.Taxi.Shared.Requests;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace GreenLebanon.Taxi.API.Controllers
{
    [Authorize(Roles ="Client")]
    [ApiController]
    [Route("api/[controller]")]
    public class ClientsController(ClientService clientService) : ControllerBase
    {
        private readonly ClientService clientService = clientService;

        [HttpGet]
        public async Task<IActionResult> GetAllClients()
        {
            return Ok(await clientService.GetAllClientsAsync());
        }

        [HttpGet("{clientId}")]
        public async Task<IActionResult> GetClient(string clientId)
        {
            return Ok(await clientService.GetClientByIdAsync(clientId));
        }

        [HttpGet("GetClientTrips/{clientId}")]
        public async Task<IActionResult> GetClientTrips(string clientId)
        {
            return Ok(await clientService.GetClientTripsAsync(clientId));
        }
    }
}

