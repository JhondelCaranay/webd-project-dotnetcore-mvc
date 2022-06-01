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
        var categories = await _context.Category.OrderByDescending(c => c.Name).Select(c => new CategoryVM
        {
            Id = c.Id,
            Name = c.Name
        }).ToListAsync();

        return categories;
    }

    public async Task<CategoryVM> GetCategoryById(int id)
    {
        var category = await _context.Category.Where(c => c.Id == id).Select(c => new CategoryVM
        {
            Id = c.Id,
            Name = c.Name
        }).FirstOrDefaultAsync();

        return category;
    }

    public async Task<Category> CreateCategory(CategoryVM categoryVM)
    {
        var category = new Category
        {
            Name = categoryVM.Name
        };

        _context.Category.Add(category);
        await _context.SaveChangesAsync();

        return category;
    }

    public async Task<Category> UpdateCategory(CategoryVM categoryVM)
    {
        var category = new Category
        {
            Id = categoryVM.Id,
            Name = categoryVM.Name
        };

        _context.Category.Update(category);
        await _context.SaveChangesAsync();
        return category;
    }

    public async Task<Category> DeleteCategory(int id)
    {
        var category = await _context.Category.FindAsync(id);
        _context.Category.Remove(category);
        await _context.SaveChangesAsync();
        return category;
    }


}