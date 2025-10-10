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

    public IActionResult EditDish()
    {
      return View();
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
          return View("EditDish");
        }
        return View("EditDish", dish);
      }
      catch (HttpRequestException ex)
      {
        ViewData["ErrorMessage"] = $"Error fetching dish: {ex.Message}";
        return View("EditDish");
      }
      catch (JsonException ex)
      {
        ViewData["ErrorMessage"] = $"Error processing dish data: {ex.Message}";
        return View("EditDish");
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
    public async Task<IActionResult> UpdateDish(int DishId)
    {
      if (DishId <= 0)
      {
        ViewData["ErrorMessage"] = "Invalid dish ID. Please provide a valid ID.";
        return View("EditDish");
      }
      try
      {
        var dish = await _menuServices.GetMenuItemById(DishId);
        if (dish == null) return NotFound("Dish with id not found");
        var updatedDish = new Menu
        {
          DishId = DishId,
          DishName = dish.DishName,
          Price = dish.Price,
          Description = dish.Description,
          IsPopular = dish.IsPopular,
          Category = dish.Category,
          Allergen = dish.Allergen,
          ImageUrl = dish.ImageUrl
        };
        return View("UpdateDish", updatedDish);

      }
      catch (Exception ex)
      {
        return StatusCode(500, $"An error occured: {ex.Message}");
      }
    }

    [HttpPost]
    public async Task<IActionResult> UpdateDish(int DishId, Menu updatedDish)
    {
      if (!ModelState.IsValid)
      {
        if (DishId <= 0)
        {
          ViewData["ErrorMessage"] = "Invalid dish ID.";
          return View(updatedDish);
        }
        return View(updatedDish);
      }

      var result = await _menuServices.UpdateMenuItem(DishId, updatedDish);
      if (!result)
      {
        ViewData["ErrorMessage"] = "Dish update unsuccessful!";
        return View(updatedDish);
      }
      ViewData["Success"] = "Dish updated successful!";
      return RedirectToAction("MenuIndex", "Menu");
    }

    [HttpPost]
    public async Task<IActionResult> DeleteDish(int id)
    {
      if (id <= 0)
      {
        ViewData["ErrorMessage"] = "Invalid dish ID. Please provide a valid ID.";
        return View("EditDish");
      }
      var dish = await _menuServices.GetMenuItemById(id);
      if (dish == null)
      {
        ViewData["ErrorMessage"] = "Dish with with ID is not Found";
        return View("EditDish");
      }
      var result = await _menuServices.DeleteMenuItem(id);
      if (!result)
      {
        ViewData["ErrorMessage"] = "Dish deletion unsuccessful!";
        return View("EditDish");
      }
      ViewData["Success"] = "Dish deleted successful!";
      return RedirectToAction("MenuIndex", "Menu");
    }
  }
}