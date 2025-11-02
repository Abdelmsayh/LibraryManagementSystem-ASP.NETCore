using BLL.Helper;
using BLL.Interfaces.IService;
using BLL.Models;
using BLL.Service;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Threading.Tasks;
using UI_APIs.Controllers;

namespace LMS.PL.Controllers
{
    [Authorize(Roles = "Admin , User")]
    public class ReservationController : BaseController
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

        [HttpGet("/api/getallreservaiont")]
        public async Task<IActionResult> GetAllReservations()
        {
            try
            {
                var reservations = await _reservationService.GetAllReservationsAsync();
                return Ok(ApiResponse<object>.Succeded(reservations));
            }
            catch (Exception ex)
            {
                ExceptionLogger.Logs(ex.Message);
                return StatusCode(500, ApiResponse<string>.Fail("Error fetching reservations."));
            }
        }


        [HttpPost("/api/reserve")]
        public async Task<IActionResult> Reserve(Guid bookId, Guid memberId)
        {
            try
            {
                await _reservationService.ReserveBookAsync(bookId, memberId);
                return Ok(ApiResponse<string>.Succeded("Book reserved successfully."));
            }
            catch (Exception ex)
            {
                ExceptionLogger.Logs(ex.Message);
                return BadRequest(ApiResponse<string>.Fail($"Error reserving book: {ex.Message}"));
            }
        }


        [HttpPut("/api/expire/{id}")]
        public async Task<IActionResult> Expire(Guid id)
        {
            try
            {
                await _reservationService.ExpireReservationAsync(id);
                return Ok(ApiResponse<string>.Succeded("Reservation expired successfully."));
            }
            catch (Exception ex)
            {
                ExceptionLogger.Logs(ex.Message);
                return BadRequest(ApiResponse<string>.Fail($"Error expiring reservation: {ex.Message}"));
            }
        }



        [HttpDelete("/api/cancel")]
        public async Task<IActionResult> Cancel([FromBody] ReservationTransactionDTO model)
        {
            try
            {
                await _reservationService.DeleteReservationAsync(model);
                return Ok(ApiResponse<string>.Succeded("Reservation cancelled successfully."));
            }
            catch (Exception ex)
            {
                ExceptionLogger.Logs(ex.Message);
                return BadRequest(ApiResponse<string>.Fail($"Error cancelling reservation: {ex.Message}"));
            }
        }
    }
}