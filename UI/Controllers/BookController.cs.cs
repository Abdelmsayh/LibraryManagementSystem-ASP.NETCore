
using BLL.Interfaces.IService;
using BLL.Models;
using BLL.Service;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Threading.Tasks;

namespace LMS.PL.Controllers
{
    [Authorize(Roles = "Admin , User")]
    public class BookController : Controller
    {
        private readonly IBooksService _booksService;
        private readonly ICategoryService _categoryService;

        public BookController(IBooksService booksService, ICategoryService categoryService)
        {
            _booksService = booksService;
            _categoryService = categoryService;
        }


        public async Task<IActionResult> Index()
        {
            var books = await _booksService.GetAllBooksAsync();
            return View(books);
        }

        public async Task<IActionResult> Details(Guid id)
        {
            var book = await _booksService.GetBooksByIdAsync(id);
            if (book == null)
                return NotFound();

            return View(book);
        }


        [Authorize(Roles = "Admin")]

        public async Task<IActionResult> Create()
        {
            var categories = await _categoryService.GetAllCategoriesAsync();
            var activeCategories = categories.Where(c => c.IsActive).ToList(); // نعرض بس الـ Active
            ViewBag.Categories = new SelectList(activeCategories, "Id", "CategoryName");
            return View();
        }

        [HttpPost, Authorize(Roles = "Admin")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(BookDTO model)
        {
            if (ModelState.IsValid)
            {
                model.Id = Guid.NewGuid();
                model.CreatedOn = DateTime.Now;
                model.IsActive = true; 
                await _booksService.AddBookAsync(model);
                return RedirectToAction(nameof(Index));
            }

           
            var categories = await _categoryService.GetAllCategoriesAsync();
            var activeCategories = categories.Where(c => c.IsActive).ToList();
            ViewBag.Categories = new SelectList(activeCategories, "Id", "CategoryName", model.CategoryId);

            return View(model);
        }

        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Edit(Guid id)
        {
            var book = await _booksService.GetBooksByIdAsync(id);
            if (book == null)
                return NotFound();

            var categories = await _categoryService.GetAllCategoriesAsync();
            ViewBag.Categories = new SelectList(categories, "Id", "CategoryName", book.CategoryId);

            return View(book);
        }

        [HttpPost, Authorize(Roles = "Admin")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(BookDTO model)
        {
            if (ModelState.IsValid)
            {
                await _booksService.UpdateBookAsync(model);
                return RedirectToAction(nameof(Index));
            }

            var categories = await _categoryService.GetAllCategoriesAsync();
            ViewBag.Categories = new SelectList(categories, "Id", "CategoryName", model.CategoryId);
            return View(model);
        }

        [Authorize(Roles = "Admin")]
        [HttpGet]
        public async Task<IActionResult> Delete(Guid id)
        {
            var book = await _booksService.GetBooksByIdAsync(id);
            if (book == null)
                return NotFound();

            return View(book);
        }

        [HttpPost, Authorize(Roles = "Admin")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Delete(BookDTO model)
        {
            if (model == null)
                return BadRequest();

            var result = await _booksService.DeleteBookAsync(model);
            if (result)
            {
                TempData["Success"] = "Category deleted successfully!";
                return RedirectToAction(nameof(Index));
            }

            ModelState.AddModelError("", "Error deleting category.");
            return View(model);
        }

    
        public async Task<IActionResult> TopRated()
        {
            var book = await _booksService.GetTopRatingBookAsync();
            return View(book);
        }

        public async Task<IActionResult> LowRated()
        {
            var book = await _booksService.GetLowRatingBookAsync();
            return View(book);
        }

        public async Task<IActionResult> TopBorrowed()
        {
            var book = await _booksService.GetTopBorrowedBookAsync();
            return View(book);
        }

        public async Task<IActionResult> TopReserved()
        {
            var book = await _booksService.GetTopReservedBookAsync();
            return View(book);
        }
    }
}
