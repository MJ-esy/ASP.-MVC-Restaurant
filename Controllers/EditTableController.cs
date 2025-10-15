using ASP.MVC.DTOs;
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

    [HttpPost]
    public async Task<IActionResult> SetTableAvailability(int id, SetTableAvailabilityDTO tableAvailability)
    {
      try
      {
        var result = await _tableServices.SetTableAvailability(id, tableAvailability);
        if (!result)
        {
          ViewData["ErrorMessage"] = "Table availability update unsuccessful!";
          return View("EditTables");
        }
        ViewData["Success"] = $"Table availability update successful! Table Available: {tableAvailability.IsAvailable}";
        return View("EditTables", result);
      }
      catch (Exception ex)
      {
        ViewData["ErrorMessage"] = $"Error processing dish data: {ex.Message}";
        return View("EditTables");
      }
    }

    [HttpGet]
    public async Task<IActionResult> UpdateTable(int id)
    {
      if (id <= 0)
      {
        ViewData["ErrorMessage"] = "Invalid table ID. Please provide a valid ID.";
        return View("EditTables");
      }
      try
      {
        var table = await _tableServices.GetTableById(id);
        if (table == null)
        {
          ViewData["ErrorMessage"] = "Table with id not found";
          return View();
        }
        var updatedTable = new UpdateTableDTO
        {
          TableId = id,
          TableNum = table.TableNum,
          Capacity = table.Capacity,
          IsAvailable = table.IsAvailable
        };
        return View(updatedTable);
      }
      catch (Exception ex)
      {
        ViewData["ErrorMessage"] = $"Error processing dish data: {ex.Message}";
        return View("EditTables");

      }
    }

    [HttpPost]
    public async Task<IActionResult> UpdateTable(int id, UpdateTableDTO updatedTable)
    {
      if (!ModelState.IsValid)
      {
        if (id <= 0)
        {
          ViewData["ErrorMessage"] = "Invalid table ID.";
          return View(updatedTable);
        }
        return View(updatedTable);
      }
      try
      {
        var result = await _tableServices.UpdateTableById(id, updatedTable);
        if (result == null)
        {
          ViewData["ErrorMessage"] = "Table update unsuccessful!";
          return View(updatedTable);
        }
        ViewData["Success"] = "Table update successful!";
        return View(result);
      }
      catch (Exception ex)
      {
        ViewData["ErrorMessage"] = $"Error processing table data: {ex.Message}";
        return View("EditTables");
      }
    }
  }
}