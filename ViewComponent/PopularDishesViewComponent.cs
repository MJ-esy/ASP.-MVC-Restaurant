using Microsoft.AspNetCore.Mvc;

namespace ASP.MVC.ViewComponents
{
    public class PopularDishesViewComponent : ViewComponent
    {
        public IViewComponentResult Invoke()
        {

            return View();
        }
    }
}