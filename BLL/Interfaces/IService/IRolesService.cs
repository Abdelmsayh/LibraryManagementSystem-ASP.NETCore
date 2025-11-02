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
    public interface IRolesService
    {
        public Task<IdentityResult> AssignUserToRole(List<UserInRoleDTO> model, string RoleId);
        public Task<List<UserInRoleDTO>> GetUserInRole(string RoleId);

        public Task<List<ApplicationRole>> GetAllRolesAsync(Expression<Func<RoleDTO, bool>>? filter = null);
        public Task<IdentityResult> DeleteRoleAsync(ApplicationRole entity);
        public  Task<IdentityResult> UpdateRoleAsync(ApplicationRole entity);
        public Task<IdentityResult> AddRoleAsync(ApplicationRole entity);
        public  Task<ApplicationRole?> GetByIdAsync(string id);








    }
}
