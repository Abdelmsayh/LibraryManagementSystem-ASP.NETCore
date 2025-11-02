using BLL.Helper;
using BLL.Interfaces.IService;
using BLL.Models.Account;
using BLL.Service;
using DAL.Extend;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using UI_APIs.Controllers;

namespace LMS.API.Controllers
{

    public class AccountController : BaseController
    {
        private readonly IUserManagementService _userManagementService;
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly SignInManager<ApplicationUser> _signInManager;

        public AccountController(
            IUserManagementService userManagementService,
            UserManager<ApplicationUser> userManager,
            SignInManager<ApplicationUser> signInManager)
        {
            _userManagementService = userManagementService;
            _userManager = userManager;
            _signInManager = signInManager;
        }

        #region Registration

        [HttpPost("/api/register")]
        public async Task<IActionResult> Register([FromBody] RegistrationDTO model)
        {
 
            try
            {
                IdentityResult result = await _userManagementService.RegisterUser(model);

                if (result.Succeeded)
                    return Ok(ApiResponse<string>.Succeded("Registration successful."));

                var errors = string.Join(", ", result.Errors.Select(e => e.Description));
                return BadRequest(ApiResponse<string>.Fail(errors));
            }
            catch (Exception ex)
            {
                ExceptionLogger.Logs(ex.Message);
                return StatusCode(500, ApiResponse<string>.Fail("Server error during registration."));
            }
        }

        #endregion

        #region Login

        [HttpPost("/api/login")]
        public async Task<IActionResult> Login([FromBody] LoginDTO model)
        {

            try
            {
                    var token = await _userManagementService.Auth(model);
                    if (token == null)
                        return Unauthorized(ApiResponse<string>.Fail("Invalid credentials"));

                    return Ok(ApiResponse<string>.Succeded(token));
                

            }
            catch (Exception ex)
            {
                ExceptionLogger.Logs(ex.Message);
                return StatusCode(500, ApiResponse<string>.Fail("Server error during login."));
            }
        }

        #endregion

        #region Logout

        [HttpPost("/api/logout")]
        public async Task<IActionResult> LogOff()
        {
            try
            {
                await _userManagementService.LogOff();
                return Ok(ApiResponse<string>.Succeded("Logged out successfully."));
            }
            catch (Exception ex)
            {
                ExceptionLogger.Logs(ex.Message);
                return StatusCode(500, ApiResponse<string>.Fail("Error logging out."));
            }
        }

        #endregion

        #region Forget Password

        [HttpPost("/api/forgetpassword")]
        public async Task<IActionResult> ForgetPassword([FromBody] ForgetPasswordDTO model)
        {

            try
            {
                bool result = await _userManagementService.ForgetPasswordUser(model);

                if (result)
                    return Ok(ApiResponse<string>.Succeded("Password reset email sent successfully."));

                return NotFound(ApiResponse<string>.Fail("Unregistered account."));
            }
            catch (Exception ex)
            {
                ExceptionLogger.Logs(ex.Message);
                return StatusCode(500, ApiResponse<string>.Fail("Error sending reset password email."));
            }
        }

        #endregion

        #region Reset Password

        [HttpPost("/api/resetpassword")]
        public async Task<IActionResult> ResetPassword([FromBody] ResetPasswordDTO model)
        {

            try
            {
                IdentityResult result = await _userManagementService.ResetPasswordUser(model);

                if (result.Succeeded)
                    return Ok(ApiResponse<string>.Succeded("Password reset successful."));

                var errors = string.Join(", ", result.Errors.Select(e => e.Description));
                return BadRequest(ApiResponse<string>.Fail(errors));
            }
            catch (Exception ex)
            {
                ExceptionLogger.Logs(ex.Message);
                return StatusCode(500, ApiResponse<string>.Fail("Server error during password reset."));
            }
        }

        #endregion
    }
}
