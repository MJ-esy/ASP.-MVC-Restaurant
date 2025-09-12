namespace ASP.MVC.Models
{
    public class Menu
    {
        public int DishId { get; set; }
        public string DishName { get; set; }
        public decimal Price { get; set; }
        public string? Description { get; set; }
        public bool IsPopular { get; set; }
        public string Category { get; set; }
        public string Allergen { get; set; }
        public string? ImageUrl { get; set; }


    }
}
