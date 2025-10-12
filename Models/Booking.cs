namespace ASP.MVC.Models
{
  public class Booking
  {
    public int BookingId { get; set; }
    public int UserIdFk { get; set; }
    public int TableIdFk { get; set; }
    public string Name { get; set; }
    public int TableNum { get; set; }
    public DateTime StartDateTime { get; set; }
    public DateTime EndDateTime { get; set; }
    public int GuestNum { get; set; }
    public DateTime CreatedAt { get; set; }
    public DateTime? UpdatedAt { get; set; }
    public int Status { get; set; }
  }
}
