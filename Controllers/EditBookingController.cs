using ASP.MVC.Mapping;
using ASP.MVC.Services.BookingServices;
using Microsoft.AspNetCore.Mvc;

namespace ASP.MVC.Controllers
{
  public class EditBookingController : Controller
  {
    private readonly IBookingServices _booking;
    public EditBookingController(IBookingServices bookingServices)
    {
      _booking = bookingServices;
    }
    public IActionResult EditBookings()
    {
      return View();
    }

    [HttpGet]
    public async Task<IActionResult> AllBookings()
    {
      var bookings = await _booking.GetAllBookings();
      if (!bookings.Any())
      {
        ViewData["ErrorMessage"] = "No bookings found.";
        return View("EditBookings");
      }
      return View(bookings);
    }

    [HttpGet]
    public async Task<IActionResult> BookingById(int BookingId)
    {
      try
      {
        var booking = await _booking.GetBookingById(BookingId);
        if (booking == null)
        {
          ViewData["ErrorMessage"] = "Booking with ID not found!";
          return View("EditBookings");
        }
        var bookingVM = booking.ToBookingVM();
        return View(bookingVM);
      }
      catch (Exception ex)
      {
        ViewData["ErrorMessage"] = $"Error: {ex.Message}";
        return View("EditBookings");
      }
    }

    [HttpGet]
    public async Task<IActionResult> BookingByDate(DateTime date)
    {
      try
      {
        var bookings = await _booking.BookingByDate(date);
        if (!bookings.Any())
        {
          ViewData["ErrorMessage"] = "No bookings found.";
          return View("EditBookings");
        }

        var bookingVMs = bookings.Select(booking => booking.ToBookingVM()).ToList();
        return View(bookingVMs);
      }
      catch (Exception ex)
      {
        ViewData["ErrorMessage"] = $"Error: {ex.Message}";
        return View("EditBookings");
      }
    }

    [HttpGet]
    public async Task<IActionResult> BookingToday()
    {
      try
      {
        var bookings = await _booking.BookingsToday();
        if (!bookings.Any())
        {
          ViewData["ErrorMessage"] = "No bookings found.";
          return View("EditBookings");
        }
        return View(bookings);
      }
      catch (Exception ex)
      {
        ViewData["ErrorMessage"] = $"Error: {ex.Message}";
        return View("EditBookings");
      }
    }
  }
}
