using BLL.Interfaces.ICustamRepository;
using BLL.Interfaces.IService;
using BLL.Models;
using DAL.Entities;
using DAL.Extend;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace BLL.Service
{
    public class UsersService : IUsersService
    {
        protected readonly IGenericRepository<ApplicationUser> _genericRepo;
        protected readonly IUserRepository _userRepo;
        private readonly UserManager<ApplicationUser> _userManager;

        public UsersService(
            IGenericRepository<ApplicationUser> genericRepo,
            IUserRepository userRepo
            , UserManager<ApplicationUser> userManager)
        {
            _genericRepo = genericRepo;
            _userRepo = userRepo;
            _userManager = userManager;
        }

        private ApplicationUser MapToEntity(UserDTO dto)
        {
            return new ApplicationUser
            {
                Id = dto.Id,
                UserName = dto.UserName,
                Email = dto.Email,
                PhoneNumber = dto.PhoneNumber,
                IsActive = dto.IsActive
            };
        }

        private UserDTO MapToDTO(ApplicationUser user)
        {
            return new UserDTO
            {
                Id = user.Id,
                UserName = user.UserName,
                Email = user.Email,
                PhoneNumber = user.PhoneNumber,
                IsActive = user.IsActive
            };
        }

        public async Task<IdentityResult> AddAsync(ApplicationUser entity)
        {
            var password = entity.PlainPassword; 

            entity.PlainPassword = null;

            return await _userManager.CreateAsync(entity, password);
        }

        public async Task<IdentityResult> UpdateAsync(ApplicationUser entity)
        {
            return await _userManager.UpdateAsync(entity);
        }

        public async Task<IdentityResult> DeleteAsync(ApplicationUser entity)
        {
            return await _userManager.DeleteAsync(entity);
        }

        public async Task<List<ApplicationUser>> GetAllAsync(Expression<Func<ApplicationUser, bool>>? filter = null)
        {
            var users = _userManager.Users.ToList();

            if (filter != null)
                users.AsQueryable().Where(filter).ToList();

            return users;
        }

        public async Task<ApplicationUser?> GetByIdAsync(string id)
        {
            return await _userManager.FindByIdAsync(id);
             
        }

        public async Task<bool> ActivateUserAsync(string id)
        {
            return await _userRepo.ActivateUserAsync(id);
        }

        public async Task<bool> DeactivateUserAsync(string id)
        {
            return await _userRepo.DeactivateUserAsync(id);
        }


        public async Task<List<UserDTO>> GetActiveUsersAsync()
        {
            var users = await _userRepo.GetActiveUserAsync();
            return users.Select(MapToDTO).ToList();
        }

        public async Task<List<UserDTO>> GetInactiveUsersAsync()
        {
            var users = await _userRepo.GetInactiveUserAsync();
            return users.Select(MapToDTO).ToList();
        }

    }
}
