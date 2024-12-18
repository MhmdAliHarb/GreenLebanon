namespace Green_Taxi.Models {
    public class AuthResult {
        public bool Succeeded { get; set; }
        public string[] ErrorList { get; set; } = [];
    }
}
