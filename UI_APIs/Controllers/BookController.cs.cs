
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
    public class BookController : BaseController
    {
        private readonly IBooksService _booksService;
        private readonly ICategoryService _categoryService;

        public BookController(IBooksService booksService, ICategoryService categoryService)
        {
            _booksService = booksService;
            _categoryService = categoryService;
        }


        [HttpGet("/api/getallbooks")]
        public async Task<IActionResult> Get()
        {
           
            try
            {
                var data = await _booksService.GetAllBooksAsync();
                return Ok(ApiResponse<IEnumerable<BookDTO>>.Succeded(data));
            }
            catch (Exception ex)
            {
                ExceptionLogger.Logs(ex.Message);
                return NotFound(ApiResponse<string>.Fail(ex.Message));
            }
        }

        [HttpGet("/api/getbookbyid/{id}")]
        public async Task<IActionResult> Get(Guid id)
        {

            try
            {
                var data =  await _booksService.GetBooksByIdAsync(id);
                return Ok(ApiResponse<BookDTO>.Succeded(data));
            }
            catch (Exception ex)
            {
                ExceptionLogger.Logs(ex.Message);
                return NotFound(ApiResponse<string>.Fail(ex.Message));
            }
        }





        [HttpPost("/api/createbook")]
        public async Task<IActionResult> Post([FromBody] BookDTO model)
        {
            try
            {
                model.Id = Guid.NewGuid();
                model.CreatedOn = DateTime.UtcNow;
                model.IsActive = true;

                await _booksService.AddBookAsync(model);

                return Ok(ApiResponse<string>.Succeded("Book created successfully."));
            }
            catch (Exception ex)
            {
                ExceptionLogger.Logs(ex.Message);
                return NotFound(ApiResponse<string>.Fail(ex.Message));
            }
        }



        [HttpPut("/api/editbook")]
        public async Task<IActionResult> Put(BookDTO model)
        {
  
            try
            {
                await _booksService.UpdateBookAsync(model);
                return Ok(ApiResponse<string>.Succeded("data saved"));
            }
            catch (Exception ex)
            {

                ExceptionLogger.Logs(ex.Message);
                return NotFound(ApiResponse<string>.Fail(ex.Message));
            }
        }



        [HttpDelete("/api/deletebook")]
        public async Task<IActionResult> Delete(BookDTO model)
        {
            try
            {
                await _booksService.DeleteBookAsync(model);
                return Ok(ApiResponse<string>.Succeded("data deleted"));
            }
            catch (Exception ex)
            {

                ExceptionLogger.Logs(ex.Message);
                return NotFound(ApiResponse<string>.Fail(ex.Message));
            }
        }

        [HttpGet("/api/topratebook")]
        public async Task<IActionResult> TopRated()
        {
            try
            {
               var data = await _booksService.GetTopRatingBookAsync();
                return Ok(ApiResponse<BookDTO>.Succeded(data));
            }
            catch (Exception ex)
            {

                ExceptionLogger.Logs(ex.Message);
                return NotFound(ApiResponse<string>.Fail(ex.Message));
            }
        }

        [HttpGet("/api/lowratebook")]
        public async Task<IActionResult> LowRated()
        {
          
            try
            {
               var data = await _booksService.GetLowRatingBookAsync();
                return Ok(ApiResponse<BookDTO>.Succeded(data));
            }
            catch (Exception ex)
            {

                ExceptionLogger.Logs(ex.Message);
                return NotFound(ApiResponse<string>.Fail(ex.Message));
            }
        }

        [HttpGet("/api/topborrow")]
        public async Task<IActionResult> TopBorrowed()
        {
            try
            {
               var data =  await _booksService.GetTopBorrowedBookAsync();
                return Ok(ApiResponse<BookDTO>.Succeded(data));
            }
            catch (Exception ex)
            {

                ExceptionLogger.Logs(ex.Message);
                return NotFound(ApiResponse<string>.Fail(ex.Message));
            }
        }

        [HttpGet("/api/topreserv")]
        public async Task<IActionResult> TopReserved()
        {
            try
            {
               var data =  await _booksService.GetTopReservedBookAsync();
                return Ok(ApiResponse<BookDTO>.Succeded(data));
            }
            catch (Exception ex)
            {

                ExceptionLogger.Logs(ex.Message);
                return NotFound(ApiResponse<string>.Fail(ex.Message));
            }
        }
    }
}
