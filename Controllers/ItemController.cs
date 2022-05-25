using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using DotnetMvc.Models;
using DotnetMvc.Repository;
using Microsoft.AspNetCore.Mvc.Rendering;
using DotnetMvc.ViewModels;

namespace DotnetMvc.Controllers;

public class ItemController : Controller
{
    private readonly ItemRepository _itemRepository;
    private readonly CategoryRepository _categoryRepository;
    private readonly IWebHostEnvironment _webHostEnvironment;
    public ItemController(ItemRepository itemRepository, CategoryRepository categoryRepository, IWebHostEnvironment webHostEnvironment)
    {
        _itemRepository = itemRepository;
        _categoryRepository = categoryRepository;
        _webHostEnvironment = webHostEnvironment;
    }


    public async Task<IActionResult> Index(bool isDeleted = false, bool isCreated = false, bool isUpdated = false)
    {
        var items = await _itemRepository.GetAllItems();
        if (isDeleted == true)
        {
            ViewBag.IsDeleted = isDeleted;
            ViewBag.Message = "Item Deleted Successfully";
        }

        if (isCreated == true)
        {
            ViewBag.IsCreated = isCreated;
            ViewBag.Message = "Item Created Successfully";
        }

        if (isUpdated == true)
        {
            ViewBag.IsUpdated = isUpdated;
            ViewBag.Message = "Item Updated Successfully";
        }
        return View(items);
    }

    public async Task<IActionResult> Details(int id)
    {
        var item = await _itemRepository.GetItemById(id);
        return View(item);
    }

    public async Task<IActionResult> Delete(int id)
    {
        var item = await _itemRepository.GetItemById(id);
        return View(item);
    }

    [HttpPost]
    public async Task<IActionResult> Delete(int id, Item item)
    {
        await _itemRepository.DeleteItem(id);
        return RedirectToAction("Index", new { isDeleted = true });
    }

    public async Task<IActionResult> Create()
    {
        var categories = await _categoryRepository.GetAllCategories();
        ViewBag.Categories = categories.Select(c => new SelectListItem
        {
            Text = c.Name,
            Value = c.Id.ToString()
        });

        return View();
    }

    [HttpPost]
    public async Task<IActionResult> Create(ItemVM item)
    {
        if (ModelState.IsValid)
        {
            if (item.Image != null)
            {
                string folder = "items/image";
                string filePath = await UploadImage(folder, item.Image);
                item.ImageUrl = filePath;
            }
            else
            {
                item.ImageUrl = "/items/image\\default.jpg";
            }

            await _itemRepository.CreateItem(item);
            return RedirectToAction("Index", new { isCreated = true });
        }

        var categories = await _categoryRepository.GetAllCategories();
        ViewBag.Categories = categories.Select(c => new SelectListItem
        {
            Text = c.Name,
            Value = c.Id.ToString()
        });

        return View(item);
    }

    public async Task<IActionResult> Edit(int id)
    {
        var item = await _itemRepository.GetItemById(id);
        var categories = await _categoryRepository.GetAllCategories();
        ViewBag.Categories = categories.Select(c => new SelectListItem
        {
            Text = c.Name,
            Value = c.Id.ToString()
        });

        return View(item);
    }

    [HttpPost]
    public async Task<IActionResult> Edit(int id, ItemVM item)
    {
        if (id != item.Id)
        {
            return NotFound();
        }

        if (ModelState.IsValid)
        {
            if (item.Image != null)
            {
                string folder = "items/image";
                string filePath = await UploadImage(folder, item.Image);
                item.ImageUrl = filePath;
            }



            await _itemRepository.UpdateItem(item);
            return RedirectToAction("Index", new { isUpdated = true });
        }
        return View(item);
    }

    private async Task<string> UploadImage(string folderPath, IFormFile file)
    {
        string fileName = Guid.NewGuid().ToString() + "_" + file.FileName;
        string serverFolder = Path.Combine(_webHostEnvironment.WebRootPath, folderPath, fileName);
        string filePath = Path.Combine(folderPath, fileName);

        using (var stream = new FileStream(serverFolder, FileMode.Create))
        {
            await file.CopyToAsync(stream);
        }

        return "/" + filePath;
    }

}