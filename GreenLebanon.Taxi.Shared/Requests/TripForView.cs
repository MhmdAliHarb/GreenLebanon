using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GreenLebanon.Taxi.Shared.Requests
{
    public class TripForView
    {
        public int Id { get; set; }

        public string StartingPoint { get; set; }

        public string Destination { get; set; }

        public string Region { get; set; }

        public TimeOnly Timing { get; set; }

        public string ClientName { get; set; }

        public string Status { get; set; }
    }
}
