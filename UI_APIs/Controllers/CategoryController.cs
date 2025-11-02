using BLL.Helper;
using BLL.Interfaces.IService;
using BLL.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Linq;
using System.Threading.Tasks;
using UI_APIs.Controllers;

namespace LMS.PL.Controllers
{
    [Authorize(Roles = "Admin , User")]
    public class CategoryController : BaseController
    {

        private readonly ICategoryService _categoryService;

        public CategoryController(ICategoryService categoryService)
        {
            _categoryService = categoryService;
        }

        [HttpGet("/api/getallcategory")]
        public async Task<IActionResult> Get()
        {

            try
            {
                var data = await _categoryService.GetAllCategoriesAsync();
                return Ok(ApiResponse<IEnumerable<CategoryDTO>>.Succeded(data));
            }
            catch (Exception ex)
            {
                ExceptionLogger.Logs(ex.Message);
                return NotFound(ApiResponse<string>.Fail(ex.Message));
            }
        }


        [HttpGet("/api/getcategorybyid/{id}")]
        public async Task<IActionResult> Get(Guid id)
        {

            try
            {
                var data = await _categoryService.GetCategoryByIdAsync(id);
                return Ok(ApiResponse<CategoryDTO>.Succeded(data));
            }
            catch (Exception ex)
            {
                ExceptionLogger.Logs(ex.Message);
                return NotFound(ApiResponse<string>.Fail(ex.Message));
            }
        }



        [HttpPost("/api/createcategory")]
        public async Task<IActionResult> Post([FromBody] CategoryDTO model)
        {
            try
            {
                model.Id = Guid.NewGuid();
                model.CreatedOn = DateTime.Now;
                model.IsActive = true;

                var result = await _categoryService.AddCategoryAsync(model);

                return Ok(ApiResponse<string>.Succeded("Book created successfully."));
            }
            catch (Exception ex)
            {
                ExceptionLogger.Logs(ex.Message);
                return NotFound(ApiResponse<string>.Fail(ex.Message));
            }
        }




        [HttpPut("/api/editcategory")]
        public async Task<IActionResult> Put(CategoryDTO model)
        {

            try
            {
                await _categoryService.UpdateCategoryAsync(model);
                return Ok(ApiResponse<string>.Succeded("data saved"));
            }
            catch (Exception ex)
            {

                ExceptionLogger.Logs(ex.Message);
                return NotFound(ApiResponse<string>.Fail(ex.Message));
            }
        }




        [HttpDelete("/api/deletecategory")]
        public async Task<IActionResult> Delete(CategoryDTO model)
        {
            try
            {
                await _categoryService.DeleteCategoryAsync(model);
                return Ok(ApiResponse<string>.Succeded("data deleted"));
            }
            catch (Exception ex)
            {

                ExceptionLogger.Logs(ex.Message);
                return NotFound(ApiResponse<string>.Fail(ex.Message));
            }
        }

        [HttpGet("/api/booksincategory/{id}")]
        public async Task<IActionResult> BooksInCategory(Guid id)
        {
            try
            {
              

                var category = await _categoryService.GetCategoryByIdAsync(id);

                var books = await _categoryService.GetBooksInCategoryAsync(id);

                return Ok(ApiResponse<object>.Succeded(new
                {
                    Category = category,
                    Books = books
                }));
            }
            catch (Exception ex)
            {
                ExceptionLogger.Logs(ex.Message);
                return StatusCode(500, ApiResponse<string>.Fail("An error occurred while fetching data."));
            }
        }



    }
}
