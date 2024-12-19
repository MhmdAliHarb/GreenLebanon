using System.ComponentModel.DataAnnotations;
namespace GreenLebanon.Taxi.Shared.Requests{
    public class AddClientRequest {
        [Required]
        public string FirstName { get; set; }
        [Required]
        public string LastName { get; set; }
        [Required, Phone]
        public string PhoneNumber { get; set; }
        public string Address { get; set; }
        [Required]
        public DateTime DateOfBirth { get; set; }
    }

    public class UpdateClientRequest {

    }
}
