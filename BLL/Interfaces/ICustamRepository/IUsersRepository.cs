using DAL.Entities;
using DAL.Extend;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL.Interfaces.ICustamRepository
{
    public interface IUserRepository : IGenericRepository<ApplicationUser>
    {
        Task<ApplicationUser?> AuthenticateAsync(string userName, string password);
        Task<bool> DeactivateUserAsync(String id);
        Task<bool> ActivateUserAsync(String id);

        Task<List<ApplicationUser>> GetActiveUserAsync();
        Task<List<ApplicationUser>> GetInactiveUserAsync();
    }
}
