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
    public class ReservationController : Controller
    {
        private readonly IReservationTransactionService _reservationService;
        private readonly ICategoryService _categoryService;
        private readonly IBooksService _booksService;
        private readonly IMembersService _membersService;

        public ReservationController(IReservationTransactionService reservationService , ICategoryService categoryService, IMembersService membersService, IBooksService booksService)
        {
            _reservationService = reservationService;
            _categoryService = categoryService;
            _membersService = membersService;
            _booksService = booksService;
        }

        public async Task<IActionResult> Index()
        {
            var reservations = await _reservationService.GetAllReservationsAsync();
            return View(reservations);
        }

        public async Task<IActionResult> Create()
        {
            var books = await _booksService.GetAllBooksAsync() ?? new List<BookDTO>();
            var members = await _membersService.GetAllMembersAsync() ?? new List<MemberDTO>();
            var categories = await _categoryService.GetAllCategoriesAsync() ?? new List<CategoryDTO>();

            ViewBag.Books = new SelectList(books, "Id", "Title");
            ViewBag.Members = new SelectList(members, "Id", "FullName");
            ViewBag.Categories = new SelectList(categories, "Id", "CategoryName");

            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Reserve(Guid bookId, Guid memberId)
        {
            if (bookId == Guid.Empty || memberId == Guid.Empty)
            {
                TempData["ErrorMessage"] = "Book ID and Member ID are required.";
                return RedirectToAction(nameof(Create));
            }

            try
            {
                await _reservationService.ReserveBookAsync(bookId, memberId);
                TempData["SuccessMessage"] = "Book reserved successfully!";
                return RedirectToAction(nameof(Index));
            }
            catch (Exception ex)
            {
                TempData["ErrorMessage"] = $"Error reserving book: {ex.Message}";
                return RedirectToAction(nameof(Create));
            }
        }

        [HttpGet]
        public async Task<JsonResult> AjaxCall_GetBooksByCategory(Guid CatId)
        {
            var result = await _categoryService.GetBooksInCategoryAsync(CatId);
            return Json(result);
        }

       


        public  async Task<IActionResult> Expire(Guid Id)
        {
            await _reservationService.ExpireReservationAsync(Id);
            return RedirectToAction(nameof(Index));
        }

        [HttpGet]
        public async Task<IActionResult> Cancel(Guid Id)
        {
            var transaction = await _reservationService.GetReservationByIdAsync(Id);
            if (transaction == null)
            {
                TempData["ErrorMessage"] = "Reservation not found!";
                return RedirectToAction(nameof(Index));
            }
            return View(transaction);
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Cancel(ReservationTransactionDTO model)
        {
            await _reservationService.DeleteReservationAsync(model);
            return RedirectToAction(nameof(Index));

        }
    }
}