namespace DotnetMvc.Models;

public class Item
{
    public int Id { get; set; }
    public string Name { get; set; }
    public string Description { get; set; }
    public decimal Price { get; set; }
    public int CategoryID { get; set; }
    public Category Category { get; set; }
    public string ImageUrl { get; set; }
}
