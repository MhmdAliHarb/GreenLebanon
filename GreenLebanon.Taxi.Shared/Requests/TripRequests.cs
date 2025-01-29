using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GreenLebanon.Taxi.Shared.Requests
{
    public class AddTripRequest
    {
        [Required]
        public string StartingPoint { get; set; }
        [Required]
        public string Destination { get; set; }
        [Required]
        public string Region { get; set; }
        [Required]
        public DateTime Timing { get; set; }

        public string ClientId { get; set; }
    }
   
}
