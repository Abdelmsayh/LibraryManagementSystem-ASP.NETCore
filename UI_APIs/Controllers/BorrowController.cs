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
    public class BorrowController : BaseController
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

        [HttpGet("/api/getallBorrow")]
        public async Task<IActionResult> Get()
        {
         
            try
            {
                var data = await _borrowingService.GetAllBorrowTransactionsAsync();
                return Ok(ApiResponse<IEnumerable<BorrowingTransactionDTO>>.Succeded(data));
            }
            catch (Exception ex)
            {
                ExceptionLogger.Logs(ex.Message);
                return NotFound(ApiResponse<string>.Fail(ex.Message));
            }
        }

        [HttpGet("/api/getborrowbyid/{id}")]
        public async Task<IActionResult> Get(Guid id)
        {
            try
            {
                var data = await _borrowingService.GetBorrowTransactionByIdAsync(id);
                return Ok(ApiResponse<BorrowingTransactionDTO>.Succeded(data));
            }
            catch (Exception ex)
            {
                ExceptionLogger.Logs(ex.Message);
                return NotFound(ApiResponse<string>.Fail(ex.Message));
            }
        }


        [HttpPost("/api/createborrow")]
        public async Task<IActionResult> Post(BorrowingTransactionDTO transactionDTO) 
        {
            
            try
            {
                await _borrowingService.AddBorrowTransactionAsync(transactionDTO);

                return Ok(ApiResponse<string>.Succeded("Book created successfully."));
            }
            catch (Exception ex)
            {
                ExceptionLogger.Logs(ex.Message);
                return NotFound(ApiResponse<string>.Fail(ex.Message));
            }
        }

      

     

        [HttpDelete("/api/deleteborrow")]
        public async Task<IActionResult> Delete(BorrowingTransactionDTO model)
        {
            try
            {
                await _borrowingService.DeleteBorrowTransactionAsync(model);
                return Ok(ApiResponse<string>.Succeded("data deleted"));
            }
            catch (Exception ex)
            {

                ExceptionLogger.Logs(ex.Message);
                return NotFound(ApiResponse<string>.Fail(ex.Message));
            }
        }

    


        [HttpPost("/api/returnbook")]
        public async Task<IActionResult> Return(Guid transactionId)
        {

            try
            {
                await _borrowingService.ReturnBookAsync(transactionId);
                return Ok(ApiResponse<string>.Succeded("data returnd"));
            }
            catch (Exception ex)
            {

                ExceptionLogger.Logs(ex.Message);
                return NotFound(ApiResponse<string>.Fail(ex.Message));
            }
        }

        [HttpGet("/api/getalloverdue")]
        public async Task<IActionResult> Overdue()
        {
            try
            {
                var overdue = await _borrowingService.GetOverdueTransactionsAsync();
                return Ok(ApiResponse<List<BorrowingTransactionDTO>>.Succeded(overdue));
            }
            catch (Exception ex)
            {

                ExceptionLogger.Logs(ex.Message);
                return NotFound(ApiResponse<string>.Fail(ex.Message));
            }
        }

        [HttpGet("/api/getfinebyid/{transactionId}")]
        public async Task<IActionResult> CalculateFine(Guid transactionId)
        {
            try
            {
                var fine = await _borrowingService.CalculateFineAsync(transactionId);
                return Ok(ApiResponse<decimal>.Succeded(fine));
            }
            catch (Exception ex)
            {

                ExceptionLogger.Logs(ex.Message);
                return NotFound(ApiResponse<string>.Fail(ex.Message));
            }
        }

       

    }
}
