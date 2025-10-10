using ASP.MVC.Models;

namespace ASP.MVC.Services.BookingServices
{
    public interface IBookingServices
    {
        Task<List<Booking>> GetAllBookings();
        Task<Booking> GetBookingById(int id);
        Task<List<Booking>> BookingByDate(DateTime date);
        Task<List<Booking>> BookingsToday(DateTime date);
        Task<Booking> CreateBooking(Booking newBooking);
        Task<Booking> UpdateBooking(int id, Booking updatedBooking);
        Task<bool> DeleteBooking(int id);
    }
}
