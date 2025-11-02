using BLL.Helper;
using BLL.Interfaces.IService;
using BLL.Models;
using BLL.Models.Account;
using DAL.Extend;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Routing;
using Microsoft.AspNetCore.Routing;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace BLL.Service
{

    public class UserManagementService : IUserManagementService
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly SignInManager<ApplicationUser> _signInManager;
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly IUrlHelper _urlHelper;
        private readonly IMembersService _membersService;


        public UserManagementService(UserManager<ApplicationUser> userManager,
            SignInManager<ApplicationUser> signInManager,
            IHttpContextAccessor httpContextAccessor, IUrlHelperFactory urlHelperFactory, IMembersService membersService)
        {
            _userManager = userManager;
            _signInManager = signInManager;

            _httpContextAccessor = httpContextAccessor;
            var actionContext = new ActionContext(
                _httpContextAccessor.HttpContext,
                _httpContextAccessor.HttpContext.GetRouteData(),
                new Microsoft.AspNetCore.Mvc.Abstractions.ActionDescriptor()
            );

            _urlHelper = urlHelperFactory.GetUrlHelper(actionContext);
            _membersService = membersService;
        }

        public async Task<string> Auth(LoginDTO model)
        {
            var user = await _userManager.FindByEmailAsync(model.Email);
            await _signInManager.PasswordSignInAsync(user, model.Password, model.RememberMe, false);
            var token = await GenerateToken(user, _httpContextAccessor.HttpContext.RequestServices.GetRequiredService<IConfiguration>());
            return token;
        }

        public async Task<bool> ForgetPasswordUser(ForgetPasswordDTO model)
        {
            try
            {
                var user = await _userManager.FindByEmailAsync(model.Email);

                var token = await _userManager.GeneratePasswordResetTokenAsync(user);

                var request = _httpContextAccessor.HttpContext.Request;

                var passwordResetLink = _urlHelper.Action(
                    "ResetPassword",
                    "Account",
                    new { Email = model.Email, Token = token },
                    request.Scheme
                );

                ExceptionLogger.Logs("Password Reset Link : " + passwordResetLink);

                return true;
            }
            catch (Exception ex)
            {
                ExceptionLogger.Logs(ex.InnerException.Message);
                return false;
            }
        }

        public async Task<string> GenerateToken(ApplicationUser user, IConfiguration config)
        {
            var jwtSettings = config.GetSection("Jwt");
            var key = new SymmetricSecurityKey(Encoding.ASCII.GetBytes(jwtSettings["Key"]));
            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

            // Optional
            var claims = new List<Claim>
            {
                new Claim(ClaimTypes.NameIdentifier, user.Id.ToString()),
                new Claim(ClaimTypes.Email, user.Email),
                new Claim(ClaimTypes.Name, user.UserName)
            };

            // If you have roles
            var roles = await _userManager.GetRolesAsync(user);

            foreach (var role in roles)
            {
                claims.Add(new Claim(ClaimTypes.Role, role));
            }


            var token = new JwtSecurityToken(
                issuer: jwtSettings["Issuer"],
                audience: jwtSettings["Audience"],
                claims: claims,
                expires: DateTime.UtcNow.AddMinutes(Convert.ToDouble(jwtSettings["DurationInMinutes"])),
                signingCredentials: creds
            );

            return new JwtSecurityTokenHandler().WriteToken(token);
        }

        public async Task<Microsoft.AspNetCore.Identity.SignInResult> LoginUser(LoginDTO model)
        {
            var user = await _userManager.FindByEmailAsync(model.Email);

            if (user == null)
            {
                return Microsoft.AspNetCore.Identity.SignInResult.Failed;
            }

            return await _signInManager.PasswordSignInAsync(user, model.Password, model.RememberMe, false);
       
        }


        public async Task LogOff()
        {
            await _signInManager.SignOutAsync();
        }

        public async Task<IdentityResult> RegisterUser(RegistrationDTO model)
        {
            var member = new MemberDTO()
            {
                FullName = model.FullName,
                Email = model.Email,
                Phone = model.Phone ?? " ",
                IsActive = true,
                MembershipStartDate = DateTime.Now
            };

            bool memberAdded = await _membersService.AddMemberAsync(member);
            if (!memberAdded)
            {
                return IdentityResult.Failed(new IdentityError { Description = "Failed to create member record." });
            }

            var user = new ApplicationUser()
            {
                UserName = model.FullName,
                Email = model.Email,
                IsAgree = model.IsAgree,
                PhoneNumber = model.Phone,
                MemberId = member.Id
            };

            var result = await _userManager.CreateAsync(user, model.Password);

            if (!result.Succeeded)
            {
                await _membersService.DeleteMemberAsync(member);
                return result;
            }

            var roleResult = await _userManager.AddToRoleAsync(user, "User");
            if (!roleResult.Succeeded)
            {
                await _userManager.DeleteAsync(user);
                await _membersService.DeleteMemberAsync(member);
                return IdentityResult.Failed(new IdentityError { Description = "Failed to assign Admin role." });
            }

            return result;
        }

        public async Task<IdentityResult> ResetPasswordUser(ResetPasswordDTO model)
        {
            var user = await _userManager.FindByEmailAsync(model.Email);
            return await _userManager.ResetPasswordAsync(user, model.Token, model.Password);
        }
    }
}
