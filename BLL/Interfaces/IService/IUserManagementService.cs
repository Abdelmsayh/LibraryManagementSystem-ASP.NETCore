using BLL.Models;
using BLL.Models.Account;
using DAL.Extend;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Principal;
using System.Text;
using System.Threading.Tasks;

namespace BLL.Interfaces.IService
{
    public interface IUserManagementService
    {
        public Task<IdentityResult> RegisterUser(RegistrationDTO model);
        public Task<SignInResult> LoginUser(LoginDTO model);
        public Task LogOff();
        public Task<bool> ForgetPasswordUser(ForgetPasswordDTO model);
        public Task<IdentityResult> ResetPasswordUser(ResetPasswordDTO model);
        public Task<string> GenerateToken(ApplicationUser user, IConfiguration config);
        public Task<string> Auth(LoginDTO model);

    }
}
