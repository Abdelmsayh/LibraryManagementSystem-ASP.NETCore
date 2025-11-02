using BLL.Helper;
using BLL.Interfaces.IService;
using BLL.Models;
using BLL.Service;
using DAL.Extend;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using UI_APIs.Controllers;
namespace LMS.PL.Controllers
{
    [Authorize(Roles = "Admin")]
    public class RoleController : BaseController
    {
        private readonly IRolesService _roleService;

        public RoleController(IRolesService roleService)
        {
            _roleService = roleService;
        }

        [HttpGet("/api/getallrole")]
        public async Task<IActionResult> GetAllRoles()
        {
            try
            {
                var roles = await _roleService.GetAllRolesAsync();
                return Ok(ApiResponse<object>.Succeded(roles));
            }
            catch (Exception ex)
            {
                ExceptionLogger.Logs(ex.Message);
                return StatusCode(500, ApiResponse<string>.Fail(ex.Message));
            }
        }

        [HttpGet("/api/detroleby/{id}")]
        public async Task<IActionResult> GetRoleById(string id)
        {
            try
            {
                var role = await _roleService.GetByIdAsync(id);
                return Ok(ApiResponse<object>.Succeded(role));
            }
            catch (Exception ex)
            {
                ExceptionLogger.Logs(ex.Message);
                return StatusCode(500, ApiResponse<string>.Fail(ex.Message));
            }

        }


        [HttpPost("/api/createrole")]
        public async Task<IActionResult> Create(ApplicationRole role)
        {
            try
            {
                await _roleService.AddRoleAsync(role);
                return Ok(ApiResponse<string>.Succeded("Role created successfully."));
            }
            catch (Exception ex)
            {
                ExceptionLogger.Logs(ex.Message);
                return BadRequest(ApiResponse<string>.Fail($"Error creating role: {ex.Message}"));
            }
        }



        [HttpPut("/api/updaterole{id}")]
        public async Task<IActionResult> Update(string id, ApplicationRole role)
        {
         
            var existingRole = await _roleService.GetByIdAsync(id);
            existingRole.Name = role.Name;
            existingRole.Description = role.Description;
            existingRole.NormalizedName = role.Name?.ToUpper();

            try
            {
                await _roleService.UpdateRoleAsync(existingRole);
                return Ok(ApiResponse<string>.Succeded("Role updated successfully."));
            }
            catch (Exception ex)
            {
                ExceptionLogger.Logs(ex.Message);
                return BadRequest(ApiResponse<string>.Fail($"Error updating role: {ex.Message}"));
            }
        }


        [HttpDelete("/api/deleterole/{id}")]
        public async Task<IActionResult> Delete(string id)
        {

            try
            {
                var role = await _roleService.GetByIdAsync(id);
                await _roleService.DeleteRoleAsync(role);
                return Ok(ApiResponse<string>.Succeded("Role deleted successfully."));
            }
            catch (Exception ex)
            {
                ExceptionLogger.Logs(ex.Message);
                return BadRequest(ApiResponse<string>.Fail($"Error deleting role: {ex.Message}"));
            }
        }

        [HttpPost("assignusers/{roleId}")]
        public async Task<IActionResult> AddOrRemoveUsers([FromBody] List<UserInRoleDTO> model, string roleId)
        {

            try
            {
                var result = await _roleService.AssignUserToRole(model, roleId);

                if (result.Succeeded)
                    return Ok(ApiResponse<string>.Succeded("Users updated successfully for the role."));

                var errors = string.Join(", ", result.Errors.Select(e => e.Description));
                return BadRequest(ApiResponse<string>.Fail(errors));
            }
            catch (Exception ex)
            {
                ExceptionLogger.Logs(ex.Message);
                return BadRequest(ApiResponse<string>.Fail($"Error updating users in role: {ex.Message}"));
            }
        }


    }
}
