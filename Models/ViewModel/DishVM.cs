using System.ComponentModel.DataAnnotations;

namespace ASP.MVC.Models.ViewModel
{
    public class DishVM
    {
        [Display(Name = "Dish Name")]
        [Required(ErrorMessage = "Dish Name is required.")]
        public string DishName { get; set; }

        [Range(0.5, 1000, ErrorMessage = "Price must be between 0.5 and 1000.")]
        public decimal Price { get; set; }

        public string? Description { get; set; }
        public bool IsPopular { get; set; }

        [Required(ErrorMessage = "Category is required.")]
        public string Category { get; set; }

        public string Allergen { get; set; }
        public string? ImageUrl { get; set; }
    }
}
