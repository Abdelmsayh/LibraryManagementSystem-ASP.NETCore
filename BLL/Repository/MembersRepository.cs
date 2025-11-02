using BLL.Interfaces.ICustamRepository;
using DAL.DataBase;
using DAL.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace BLL.Repository
{
    public class MemberRepository : GenericRepository<Member>, IMembersRepository
    {
        public MemberRepository(ApplicationContext db) : base(db)
        {
        }

        public async Task<bool> CanBorrowAsync(Guid memberId)
        {
            var member = await _db.Members
                .Include(m => m.BorrowingTransactions)
                .FirstOrDefaultAsync(m => m.Id == memberId);

            if (member == null) return false;

            bool hasOutstandingFines = member.BorrowingTransactions.Any(t => t.FineAmount > 0 && !t.IsReturned);
            bool reachedLimit = member.BorrowingTransactions.Count(t => !t.IsReturned) >= 5;

            return !hasOutstandingFines && !reachedLimit;
        }

        public async Task<List<Member>> FilterMembersAsync(Expression<Func<Member, bool>> predicate)
        {
            return await _db.Members
                .Where(predicate)
                .ToListAsync();
        }

        public async Task<List<Book?>> GetBorrowedBooksAsync(Guid memberId)
        {
            return await _db.BorrowingTransactions
                .Where(t => t.MemberId == memberId && !t.IsReturned)
                .Include(t => t.Book)
                .Select(t => t.Book)
                .ToListAsync();
        }

        public async Task<Member?> GetMemberProfileWithHistoryAsync(Guid memberId)
        {
            return await _db.Members
                .Include(m => m.BorrowingTransactions)
                    .ThenInclude(t => t.Book)
                .Include(m => m.ReservationTransactions)
                    .ThenInclude(r => r.Book)
                .FirstOrDefaultAsync(m => m.Id == memberId);
        }

        public async Task<decimal> GetOutstandingFinesAsync(Guid memberId)
        {
            var sum = await _db.BorrowingTransactions
                .Where(t => t.MemberId == memberId && (t.FineAmount ?? 0) > 0 && !t.IsReturned)
                .SumAsync(t => (decimal?)t.FineAmount) ?? 0m;

            return sum;
        }


        public async Task<List<Member>> SearchMembersAsync(string keyword)
        {
            keyword = keyword.ToLower();

            return await _db.Members
                .Where(m =>
                    m.FullName.ToLower().Contains(keyword) ||
                    m.Email.ToLower().Contains(keyword) ||
                    m.Phone.ToLower().Contains(keyword))
                .ToListAsync();
        }
    }
}
