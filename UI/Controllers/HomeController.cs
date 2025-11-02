using BLL.Interfaces.IService;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace LMS.PL.Controllers
{
    [Authorize(Roles = "Admin , User")]
    public class HomeController : Controller
    {
        private readonly IBorrowingTransactionService _transactionService;
        private readonly IBooksService _booksService;
        private readonly IMembersService _membersService;
        private readonly IReservationTransactionService _reservationService;
        public HomeController(IBooksService booksService,IMembersService membersService , IReservationTransactionService reservationTransactionService , IBorrowingTransactionService borrowingTransactionService) 
        {
            _booksService = booksService;
            _membersService = membersService;
            _reservationService = reservationTransactionService;
            _transactionService = borrowingTransactionService;

        }
        public async Task<IActionResult> Index()
        {
            ViewBag.TotalBooks = await _booksService.GetAllBooksAsync().ContinueWith(t => t.Result.Count());
            ViewBag.TotalMembers = await _membersService.GetAllMembersAsync().ContinueWith(t => t.Result.Count());
            ViewBag.TotalReservations = await _reservationService.GetAllReservationsAsync().ContinueWith(t => t.Result.Count());
            ViewBag.TotalBorrowings = await _transactionService.GetAllBorrowTransactionsAsync().ContinueWith(t => t.Result.Count());

            ViewBag.RecentReservations = (await _reservationService.GetAllReservationsAsync())
                                         .OrderByDescending(r => r.ReservationDate)
                                         .Take(5)
                                         .ToList();

            return View();
        }

    }
}
