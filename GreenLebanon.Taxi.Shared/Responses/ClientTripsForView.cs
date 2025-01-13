using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GreenLebanon.Taxi.Shared.Responses
{
    public class ClientTripsForView
    {
        public string StartingPoint { get; set; }

        public string Destination { get; set; }

        public string Region { get; set; }

        public TimeOnly Timing { get; set; }

        public string DriverName { get; set; }

        public string TripStatus { get; set; }
    }
}
