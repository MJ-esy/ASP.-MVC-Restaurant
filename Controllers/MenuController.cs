using Microsoft.AspNetCore.Mvc;

namespace ASP.MVC.Controllers
{
    public class MenuController : Controller
    {
        public IActionResult MenuIndex()
        {
            return View();
        }
    }
}
