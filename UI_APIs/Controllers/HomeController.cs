using BLL.Helper;
using BLL.Interfaces.IService;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using UI_APIs.Controllers;

namespace LMS.API.Controllers
{

    [Authorize(Roles = "Admin , User")]
    public class HomeController : BaseController
    {
        private readonly IBorrowingTransactionService _transactionService;
        private readonly IBooksService _booksService;
        private readonly IMembersService _membersService;
        private readonly IReservationTransactionService _reservationService;

        public HomeController(
            IBooksService booksService,
            IMembersService membersService,
            IReservationTransactionService reservationTransactionService,
            IBorrowingTransactionService borrowingTransactionService)
        {
            _booksService = booksService;
            _membersService = membersService;
            _reservationService = reservationTransactionService;
            _transactionService = borrowingTransactionService;
        }

        [HttpGet("/api/getdashboard")]
        public async Task<IActionResult> GetDashboard()
        {
            try
            {
                var totalBooks = (await _booksService.GetAllBooksAsync()).Count();
                var totalMembers = (await _membersService.GetAllMembersAsync()).Count();
                var totalReservations = (await _reservationService.GetAllReservationsAsync()).Count();
                var totalBorrowings = (await _transactionService.GetAllBorrowTransactionsAsync()).Count();

                var recentReservations = (await _reservationService.GetAllReservationsAsync())
                    .OrderByDescending(r => r.ReservationDate)
                    .Take(5)
                    .ToList();

                var dashboard = new
                {
                    TotalBooks = totalBooks,
                    TotalMembers = totalMembers,
                    TotalReservations = totalReservations,
                    TotalBorrowings = totalBorrowings,
                    RecentReservations = recentReservations
                };

                return Ok(ApiResponse<object>.Succeded(dashboard));
            }
            catch (Exception ex)
            {
                ExceptionLogger.Logs(ex.Message);
                return BadRequest(ApiResponse<string>.Fail(ex.Message));
            }
        }
    }
}
