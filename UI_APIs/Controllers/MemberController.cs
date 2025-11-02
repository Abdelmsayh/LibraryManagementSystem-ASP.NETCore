using BLL.Helper;
using BLL.Interfaces.IService;
using BLL.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Threading.Tasks;
using UI_APIs.Controllers;

namespace LMS.PL.Controllers
{

    [Authorize(Roles = "Admin , User")]
    public class MemberController : BaseController
    {
        private readonly IMembersService _membersService;

        public MemberController(IMembersService membersService)
        {
            _membersService = membersService;
        }

        [HttpGet("/api/getallmember")]
        public async Task<IActionResult> Get()
        {
            try
            {
                var members = await _membersService.GetAllMembersAsync();
                return Ok(ApiResponse<object>.Succeded(members));
            }
            catch (Exception ex)
            {
                ExceptionLogger.Logs(ex.Message);
                return StatusCode(500, ApiResponse<string>.Fail(ex.Message));
            }
        }

        [HttpGet("/api/getbyid/{id}")]
        public async Task<IActionResult> GetMemberById(Guid id)
        {

            try
            {
                var member = await _membersService.GetMemberByIdAsync(id);
                if (member == null)
                    return NotFound(ApiResponse<string>.Fail("Member not found."));

                return Ok(ApiResponse<object>.Succeded(member));
            }

            catch (Exception ex)
            {
                ExceptionLogger.Logs(ex.Message);
                return StatusCode(500, ApiResponse<string>.Fail(ex.Message));
            }

        }




        [HttpPost("/api/createmember")]
        public async Task<IActionResult> Create(MemberDTO model)
        {

            try
            {

                await _membersService.AddMemberAsync(model);
                return Ok(ApiResponse<string>.Succeded("Member created successfully."));
            }
            catch (Exception ex)
            {
                ExceptionLogger.Logs(ex.Message);
                return StatusCode(500, ApiResponse<string>.Fail(ex.Message));
            }

        }

        [HttpPut("/api/updatemember/{id}")]
        public async Task<IActionResult> Update(Guid id, MemberDTO model)
        {
            try
            {
                await _membersService.UpdateMemberAsync(model);
                return Ok(ApiResponse<string>.Succeded("Member updated successfully."));
            }
            catch (Exception ex)
            {
                ExceptionLogger.Logs(ex.Message);
                return StatusCode(500, ApiResponse<string>.Fail(ex.Message));
            }
        }


        [HttpDelete("/api/deletemember/{id}")]
        public async Task<IActionResult> Delete(Guid id)
        {
            try
            {
                var member = await _membersService.GetMemberByIdAsync(id);
                await _membersService.DeleteMemberAsync(new MemberDTO { Id = id });
                return Ok(ApiResponse<string>.Succeded("Member deleted successfully."));
            }
            catch (Exception ex)
            {
                ExceptionLogger.Logs(ex.Message);
                return StatusCode(500, ApiResponse<string>.Fail(ex.Message));
            }

        }

        [HttpGet("/api/canborrow/{id}")]
        public async Task<IActionResult> CanBorrow(Guid id)
        {
            try
            {
                var member = await _membersService.GetMemberByIdAsync(id);
                bool canBorrow = await _membersService.CanBorrowAsync(id);
                return Ok(ApiResponse<object>.Succeded(new { MemberId = id, CanBorrow = canBorrow }));
            }
            catch (Exception ex)
            {
                ExceptionLogger.Logs(ex.Message);
                return StatusCode(500, ApiResponse<string>.Fail(ex.Message));
            }

        }


        [HttpGet("/api/fines/{id}")]
        public async Task<IActionResult> GetFines(Guid id)
        {
            try
            {
                var member = await _membersService.GetMemberByIdAsync(id);
                var totalFine = await _membersService.GetOutstandingFinesAsync(id);
                return Ok(ApiResponse<object>.Succeded(new { Member = member, TotalFine = totalFine }));
            }
            catch (Exception ex)
            {
                ExceptionLogger.Logs(ex.Message);
                return StatusCode(500, ApiResponse<string>.Fail(ex.Message));
            }

        }

        [HttpGet("/api/porfile/{id}")]
        public async Task<IActionResult> GetProfile(Guid id)
        {
            try
            {
                var member = await _membersService.GetMemberProfileWithHistoryAsync(id);
                return Ok(ApiResponse<object>.Succeded(member));
            }
            catch (Exception ex)
            {
                ExceptionLogger.Logs(ex.Message);
                return StatusCode(500, ApiResponse<string>.Fail(ex.Message));
            }

        }

    }
}