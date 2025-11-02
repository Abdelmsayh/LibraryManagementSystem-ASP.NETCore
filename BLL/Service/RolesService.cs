using BLL.Interfaces.ICustamRepository;
using BLL.Interfaces.IService;
using BLL.Models;
using DAL.Extend;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace BLL.Service
{
    public class RolesService : IRolesService
    {
  
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly RoleManager<ApplicationRole> _roleManager;

        public RolesService(
               UserManager<ApplicationUser> userManager
            , RoleManager<ApplicationRole> roleManager)
        {
           
            _userManager = userManager;
            _roleManager = roleManager;
        }

 

        public async Task<IdentityResult> AddRoleAsync(ApplicationRole entity)
        {
            return await _roleManager.CreateAsync(entity);

        }

        public async Task<IdentityResult> UpdateRoleAsync(ApplicationRole entity)
        {
            return await _roleManager.UpdateAsync(entity);

        }

        public async Task<IdentityResult> DeleteRoleAsync(ApplicationRole entity)
        {
            return await _roleManager.DeleteAsync(entity);

        }

        public async Task<List<ApplicationRole>> GetAllRolesAsync(Expression<Func<RoleDTO, bool>>? filter = null)
        {
            return _roleManager.Roles.ToList();
            
        }

        public async Task<List<UserInRoleDTO>> GetUserInRole(string RoleId)
        {
            var role = await _roleManager.FindByIdAsync(RoleId);

            var model = new List<UserInRoleDTO>();

            foreach (var user in _userManager.Users.ToList())
            {
                var userInRole = new UserInRoleDTO()
                {
                    UserId = user.Id,
                    UserName = user.UserName
                };

                if (await _userManager.IsInRoleAsync(user, role.Name))
                {
                    userInRole.IsSelected = true;
                }
                else
                {
                    userInRole.IsSelected = false;
                }

                model.Add(userInRole);
            }

            return model;
        }


        public async Task<IdentityResult> AssignUserToRole(List<UserInRoleDTO> model, string RoleId)
        {
            var role = await _roleManager.FindByIdAsync(RoleId);

            IdentityResult result = null;

            for (int i = 0; i < model.Count; i++)
            {
                var user = await _userManager.FindByIdAsync(model[i].UserId);

                if (model[i].IsSelected && !(await _userManager.IsInRoleAsync(user, role.Name)))
                {

                    result = await _userManager.AddToRoleAsync(user, role.Name);
                }

                if (!model[i].IsSelected && (await _userManager.IsInRoleAsync(user, role.Name)))
                {
                    result = await _userManager.RemoveFromRoleAsync(user, role.Name);
                }
            }

            return result;
        }

        public async Task<ApplicationRole?> GetByIdAsync(string id)
        {
                return await _roleManager.FindByIdAsync(id);
        }
    }
}
