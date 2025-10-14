using ASP.MVC.DTOs;
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

    [HttpGet]
    public IActionResult CreateBooking()
    {
      return View("EditBookings");
    }

    [HttpPost]
    public async Task<IActionResult> CreateBooking(CreateBookingDTO newBooking)
    {
      if (!ModelState.IsValid)
      {
        ViewData["ErrorMessage"] = "Invalid booking data.";
        return View("EditBookings");
      }
      try
      {
        var createdBooking = await _booking.CreateBooking(newBooking);
        if (createdBooking == null)
        {
          ViewData["ErrorMessage"] = "Failed to create booking. Booking == null";
          return View("EditBookings");
        }
        if (createdBooking != null)
        {
          ViewData["Success"] = "Booking created successfully!";
          return View("EditBookings");
        }
        else
        {
          ViewData["ErrorMessage"] = "Failed to create booking.";
          return View("EditBookings");
        }
      }
      catch (Exception ex)
      {
        ViewData["ErrorMessage"] = $"Error: {ex.Message}";
        return View("EditBookings");
      }
    }

    [HttpGet]
    public async Task<IActionResult> UpdateBooking(int id)
    {

      try
      {
        // Fetch the booking details by ID
        var booking = await _booking.GetBookingById(id);
        if (booking == null)
        {
          ViewData["ErrorMessage"] = "Booking not found.";
          return View("EditBookings");
        }

        // Map the booking to UpdateBookingDTO
        var updateBookingDTO = new UpdateBookingDTO
        {
          BookingId = id,
          StartDateTime = booking.StartDateTime,
          GuestNum = booking.GuestNum,
          Status = booking.Status
        };

        // Pass the DTO to the view
        return View(updateBookingDTO);
      }
      catch (Exception ex)
      {
        ViewData["ErrorMessage"] = $"Error: {ex.Message}";
        return View("EditBookings");
      }
    }

    [HttpPost]
    public async Task<IActionResult> UpdateBooking(int id, UpdateBookingDTO updatedBooking)
    {
      try
      {
        var booking = await _booking.GetBookingById(id);
        if (booking == null)
        {
          ViewData["ErrorMessage"] = "Booking with ID not found!";
          return View("EditBookings");
        }
        var bookingWithId = await _booking.UpdateBooking(id, updatedBooking);
        if (bookingWithId == null)
        {
          ViewData["ErrorMessage"] = "Failed to update booking. Booking == null";
          return View("EditBookings");
        }
        if (bookingWithId != null)
        {
          ViewData["Success"] = "Booking updated successfully!";
          return View(bookingWithId);
        }
        else
        {
          ViewData["ErrorMessage"] = "Failed to update booking.";
          return View("EditBookings");
        }
      }
      catch (Exception ex)
      {
        ViewData["ErrorMessage"] = $"Error: {ex.Message}";
        return View("EditBookings");
      }
    }
  }
}
