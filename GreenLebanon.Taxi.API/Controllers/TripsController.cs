﻿using Microsoft.AspNetCore.Mvc;
using GreenLebanon.Taxi.Application.Services;
using GreenLebanon.Taxi.Shared.Requests;
using Microsoft.AspNetCore.Authorization;
using System.Security.Claims;
namespace GreenLebanon.Taxi.API.Controllers {
    [ApiController]
    [Route ("api/[controller]")]
    public class TripsController(TripService tripService) : Controller {

        private readonly TripService tripService = tripService;
       
        [HttpPost]
        public async Task<IActionResult> AddTrip( [FromBody] AddTripRequest request )
        {
            if ( !ModelState.IsValid )
            {
                return BadRequest(ModelState);
            }

            return Ok(await tripService.AddNewTripAsync(request));
        }


        [HttpGet]
        public async Task<IActionResult> GetAllTrips( [FromQuery] int? tripId ) 
        {
            var trips = await tripService.GetAllTripsAsync(tripId);
    
            if(!trips.Any())
            {
                return NotFound();
            }
            return Ok(trips);
        }


        [HttpGet("GetTripsByUserId")]
        public async Task<IActionResult> GetTripsByUserId(  )
        {
            var clientId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            if ( clientId == null )
                return Unauthorized();

            var trips = string.Empty; 
                //= await tripService.GetTripsByUserIdAsync(clientId);
            return Ok(trips);
        }

    }
}
