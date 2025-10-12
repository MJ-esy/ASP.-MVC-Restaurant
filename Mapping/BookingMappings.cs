using ASP.MVC.DTOs;
using ASP.MVC.Models.ViewModel;

namespace ASP.MVC.Mapping
{
  public static class BookingMappings
  {
    public static BookingVM ToBookingVM(this BookingDTO bookingDTO)
    {
      return new BookingVM
      {
        BookingId = bookingDTO.BookingId,
        StartDateTime = bookingDTO.StartDateTime,
        EndDateTime = bookingDTO.EndDateTime,
        GuestNum = bookingDTO.GuestNum,
        Name = bookingDTO.User != null ? bookingDTO.User.Name : null,
        Email = bookingDTO.User != null ? bookingDTO.User.Email : null,
        Phone = bookingDTO.User != null ? bookingDTO.User.Phone : null,
        TableId = bookingDTO.Table != null ? bookingDTO.Table.TableId : null,
        TableNum = bookingDTO.Table != null ? bookingDTO.Table.TableNum : null,
        TableCapacity = bookingDTO.Table != null ? bookingDTO.Table.Capacity : null,
        Status = bookingDTO.Status,
        CreatedAt = bookingDTO.CreatedAt,
        UpdatedAt = bookingDTO.UpdatedAt
      };
    }
  }
}
