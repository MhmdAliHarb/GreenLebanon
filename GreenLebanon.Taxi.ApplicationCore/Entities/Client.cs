namespace GreenLebanon.Taxi.ApplicationCore.Entities
{
    public class Client
    {
        public int Id { get; set; }

        public string ApplicationUserId { get; set; }
        public virtual ApplicationUser ApplicationUser { get; set; }
    }
}
