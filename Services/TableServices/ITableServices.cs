using ASP.MVC.DTOs;

namespace ASP.MVC.Services.TableServices
{
  public interface ITableServices
  {
    Task<List<TableSummaryDTO>> GetAllTables();
    Task<TableSummaryDTO> GetTableById(int id);
    Task<IEnumerable<TableDTO>> GetAvailableTable();
    Task<bool> SetTableAvailability(int id);
    Task<UpdateTableDTO> UpdateTableById(int id, UpdateTableDTO updateTable);
  }
}
