using ASP.MVC.DTOs;
using System.ComponentModel.DataAnnotations;

namespace ASP.MVC.Models.ViewModel
{
  public class BookingVM
  {
    public int BookingId { get; set; }

    [DisplayFormat(DataFormatString = "{0:MM/dd/yyyy HH.mm}")]
    public DateTime StartDateTime { get; set; }

    [DisplayFormat(DataFormatString = "{0:MM/dd/yyyy HH.mm}")]
    public DateTime EndDateTime { get; set; }

    public int GuestNum { get; set; }
    public int UserIdFk { get; set; }

    public string? Name { get; set; }
    public string? Email { get; set; }
    public string? Phone { get; set; }

    public int? TableId { get; set; }
    public int? TableNum { get; set; }
    public int? TableCapacity { get; set; }

    public BookingStatus Status { get; set; }

    [DisplayFormat(DataFormatString = "{0:MM/dd/yyyy HH.mm}")]
    public DateTime CreatedAt { get; set; }

    [DisplayFormat(DataFormatString = "{0:MM/dd/yyyy HH.mm}")]
    public DateTime? UpdatedAt { get; set; }
  }
}
