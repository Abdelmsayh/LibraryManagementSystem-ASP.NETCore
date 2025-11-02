using BLL.Interfaces.ICustamRepository;
using DAL.Extend;
using Microsoft.AspNetCore.Identity;
using System.Linq.Expressions;

namespace BLL.Repository
{
    public class UsersRepository : IUserRepository
    {
        private readonly UserManager<ApplicationUser> _userManager;

        public UsersRepository(UserManager<ApplicationUser> userManager)
        {
            _userManager = userManager;
        }

        public async Task<bool> ActivateUserAsync(string id)
        {
            var user = await _userManager.FindByIdAsync(id);
            if (user == null) return false;

            user.IsActive = true;
            await _userManager.UpdateAsync(user);
            return true;
        }

        public async Task<bool> AddAsync(ApplicationUser entity)
        {
            var result = await _userManager.CreateAsync(entity, entity.PasswordHash!); // لو عندك Password
            return result.Succeeded;
        }

        public async Task<ApplicationUser?> AuthenticateAsync(string userName, string password)
        {
            var user = await _userManager.FindByNameAsync(userName);
            if (user != null && user.IsActive)
            {
                var result = await _userManager.CheckPasswordAsync(user, password);
                if (result) return user;
            }
            return null;
        }

        public async Task<bool> ChangePasswordAsync(string id, string newPassword)
        {
            var user = await _userManager.FindByIdAsync(id);
            if (user == null || !user.IsActive) return false;

            var token = await _userManager.GeneratePasswordResetTokenAsync(user);
            var result = await _userManager.ResetPasswordAsync(user, token, newPassword);
            return result.Succeeded;
        }

        public async Task<bool> DeactivateUserAsync(string id)
        {
            var user = await _userManager.FindByIdAsync(id);
            if (user == null) return false;

            user.IsActive = false;
            await _userManager.UpdateAsync(user);
            return true;
        }

        public async Task<bool> DeleteAsync(ApplicationUser entity)
        {
            var result = await _userManager.DeleteAsync(entity);
            return result.Succeeded;
        }

        public Task<List<ApplicationUser>> GetActiveUserAsync()
        {
            return Task.FromResult(_userManager.Users.Where(u => u.IsActive).ToList());
        }

        public Task<List<ApplicationUser>> GetInactiveUserAsync()
        {
            return Task.FromResult(_userManager.Users.Where(u => !u.IsActive).ToList());
        }

        public Task<List<ApplicationUser>> GetAllAsync(Expression<Func<ApplicationUser, bool>>? filter = null)
        {
            var query = _userManager.Users.AsQueryable();
            if (filter != null)
                query = query.Where(filter);
            return Task.FromResult(query.ToList());
        }

        public Task<IEnumerable<ApplicationUser>> GetAllIncludeAsync(Expression<Func<ApplicationUser, bool>>? filter = null, List<Expression<Func<ApplicationUser, object>>>? includeProperties = null)
        {
            var query = _userManager.Users.AsQueryable();
            if (filter != null)
                query = query.Where(filter);
            return Task.FromResult(query.AsEnumerable());
        }

        public async Task<ApplicationUser?> GetByAsync(Expression<Func<ApplicationUser, bool>> filter)
        {
            return await Task.FromResult(_userManager.Users.FirstOrDefault(filter));
        }

        public async Task<ApplicationUser> GetByIncludeAsync(Expression<Func<ApplicationUser, bool>>? filter = null, List<Expression<Func<ApplicationUser, object>>>? includeProperties = null)
        {
            return await Task.FromResult(_userManager.Users.FirstOrDefault(filter));
        }

        public Task<bool> SaveAsync()
        {
            return Task.FromResult(true);
        }

        public async Task<bool> UpdateAsync(ApplicationUser entity)
        {
            var result = await _userManager.UpdateAsync(entity);
            return result.Succeeded;
        }
    }
}
