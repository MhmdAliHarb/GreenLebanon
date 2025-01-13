namespace GreenLebanon.Taxi.ApplicationCore.Entities
{
    public class Trip
    {
        public int Id { get; set; }

        public string StartingPoint { get; set; }

        public string Destination { get; set; }

        public string Region { get; set; }

        public TimeOnly Timing { get; set; }

        public string DriverId { get; set; }

        public Driver Driver { get; set; }

        public string ClientId { get; set; }
        public ApplicationUser Client { get; set; }

        public Status Status { get; set; }
    }

    public enum Status
    {
        Pending,
        InProgress,
        Done
    }
}
