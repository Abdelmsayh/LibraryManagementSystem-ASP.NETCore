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
    public class BorrowController : Controller
    {
        private readonly IBooksService _booksService;
        private readonly IBorrowingTransactionService _borrowingService;
        private readonly IMembersService _membersService;
        private readonly ICategoryService _categoryService;


        public BorrowController(IBooksService booksService, IBorrowingTransactionService borrowingService , IMembersService membersService, ICategoryService categoryService)
        {
            _booksService = booksService;
            _borrowingService = borrowingService;
            _membersService = membersService;
            _categoryService = categoryService;
        }

        public async Task<IActionResult> Index()
        {
            var transactions = await _borrowingService.GetAllBorrowTransactionsAsync();
            return View(transactions);
        }

        [HttpGet]
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
        public async Task<IActionResult> Create(BorrowingTransactionDTO transactionDTO) 
        {
            if (ModelState.IsValid)
            {
                bool success = await _borrowingService.AddBorrowTransactionAsync(transactionDTO);

                if (success)
                {
                    return RedirectToAction(nameof(Index));
                }
            }

            var books = await _booksService.GetAllBooksAsync() ?? new List<BookDTO>();
            var members = await _membersService.GetAllMembersAsync() ?? new List<MemberDTO>();
            var categories = await _categoryService.GetAllCategoriesAsync() ?? new List<CategoryDTO>();
            ViewBag.Books = new SelectList(books, "Id", "Title", transactionDTO.BookId);
            ViewBag.Members = new SelectList(members, "Id", "FullName", transactionDTO.MemberId);
            ViewBag.Categories = new SelectList(categories, "Id", "CategoryName");

            return View(transactionDTO);
        }

        [HttpGet]
        public async Task<JsonResult> AjaxCall_GetBooksByCategory(Guid CatId)
        {
            var result = await _categoryService.GetBooksInCategoryAsync(CatId);
            return Json(result);
        }

        [HttpGet]
        public async Task<IActionResult> Delete(Guid id)
        {
            var transaction = await _borrowingService.GetBorrowTransactionByIdAsync(id);
            return View(transaction);
        }

        [HttpPost, ActionName("Delete")]
        public async Task<IActionResult> DeleteConfirmed(BorrowingTransactionDTO model)
        {
             await _borrowingService.DeleteBorrowTransactionAsync(model);
            return RedirectToAction(nameof(Index));
        }

        public async Task<IActionResult> Details(Guid id)
        {
            var transaction = await _borrowingService.GetBorrowTransactionByIdAsync(id);
            var members = await _membersService.GetAllMembersAsync() ?? new List<MemberDTO>();
            ViewBag.Members = new SelectList(members, "Id", "FullName");


            return View(transaction);
        }

        [HttpGet]
        public async Task<IActionResult> ReturnBook(Guid transactionId) 
        {
            var transaction = await _borrowingService.GetBorrowTransactionByIdAsync(transactionId);
            var members = await _membersService.GetAllMembersAsync() ?? new List<MemberDTO>();
            ViewBag.Members = new SelectList(members, "Id", "FullName");

            if (transaction == null || transaction.IsReturned)
            {
                return RedirectToAction(nameof(Index));
            }

            return View(transaction);
        }

        [HttpPost, ActionName("ReturnBook")]
        public async Task<IActionResult> ConvermReturnBook(Guid transactionId)
        {
            await _borrowingService.ReturnBookAsync(transactionId);
            return RedirectToAction(nameof(Index));
        }

        public async Task<IActionResult> Overdue()
        {
            var overdue = await _borrowingService.GetOverdueTransactionsAsync();
            var members = await _membersService.GetAllMembersAsync() ?? new List<MemberDTO>();

            ViewBag.Members = new SelectList(members, "Id", "FullName");
            return View(overdue);
        }

        public async Task<IActionResult> ByMember(Guid memberId)
        {
            var books = await _borrowingService.GetBorrowedBooksByMemberAsync(memberId);
            return View(books);
        }

        public async Task<IActionResult> CalculateFine(Guid transactionId)
        {
            var fine = await _borrowingService.CalculateFineAsync(transactionId);
            ViewBag.Fine = fine;
            return View();
        }

       

    }
}
