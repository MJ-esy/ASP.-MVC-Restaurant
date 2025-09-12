namespace ASP.MVC.Models
{
    public class Admin
    {
        public int AdminId { get; set; }
        public string Username { get; set; }
        public string Password { get; set; }
        public string Role { get; set; } = "ADMIN"; 
    }
}
