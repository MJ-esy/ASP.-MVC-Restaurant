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
      var response = await _client.GetAsync("Table/getAvailableTables");
      var tables = await response.Content.ReadFromJsonAsync<IEnumerable<TableDTO>>();
      return tables;
    }
    public async Task<bool> SetTableAvailability(int id, SetTableAvailabilityDTO tableAvailability)
    {
      var content = await GetTableById(id);
      if (content == null) return false;
      content.IsAvailable = tableAvailability.IsAvailable;
      var toJson = JsonContent.Create(content);
      var response = await _client.PatchAsync($"Table/setTableAvailability/{id}", toJson);
      if (!response.IsSuccessStatusCode) return false;
      return response.IsSuccessStatusCode;
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
