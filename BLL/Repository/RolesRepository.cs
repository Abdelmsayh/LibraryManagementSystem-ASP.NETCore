using BLL.Interfaces.ICustamRepository;
using DAL.Extend;
using Microsoft.AspNetCore.Identity;

namespace BLL.Repository
{
    public class RolesRepository : IRolesRepository
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly RoleManager<ApplicationRole> _roleManager;

        public RolesRepository(UserManager<ApplicationUser> userManager, RoleManager<ApplicationRole> roleManager)
        {
            _userManager = userManager;
            _roleManager = roleManager;
        }

        public async Task AssignRoleToUserAsync(string userId, string roleId)
        {
            var user = await _userManager.FindByIdAsync(userId);
            if (user == null)
                throw new Exception("User not found");

            var role = await _roleManager.FindByIdAsync(roleId);
            if (role == null)
                throw new Exception("Role not found");

            await _userManager.AddToRoleAsync(user, role.Name!);
        }

        public async Task RemoveRoleFromUserAsync(string userId, string roleId)
        {
            var user = await _userManager.FindByIdAsync(userId);
            var role = await _roleManager.FindByIdAsync(roleId);
            if (user != null && role != null)
                await _userManager.RemoveFromRoleAsync(user, role.Name!);
        }

        public async Task<List<ApplicationRole>> GetRolesByUserAsync(string userId)
        {
            var user = await _userManager.FindByIdAsync(userId);
            if (user == null)
                return new List<ApplicationRole>();

            var roleNames = await _userManager.GetRolesAsync(user);
            var roles = _roleManager.Roles
                .Where(r => roleNames.Contains(r.Name))
                .ToList();

            return roles;
        }
    }
}
