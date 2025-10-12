namespace ASP.MVC.DTOs
{
  public enum BookingStatus
  {
    Confirmed = 1,
    Completed = 2,
    Cancelled = 3,
    NoShow = 4
  }

  public class UserDto
  {
    public string Name { get; set; }
    public string Email { get; set; }
    public string Phone { get; set; }
    public int TotalBookings { get; set; }
  }

  public class TableDto
  {
    public int TableId { get; set; }
    public int TableNum { get; set; }
    public int Capacity { get; set; }
  }

  public class BookingDTO
  {
    public int BookingId { get; set; }

    public DateTime StartDateTime { get; set; }
    public DateTime EndDateTime { get; set; }

    public int TableIdFk { get; set; }
    public TableDto? Table { get; set; }

    public int GuestNum { get; set; }

    public int UserIdFk { get; set; }
    public UserDto? User { get; set; }

    public DateTime CreatedAt { get; set; }
    public DateTime? UpdatedAt { get; set; }

    public BookingStatus Status { get; set; }
  }
}
