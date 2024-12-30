using GreenLebanon.Taxi.Application.Services;
using GreenLebanon.Taxi.Shared.Requests;
using Microsoft.AspNetCore.Mvc;

namespace GreenLebanon.Taxi.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ClientsController(ClientService clientService) : ControllerBase
    {
        private readonly ClientService clientService = clientService;
    }
}

