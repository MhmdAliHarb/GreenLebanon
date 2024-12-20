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
        public int Id { get; set; }
        [Required]
        public string StartingPoint { get; set; }
        [Required]
        public string Destination { get; set; }
        [Required]
        public string Region { get; set; }
        [Required]
        public TimeOnly Timing { get; set; }
    }
}
