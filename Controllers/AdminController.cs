using Microsoft.AspNetCore.Mvc;

namespace ASP.MVC.Controllers
{
    public class AdminController : Controller
    {
        public IActionResult AdminPage()
        {
            return View();
        }
    }
}
