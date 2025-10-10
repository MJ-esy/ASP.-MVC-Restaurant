using ASP.MVC.Models;

namespace ASP.MVC.Services.MenuServices
{
    public interface IMenuServices
    {
        Task<List<Menu>> GetMenuItems();
        Task<Menu> GetMenuItemById(int id);
        Task<bool> CreateMenuItem(Menu newMenuItem);
        Task<bool> UpdateMenuItem(int id, Menu updatedMenuItem);
        Task<bool> DeleteMenuItem(int id);
    }
}
