using System.ComponentModel.DataAnnotations;

namespace DotnetMvc.ViewModels;

public class ItemVM
{
    public int Id { get; set; }
    [Required]
    public string Name { get; set; }
    public string Description { get; set; }

    [Required]
    [Range(0.01, double.MaxValue, ErrorMessage = "Price must be greater than 0")]
    public decimal Price { get; set; }
    [Display(Name = "Category")]
    public int CategoryID { get; set; }
    public string CategoryName { get; set; }
    [Display(Name = "Cover photo")]
    // [Required]
    public IFormFile Image { get; set; }
    public string ImageUrl { get; set; }

}
