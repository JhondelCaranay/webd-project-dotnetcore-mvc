using DotnetMvc.Data;
using DotnetMvc.Models;
using DotnetMvc.ViewModels;
using Microsoft.EntityFrameworkCore;

namespace DotnetMvc.Repository;


public class CategoryRepository
{
    private readonly AppDbContext _context = null;
    public CategoryRepository(AppDbContext context)
    {
        _context = context;
    }

    public async Task<List<CategoryVM>> GetAllCategories()
    {
        var categories = await _context.Category.Select(c => new CategoryVM
        {
            Id = c.Id,
            Name = c.Name
        }).ToListAsync();

        return categories;
    }


}