using ASP.MVC.DTOs;
using ASP.MVC.Models;

namespace ASP.MVC.Services.BookingServices
{
    public class BookingServices : IBookingServices
    {
        private readonly HttpClient _client;
        public BookingServices(IHttpClientFactory clientFactory)
        {
            _client = clientFactory.CreateClient("ASP_Reservations");
        }
        public async Task<List<Booking>> GetAllBookings()
        {
            var response = await _client.GetAsync("Bookings/allBookings");
            var bookingList = await response.Content.ReadFromJsonAsync<List<Booking>>();
            return bookingList;
        }

        public async Task<BookingDTO> GetBookingById(int id)
        {
            var response = await _client.GetAsync($"Bookings/{id}");
            var booking = await response.Content.ReadFromJsonAsync<BookingDTO>();
            return booking;
        }

        public async Task<List<BookingDTO>> BookingByDate(DateTime date)
        {
        var response = await _client.GetAsync($"Bookings/bookingsByDate?date={date}");
            var bookings = await response.Content.ReadFromJsonAsync<List<BookingDTO>>();
            return bookings;
        }

        public async Task<List<Booking>> BookingsToday()
        {
            var response = await _client.GetAsync($"Bookings/bookingsToday");
            var bookings = await response.Content.ReadFromJsonAsync<List<Booking>>();
            return bookings;
        }

        public async Task<Booking> CreateBooking(Booking newBooking)
        {
            var response = await _client.PostAsJsonAsync("Bookings/createNewBooking", newBooking);
            var createdBooking = await response.Content.ReadFromJsonAsync<Booking>();
            return createdBooking;
        }

        public async Task<Booking> UpdateBooking(int id, Booking updatedBooking)
        {
            var response = await _client.PutAsJsonAsync($"Bookings/updateBooking/{id}", updatedBooking);
            var booking = await response.Content.ReadFromJsonAsync<Booking>();
            return booking;
        }

        public async Task<bool> DeleteBooking(int id)
        {
            var response = await _client.DeleteAsync($"Bookings/deleteBooking/{id}");
            var result = response.IsSuccessStatusCode;
            return result;
        }
    }
}
