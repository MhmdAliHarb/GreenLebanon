using Microsoft.AspNetCore.Mvc;
using GreenLebanon.Taxi.Application.Services;
using GreenLebanon.Taxi.Shared.Requests;
namespace GreenLebanon.Taxi.API.Controllers {
    [ApiController]
    [Route ("api/[controller]")]
    public class TripsController(TripService tripService) : Controller {

        private readonly TripService tripService = tripService;
        [HttpPost]
        [HttpPost]
        public async Task<IActionResult> AddTrip( [FromBody] AddTripRequest request )
        {
            if ( !ModelState.IsValid )
            {
                return BadRequest(ModelState);
            }

            return Ok(await tripService.AddNewTripAsync(request));
        }

    }
}
