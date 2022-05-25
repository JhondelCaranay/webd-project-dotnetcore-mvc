

using DotnetMvc.Data;
using DotnetMvc.Models;
using DotnetMvc.ViewModels;
using Microsoft.EntityFrameworkCore;

namespace DotnetMvc.Repository;


public class ItemRepository
{
    private readonly AppDbContext _context = null;
    public ItemRepository(AppDbContext context)
    {
        _context = context;
    }

    public async Task<List<ItemVM>> GetAllItems()
    {
        //get all items order by descending
        var items = await _context.Items.OrderByDescending(i => i.Id).Select(i => new ItemVM
        {
            Id = i.Id,
            Name = i.Name,
            Description = i.Description,
            Price = i.Price,
            CategoryID = i.CategoryID,
            CategoryName = i.Category.Name,
            ImageUrl = i.ImageUrl
        }).ToListAsync();

        return items;


        // var items = await _context.Items.Select(i => new ItemVM
        // {
        //     Id = i.Id,
        //     Name = i.Name,
        //     Description = i.Description,
        //     Price = i.Price,
        //     CategoryID = i.CategoryID,
        //     CategoryName = i.Category.Name
        // }).ToListAsync();

        // return items;
        // var items = await _context.Items.ToListAsync();

        // return items.Select(i => new ItemVM
        // {
        //     Id = i.Id,
        //     Name = i.Name,
        //     Price = i.Price,
        //     Description = i.Description,
        //     CategoryID = i.CategoryID,
        //     CategoryName = i.Category.Name
        // }).ToList();
    }

    public async Task<ItemVM> GetItemById(int id)
    {
        var item = await _context.Items.Where(i => i.Id == id).Select(i => new ItemVM
        {
            Id = i.Id,
            Name = i.Name,
            Price = i.Price,
            Description = i.Description,
            CategoryID = i.CategoryID,
            CategoryName = i.Category.Name,
            ImageUrl = i.ImageUrl
        }).FirstOrDefaultAsync();

        return item;
    }

    public async Task<Item> DeleteItem(int id)
    {
        var item = await _context.Items.FindAsync(id);
        _context.Items.Remove(item);
        await _context.SaveChangesAsync();
        return item;
    }

    public async Task<Item> CreateItem(ItemVM item)
    {

        var newItem = new Item
        {
            Name = item.Name,
            Price = item.Price,
            Description = item.Description,
            CategoryID = item.CategoryID,
            ImageUrl = item.ImageUrl
        };

        await _context.Items.AddAsync(newItem);
        await _context.SaveChangesAsync();
        return newItem;
    }


    public async Task<Item> UpdateItem(ItemVM item)
    {
        var updatedItem = new Item
        {
            Id = item.Id,
            Name = item.Name,
            Price = item.Price,
            Description = item.Description,
            CategoryID = item.CategoryID,
            ImageUrl = item.ImageUrl
        };

        _context.Items.Update(updatedItem);
        await _context.SaveChangesAsync();
        return updatedItem;
    }
}