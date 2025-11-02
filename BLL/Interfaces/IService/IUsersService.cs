using BLL.Models;
using DAL.Extend;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace BLL.Interfaces.IService
{
    public interface IUsersService
    {
        Task<List<ApplicationUser>> GetAllAsync(Expression<Func<ApplicationUser, bool>>? filter = null);
        Task<ApplicationUser?> GetByIdAsync(String id);
        Task<IdentityResult> AddAsync(ApplicationUser entity);
        Task<IdentityResult> UpdateAsync(ApplicationUser entity);
        Task<IdentityResult> DeleteAsync(ApplicationUser entity);
        Task<bool> DeactivateUserAsync(String id);
        Task<bool> ActivateUserAsync(String id);
        Task<List<UserDTO>> GetActiveUsersAsync();
        Task<List<UserDTO>> GetInactiveUsersAsync();
    }
}
