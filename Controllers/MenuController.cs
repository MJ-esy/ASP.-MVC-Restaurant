using ASP.MVC.Services.MenuServices;
using Microsoft.AspNetCore.Mvc;

namespace ASP.MVC.Controllers
{
    public class MenuController : Controller
    {
        private readonly IMenuServices _menuServices;
        public MenuController(IMenuServices menuServices)
        {
            _menuServices = menuServices;
        }

        public async Task<IActionResult> MenuIndex()
        {
            var dishList = await _menuServices.GetMenuItems();
            return View(dishList);
        }

        public IActionResult _DishCard()
        {
            return View();
        }
    }
}
