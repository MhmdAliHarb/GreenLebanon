using System.ComponentModel.DataAnnotations;

namespace Green_Taxi.Models {
    public class ClientModel {
        public int Id { get; set; }
        [Required]
        public string FirstName { get; set; }
        [Required]
        public string LastName { get; set; }
        [Required, Phone]
        public string PhoneNb { get; set; }
        public string PlaceOfLiving { get; set; }
        [Required]
        public DateTime DateOfBirth { get; set; }

        public byte[]? ProfileImage { get; set; }


    }
}
