using ASP.MVC.DTOs;

namespace ASP.MVC.Services.TableServices
{
  public class TableServices : ITableServices
  {
    private readonly HttpClient _client;
    public TableServices(IHttpClientFactory clientFactory)
    {
      _client = clientFactory.CreateClient("ASP_Reservations");
    }
    public async Task<List<TableSummaryDTO>> GetAllTables()
    {
      var response = await _client.GetAsync("Table/allTables");
      var tableList = await response.Content.ReadFromJsonAsync<List<TableSummaryDTO>>();
      return tableList;
    }
    public async Task<TableSummaryDTO> GetTableById(int id)
    {
      var response = await _client.GetAsync($"Table/{id}");
      var table = await response.Content.ReadFromJsonAsync<TableSummaryDTO>();
      return table;
    }
    public async Task<IEnumerable<TableDTO>> GetAvailableTable()
    {
      var response = await _client.GetAsync("Table/getAvailableTable");
      var tables = await response.Content.ReadFromJsonAsync<IEnumerable<TableDTO>>();
      return tables;
    }
    public async Task<bool> SetTableAvailability(int id)
    {
      var table = await _client.PatchAsync($"Table/setTableAvailability/{id}", null);
      if (table == null) return false;
      return table.IsSuccessStatusCode;
    }
    public async Task<UpdateTableDTO> UpdateTableById(int id, UpdateTableDTO updateTable)
    {
      var response = await _client.PutAsJsonAsync($"Table/updateTable/{id}", updateTable);
      if (!response.IsSuccessStatusCode)
      {
        Console.WriteLine(response.StatusCode);
        return null;
      }
      var updatedTable = await response.Content.ReadFromJsonAsync<UpdateTableDTO>();
      return updatedTable;
    }
  }
}
