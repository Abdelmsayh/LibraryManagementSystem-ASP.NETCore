using BLL.Helper;
using BLL.Interfaces.IService;
using BLL.Models.Account;
using DAL.Extend;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using System.Security.Principal;

namespace UI.Controllers
{
    public class AccountController : Controller
    {
        private readonly IUserManagementService _userManagementService;
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly SignInManager<ApplicationUser> _signInManager;


        public AccountController(IUserManagementService userManagementService, UserManager<ApplicationUser> userManager, SignInManager<ApplicationUser> signInManager)
        {
            _userManagementService = userManagementService;
            _userManager = userManager;
            _signInManager = signInManager;
        }


        #region Registration

        public IActionResult Registration()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Registration(RegistrationDTO model)
        {

            if (ModelState.IsValid)
            {
                IdentityResult result = await _userManagementService.RegisterUser(model);

                if (result.Succeeded)
                    return RedirectToAction("Login");


                foreach (var item in result.Errors)
                {
                    ModelState.AddModelError("", item.Description);
                }
            }

            return View(model);
        }


        #endregion


        #region Login


        public IActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Login(LoginDTO model)
        {
            if (ModelState.IsValid)
            {
                Microsoft.AspNetCore.Identity.SignInResult result = await _userManagementService.LoginUser(model);

                if (result.Succeeded)
                    return RedirectToAction("Index", "Home");


                ModelState.AddModelError("", "invalid username or password");
            }

            return View(model);
        }

        #endregion


        #region Sign Out

        [HttpPost]
        public async Task<IActionResult> LogOff()
        {
            await _userManagementService.LogOff();
            return RedirectToAction("Login");
        }

        #endregion


        #region Forget Password

        public IActionResult ForgetPassword()
        {
            return View();
        }

        public IActionResult ConfirmForgetPassword()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> ForgetPassword(ForgetPasswordDTO model)
        {
            if (ModelState.IsValid)
            {
                bool result = await _userManagementService.ForgetPasswordUser(model);

                if (result)
                    return RedirectToAction("ConfirmForgetPassword");


                ModelState.AddModelError("", "un registered account");
            }

            return View(model);
        }

        #endregion


        #region Reset Password (Recover password)

        public IActionResult ResetPassword(string Email, string Token)
        {
            return View();
        }

        public IActionResult ConfirmResetPassword()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> ResetPassword(ResetPasswordDTO model)
        {

            if (ModelState.IsValid)
            {
                IdentityResult result = await _userManagementService.ResetPasswordUser(model);

                if (result.Succeeded)
                    return RedirectToAction("ConfirmResetPassword");


                foreach (var item in result.Errors)
                {
                    ModelState.AddModelError("", item.Description);
                }

            }

            return View(model);
        }

        #endregion

        #region Upload Phote

        [HttpPost]
        public async Task<IActionResult> UploadProfileImage(IFormFile Image)
        {
            if (Image == null || Image.Length == 0)
                return RedirectToAction("Index", "Home");

            var user = await _userManager.GetUserAsync(User);
            if (user == null)
                return RedirectToAction("Login", "Account");

            string fileName = await FileUploader.UploadAsync(Image, "Uploads/Users");
            user.ImageName = fileName;

            await _userManager.UpdateAsync(user);

            var claims = new List<Claim> { new Claim("ImageName", fileName) };
            await _signInManager.SignInWithClaimsAsync(user, isPersistent: false, claims);

            return RedirectToAction("Index", "Home");
        }

        #endregion
    }
}
