using BLL.Helper;
using BLL.Interfaces.IService;
using BLL.Models;
using DAL.Extend;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using UI_APIs.Controllers;

namespace LMS.API.Controllers
{
    [Authorize(Roles = "Admin")]
    public class UserController : BaseController
    {
        private readonly IUsersService _userService;
        private readonly IMembersService _membersService;

        public UserController(IUsersService userService, IMembersService membersService)
        {
            _userService = userService;
            _membersService = membersService;
        }

        [HttpGet("/api/getall")]
        public async Task<IActionResult> GetAll()
        {
            try
            {
                var data = await _userService.GetAllAsync();
                return Ok(ApiResponse<List<ApplicationUser>>.Succeded(data));
            }
            catch (Exception ex)
            {
                ExceptionLogger.Logs(ex.Message);
                return BadRequest(ApiResponse<string>.Fail(ex.Message));
            }
        }

        [HttpGet("/api/getby/{id}")]
        public async Task<IActionResult> GetById(string id)
        {
           
            try
            {
                var user = await _userService.GetByIdAsync(id);
                if (user == null)
                    return NotFound(ApiResponse<string>.Fail("User not found."));

                return Ok(ApiResponse<ApplicationUser>.Succeded(user));
            }
            catch (Exception ex)
            {
                ExceptionLogger.Logs(ex.Message);
                return BadRequest(ApiResponse<string>.Fail(ex.Message));
            }
        }

        [HttpPost("/api/create")]
        public async Task<IActionResult> Create([FromBody] ApplicationUser user)
        {
    
            try
            {
                var result = await _userService.AddAsync(user);
                return Ok(ApiResponse<string>.Succeded("User created successfully."));
            }
            catch (Exception ex)
            {
                ExceptionLogger.Logs(ex.Message);
                return BadRequest(ApiResponse<string>.Fail(ex.Message));
            }
        }

        [HttpPut("/api/edit")]
        public async Task<IActionResult> Edit([FromBody] ApplicationUser user)
        {

            try
            {
                var result = await _userService.UpdateAsync(user);
                return Ok(ApiResponse<string>.Succeded("User updated successfully."));
            }
            catch (Exception ex)
            {
                ExceptionLogger.Logs(ex.Message);
                return BadRequest(ApiResponse<string>.Fail(ex.Message));
            }
        }

        [HttpDelete("/api/delete/{id}")]
        public async Task<IActionResult> Delete(string id)
        {
            try
            {
                var user = await _userService.GetByIdAsync(id);
                await _userService.DeleteAsync(user);
                return Ok(ApiResponse<string>.Succeded("User deleted successfully."));
            }
            catch (Exception ex)
            {
                ExceptionLogger.Logs(ex.Message);
                return BadRequest(ApiResponse<string>.Fail(ex.Message));
            }
        }

        [HttpPut("/api/activate/{id}")]
        public async Task<IActionResult> Activate(string id)
        {
            try
            {
                await _userService.ActivateUserAsync(id);
                return Ok(ApiResponse<string>.Succeded("User activated successfully."));
            }
            catch (Exception ex)
            {
                ExceptionLogger.Logs(ex.Message);
                return BadRequest(ApiResponse<string>.Fail(ex.Message));
            }
        }

        [HttpPut("/api/deactivate/{id}")]
        public async Task<IActionResult> Deactivate(string id)
        {
            try
            {
                await _userService.DeactivateUserAsync(id);
                return Ok(ApiResponse<string>.Succeded("User deactivated successfully."));
            }
            catch (Exception ex)
            {
                ExceptionLogger.Logs(ex.Message);
                return BadRequest(ApiResponse<string>.Fail(ex.Message));
            }
        }

        [HttpGet("/api/getallactive")]
        public async Task<IActionResult> GetActiveUsers()
        {
            try
            {
                var users = await _userService.GetActiveUsersAsync();
                return Ok(ApiResponse<IEnumerable<UserDTO>>.Succeded(users));
            }
            catch (Exception ex)
            {
                ExceptionLogger.Logs(ex.Message);
                return BadRequest(ApiResponse<string>.Fail(ex.Message));
            }
        }

        [HttpGet("/api/getallinactive")]
        public async Task<IActionResult> GetInactiveUsers()
        {
            try
            {
                var users = await _userService.GetInactiveUsersAsync();
                return Ok(ApiResponse<IEnumerable<UserDTO>>.Succeded(users));
            }
            catch (Exception ex)
            {
                ExceptionLogger.Logs(ex.Message);
                return BadRequest(ApiResponse<string>.Fail(ex.Message));
            }
        }
    }
}
