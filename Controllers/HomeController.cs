using ASP.MVC.Services.MenuServices;
using Microsoft.AspNetCore.Mvc;

namespace ASP.MVC.Controllers
{
    public class HomeController : Controller
    {
        private readonly IMenuServices _menuServices;
        private readonly ILogger<HomeController> _logger;

        public HomeController(ILogger<HomeController> logger, IMenuServices menuServices)
        {
            _logger = logger;
            _menuServices = menuServices;
        }

        public async Task<IActionResult> Index()
        {
            var menuItems = await _menuServices.GetMenuItems();
            return View(menuItems);
        }


    }
}
