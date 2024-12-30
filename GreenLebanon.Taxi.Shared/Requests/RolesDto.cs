namespace GreenLebanon.Taxi.Shared.Requests
{
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;

    public class RolesDto
    {
        [Required]
        public List<string> Roles { get; set; }
    }
}
