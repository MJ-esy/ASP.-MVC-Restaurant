using ASP.MVC.DTOs;
using ASP.MVC.Models;

namespace ASP.MVC.Services.BookingServices
{
    public interface IBookingServices
    {
        Task<List<Booking>> GetAllBookings();
        Task<BookingDTO> GetBookingById(int id);
        Task<List<BookingDTO>> BookingByDate(DateTime date);
        Task<List<Booking>> BookingsToday();
        Task<Booking> CreateBooking(Booking newBooking);
        Task<Booking> UpdateBooking(int id, Booking updatedBooking);
        Task<bool> DeleteBooking(int id);
    }
}
