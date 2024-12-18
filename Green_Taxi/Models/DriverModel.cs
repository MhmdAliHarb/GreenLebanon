using Green_Taxi.Enums;

namespace Green_Taxi.Models {
    public class DriverModel {
        public int Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string PhoneNb { get; set; }
        public string VehicleNb { get; set; }
        public string VehcileName { get; set; }
        public DateTime DateOfBirth { get; set; }
        public VehicleType VehicleType { get; set; }
    }
}
