using BLL.Interfaces.IService;
using BLL.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace LMS.PL.Controllers
{
    [Authorize(Roles = "Admin , User")]
    public class CategoryController : Controller
    {

        private readonly ICategoryService _categoryService;

        public CategoryController(ICategoryService categoryService)
        {
            _categoryService = categoryService;
        }

        public async Task<IActionResult> Index()
        {
            var categories = await _categoryService.GetAllCategoriesAsync();
            var activeCategories = categories.Where(c => c.IsActive).ToList();
            return View(activeCategories);
        }

        public async Task<IActionResult> Details(Guid id)
        {
            if (id == Guid.Empty)
                return BadRequest("Invalid category ID.");

            var category = await _categoryService.GetCategoryByIdAsync(id);
            if (category == null)
                return NotFound("Category not found.");

            return View(category);
        }

        [HttpGet, Authorize(Roles = "Admin")]
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost, Authorize(Roles = "Admin")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(CategoryDTO model)
        {
            if (!ModelState.IsValid)
                return View(model);

            model.Id = Guid.NewGuid();
            model.CreatedOn = DateTime.Now;
            model.IsActive = true;

            var result = await _categoryService.AddCategoryAsync(model);
            if (result)
            {
                TempData["Success"] = "Category added successfully!";
                return RedirectToAction(nameof(Index));
            }

            ModelState.AddModelError("", "Error adding category.");
            return View(model);
        }

        [HttpGet, Authorize(Roles = "Admin")]
        public async Task<IActionResult> Edit(Guid id)
        {
            if (id == Guid.Empty)
                return BadRequest("Invalid category ID.");

            var category = await _categoryService.GetCategoryByIdAsync(id);
            if (category == null)
                return NotFound("Category not found.");

            return View(category);
        }

        [HttpPost, Authorize(Roles = "Admin")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(CategoryDTO model)
        {
            if (!ModelState.IsValid)
                return View(model);

            var updated = await _categoryService.UpdateCategoryAsync(model);
            if (updated)
            {
                TempData["Success"] = "Category updated successfully!";
                return RedirectToAction(nameof(Index));
            }

            ModelState.AddModelError("", "Error updating category.");
            return View(model);
        }

        [HttpGet, Authorize(Roles = "Admin")]
        public async Task<IActionResult> Delete(Guid id)
        {
            if (id == Guid.Empty)
                return BadRequest("Invalid category ID.");

            var category = await _categoryService.GetCategoryByIdAsync(id);
            if (category == null)
                return NotFound("Category not found.");

            return View(category);
        }

        [HttpPost, Authorize(Roles = "Admin")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Delete(CategoryDTO model)
        {
            if (model == null)
                return BadRequest();

            var result = await _categoryService.DeleteCategoryAsync(model);
            if (result)
            {
                TempData["Success"] = "Category deleted successfully!";
                return RedirectToAction(nameof(Index));
            }

            ModelState.AddModelError("", "Error deleting category.");
            return View(model);
        }

        public async Task<IActionResult> BooksInCategory(Guid id)
        {
            if (id == Guid.Empty)
                return BadRequest();

            var books = await _categoryService.GetBooksInCategoryAsync(id);
            var category = await _categoryService.GetCategoryByIdAsync(id);

            if (category == null)
                return NotFound("Category not found.");

            ViewBag.Category = category;
            return View(books);
        }

     
    }
}
