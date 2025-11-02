using DAL.Entities;
using DAL.Extend;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL.Interfaces.ICustamRepository
{
    public interface IRolesRepository
    {
        Task AssignRoleToUserAsync(String userId, string roleId);
        Task RemoveRoleFromUserAsync(string userId, string roleId);

        Task<List<ApplicationRole>> GetRolesByUserAsync(String userId);
    }
}
