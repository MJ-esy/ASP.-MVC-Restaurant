using Microsoft.AspNetCore.Mvc;

namespace ASP.MVC.Controllers
{
    public class BookingController : Controller
    {
        public IActionResult Booking()
        {
            return View();
        }
    }
}
