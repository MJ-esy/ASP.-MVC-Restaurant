using ASP.MVC.Services.TableServices;
using Microsoft.AspNetCore.Mvc;

namespace ASP.MVC.Controllers
{
  public class EditTableController : Controller
  {
    private readonly ITableServices _tableServices;
    public EditTableController(ITableServices tableServices)
    {
      _tableServices = tableServices;
    }

    public IActionResult EditTables()
    {
      return View();
    }

    [HttpGet]
    public async Task<IActionResult> AllTables()
    {
      try
      {
        var tables = await _tableServices.GetAllTables();
        if (tables == null || !tables.Any())
        {
          ViewData["ErrorMessage"] = "No tables found.";
          return View("EditTables");
        }
        return View(tables);
      }
      catch (Exception ex)
      {

        ViewData["ErrorMessage"] = $"Error processing dish data: {ex.Message}";
        return View("EditTables");
      }      
    }

    [HttpGet]
    public async Task<IActionResult> AvailableTables()
    {
      try
      {
        var tables = await _tableServices.GetAvailableTable();
        if (tables == null || !tables.Any())
        {
          ViewData["ErrorMessage"] = "No tables found.";
          return View("EditTables");
        }
        return View(tables);
      }
      catch (Exception ex)
      {

        ViewData["ErrorMessage"] = $"Error processing dish data: {ex.Message}";
        return View("EditTables");
      }
    }
  }
}
