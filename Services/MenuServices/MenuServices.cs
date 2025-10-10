using ASP.MVC.Models;
using System.Text.Json;

namespace ASP.MVC.Services.MenuServices
{
    public class MenuServices : IMenuServices
    {
        private readonly HttpClient _client;
        public MenuServices(IHttpClientFactory clientFactory)
        {
            _client = clientFactory.CreateClient("ASP_Reservations");
        }
        public async Task<List<Menu>> GetMenuItems()
        {
            var response = await _client.GetAsync("Dish/allDishes");
            var dishList = await response.Content.ReadFromJsonAsync<List<Menu>>();
            return dishList;
        }

        public async Task<Menu> GetMenuItemById(int id)
        {
            try
            {
                var response = await _client.GetAsync($"Dish/id?id={id}");
                if (!response.IsSuccessStatusCode)
                {
                    throw new HttpRequestException($"Request failed with status code {response.StatusCode}");
                }

                var content = await response.Content.ReadFromJsonAsync<Menu>();
                return content;
            }
            catch (Exception ex)
            {
                throw new Exception($"Error fetching menu item with ID {id}: {ex.Message}", ex);
            }
        }
           
        

        public async Task<bool> CreateMenuItem(Menu newMenuItem)
        {
            var response = await _client.PostAsJsonAsync("Dish/createNewDish", newMenuItem);
            if (!response.IsSuccessStatusCode)
            {
                Console.WriteLine(response.StatusCode); 
                return false;
            }
            return response.IsSuccessStatusCode;
        }

        public async Task<bool> UpdateMenuItem(int id, Menu updatedMenuItem)
        {
            var response = await _client.PutAsJsonAsync($"Dish/updateDish/{id}", updatedMenuItem);
            if (!response.IsSuccessStatusCode)
            {
                Console.WriteLine(response.StatusCode);
                return false;
            }
            return response.IsSuccessStatusCode;
        }

        public async Task<bool> DeleteMenuItem(int id)
        {
            var response = await _client.DeleteAsync($"Dish/deleteDish/{id}");
            if (!response.IsSuccessStatusCode)
            {
                Console.WriteLine(response.StatusCode);
                return false;
            }
            return response.IsSuccessStatusCode;
        }


    }
}
