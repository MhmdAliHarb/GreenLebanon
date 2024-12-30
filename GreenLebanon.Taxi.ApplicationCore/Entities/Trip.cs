namespace GreenLebanon.Taxi.ApplicationCore.Entities
{
    public class Trip {
        public int Id { get; set; }
        public string StartingPoint { get; set; }
        public string Destination { get; set; }
        public string Region { get; set; }
        public TimeOnly Timing {  get; set; }

        public string UserId { get; set; }
        public virtual ApplicationUser ApplicationUser { get; set; }

        public Status Status { get; set; }
    }

    public enum Status
    {
        Pending,
        InProgress,
        Done
    }
}
