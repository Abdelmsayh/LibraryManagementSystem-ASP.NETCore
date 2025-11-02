using DAL.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace BLL.Interfaces.ICustamRepository
{
    public interface IMembersRepository
    {

        Task<List<Member>> SearchMembersAsync(string keyword);
        Task<List<Book>> GetBorrowedBooksAsync(Guid memberId);
        Task<bool> CanBorrowAsync(Guid memberId);
        Task<decimal> GetOutstandingFinesAsync(Guid memberId);
        Task<Member> GetMemberProfileWithHistoryAsync(Guid memberId);
        Task<List<Member>> FilterMembersAsync(Expression<Func<Member, bool>> predicate);
    }
}
