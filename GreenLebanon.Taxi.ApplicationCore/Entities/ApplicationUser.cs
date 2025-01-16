using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GreenLebanon.Taxi.ApplicationCore.Entities
{
    public class ApplicationUser : IdentityUser
    {
        public string FirstName { get; set; }

        public string LastName { get; set; }

        public virtual ICollection<Trip> Trips { get; set; }
    }

    public class Driver
    {
        public string UserId { get; set; }

        public ApplicationUser ApplicationUser { get; set; }

        public string Region { get; set; }
    }
    
}
