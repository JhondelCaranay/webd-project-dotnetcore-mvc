using DotnetMvc.Repository;
using DotnetMvc.ViewModels;
using Microsoft.AspNetCore.Mvc;

namespace DotnetMvc.Controllers;


public class CategoryController : Controller
{
    private readonly CategoryRepository _categoryRepository;
    public CategoryController(CategoryRepository categoryRepository)
    {
        _categoryRepository = categoryRepository;
    }

    public async Task<IActionResult> Index(bool isCreated = false)
    {
        var categories = await _categoryRepository.GetAllCategories();

        if (isCreated == true)
        {
            ViewBag.IsCreated = isCreated;
            ViewBag.Message = "Item Created Successfully";
        }


        return View(categories);
    }

    public async Task<IActionResult> Edit(int id)
    {
        var category = await _categoryRepository.GetCategoryById(id);
        return View(category);
    }

    [HttpPost]
    public async Task<IActionResult> Edit(int id, CategoryVM categoryVM)
    {
        if (id != categoryVM.Id)
        {
            return NotFound();
        }

        if (ModelState.IsValid)
        {
            await _categoryRepository.UpdateCategory(categoryVM);
            return RedirectToAction("Index", new { isUpdated = true });
        }
        return View(categoryVM);
    }

    public IActionResult create()
    {
        return View();
    }

    [HttpPost]
    public async Task<IActionResult> create(CategoryVM categoryVM)
    {
        if (ModelState.IsValid)
        {
            await _categoryRepository.CreateCategory(categoryVM);
            return RedirectToAction("Index", new { isCreated = true });
        }
        return View(categoryVM);
    }

    public async Task<IActionResult> Delete(int id)
    {
        var category = await _categoryRepository.GetCategoryById(id);
        return View(category);
    }

    [HttpPost]
    public async Task<IActionResult> Delete(int id, CategoryVM categoryVM)
    {
        await _categoryRepository.DeleteCategory(id);
        return RedirectToAction("Index", new { isDeleted = true });
    }
}