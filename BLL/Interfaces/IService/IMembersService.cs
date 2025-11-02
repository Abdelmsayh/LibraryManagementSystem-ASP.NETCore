using BLL.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace BLL.Interfaces.IService
{
    public interface IMembersService
    {
        Task<List<MemberDTO>> GetAllMembersAsync(Expression<Func<MemberDTO, bool>>? filter = null);
        Task<MemberDTO?> GetMemberByIdAsync(Guid id);
        Task<MemberDTO?> GetSingleMemberAsync(Expression<Func<MemberDTO, bool>> filter);
        Task<bool> AddMemberAsync(MemberDTO entity);
        Task<bool> UpdateMemberAsync(MemberDTO entity);
        Task<bool> DeleteMemberAsync(MemberDTO entity);

        Task<List<MemberDTO>> SearchMembersAsync(string keyword);
        Task<List<BookDTO>> GetBorrowedBooksAsync(Guid memberId);
        Task<bool> CanBorrowAsync(Guid memberId);
        Task<decimal> GetOutstandingFinesAsync(Guid memberId);
        Task<MemberDTO> GetMemberProfileWithHistoryAsync(Guid memberId);
        Task<List<MemberDTO>> FilterMembersAsync(Expression<Func<MemberDTO, bool>> predicate);
    }
}
