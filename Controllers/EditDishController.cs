using ASP.MVC.Models;
using ASP.MVC.Models.ViewModel;
using ASP.MVC.Services.MenuServices;
using Microsoft.AspNetCore.Mvc;
using System.Text.Json;

namespace ASP.MVC.Controllers
{
    public class EditDishController : Controller
    {
        private readonly IMenuServices _menuServices;
        public EditDishController(IMenuServices menuServices)
        {
            _menuServices = menuServices;
        }

        public IActionResult EditDishIndex()
        {
            return View(new Menu());
        }

        [HttpGet]
        public async Task<IActionResult> GetDishById(int dishId)
        {
            try
            {
                var dish = await _menuServices.GetMenuItemById(dishId);
                if (dish == null)
                {
                    ViewData["ErrorMessage"] = "No dish found with the provided ID.";
                    return View("EditDishIndex", new Menu());
                }
                
                //ViewBag.DishById = dish;
                return View("EditDishIndex", dish);
            }
            catch (HttpRequestException ex)
            {
                ViewData["ErrorMessage"] = $"Error fetching dish: {ex.Message}";
                return View("EditDishIndex");
            }
            catch (JsonException ex)
            {
                ViewData["ErrorMessage"] = $"Error processing dish data: {ex.Message}";
                return View("EditDishIndex");
            }
        }

        [HttpGet]
        public IActionResult CreateDish()
        {
            //to fill in data
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> CreateDish(DishVM newDish)
        {
            if (!ModelState.IsValid)
            {
                return View(newDish);
            }
            var menuItem = new Menu
            {
                DishName = newDish.DishName,
                Price = newDish.Price,
                Description = newDish.Description,
                IsPopular = newDish.IsPopular,
                Category = newDish.Category,
                Allergen = newDish.Allergen,
                ImageUrl = newDish.ImageUrl
            };
            var result = await _menuServices.CreateMenuItem(menuItem);
            if (!result)
            {
                ViewData["ErrorMessage"] = "Dish creation is unsuccessful!";
                return View(newDish);
            }
            return RedirectToAction("MenuIndex", "Menu");
        }

        [HttpGet]
        public async Task<IActionResult> UpdateDish(int id)
        {
            if (id <= 0)
            {
                ViewData["ErrorMessage"] = "Invalid dish ID. Please provide a valid ID.";
                return View("EditDishIndex");
            }
            try
            {
                var dish = await _menuServices.GetMenuItemById(id);
                if (dish == null) return NotFound("Dish with id not found");
                var updatedDish = new Menu
                {
                    DishId = dish.DishId,
                    DishName = dish.DishName,
                    Price = dish.Price,
                    Description = dish.Description,
                    IsPopular = dish.IsPopular,
                    Category = dish.Category,
                    Allergen = dish.Allergen,
                    ImageUrl = dish.ImageUrl
                };
                return View(updatedDish);

            }
            catch (Exception ex)
            {
                return StatusCode(500, $"An error occured: {ex.Message}");
            }
        }   

        [HttpPut]
        public async Task<IActionResult> UpdateDish(int id, Menu updatedDish)
        {
            if (!ModelState.IsValid)
            {
                if (id <= 0)
                {
                    ModelState.AddModelError("", "Invalid dish ID.");
                    return View(updatedDish);
                }
                return View(updatedDish);
            }
            
            var result = await _menuServices.UpdateMenuItem(id, updatedDish);
            if (!result)
            {
                ViewData["ErrorMessage"] = "Dish update unsuccessful!";
                return View(updatedDish);
            }
            TempData["Success"] = "Dish updated successful!";
            return RedirectToAction("MenuIndex", "Menu");
        }

        public async Task<IActionResult> DeleteDish(int id)
        {
            var dish = await _menuServices.GetMenuItemById(id);
            if (dish == null) return View("NotFound");
            await _menuServices.DeleteMenuItem(id);
            return View("Success");
        }

    }
}