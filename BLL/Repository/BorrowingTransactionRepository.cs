using BLL.Interfaces.ICustamRepository;
using BLL.Models;
using DAL.DataBase;
using DAL.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace BLL.Repository
{
    public class BorrowingTransactionRepository : GenericRepository<BorrowingTransaction>, IBorrowingTransactionRepository
    {
        public BorrowingTransactionRepository(ApplicationContext db) : base(db) { }

        public async Task BorrowBookAsync(Guid bookId, Guid memberId)
        {
            var bookExists = await _db.Books.AnyAsync(b => b.Id == bookId);
            var memberExists = await _db.Members.AnyAsync(m => m.Id == memberId);

            if (!bookExists || !memberExists)
                throw new Exception("Invalid book or member ID");

            bool alreadyBorrowed = await _db.BorrowingTransactions
                .AnyAsync(t => t.BookId == bookId && t.MemberId == memberId && t.ReturnDate == null);

            if (alreadyBorrowed)
                throw new Exception("This book is already borrowed by this member.");

            var transaction = new BorrowingTransaction
            {
                Id = Guid.NewGuid(),
                BookId = bookId,
                MemberId = memberId,
                BorrowingDate = DateTime.Now,
                DueDate = DateTime.Now.AddDays(14),
                FineAmount = 0
            };

            await AddAsync(transaction);
        }

        public async Task<decimal> CalculateFineAsync(Guid transactionId)
        {
            var transaction = await _db.BorrowingTransactions
                .FirstOrDefaultAsync(t => t.Id == transactionId);

            if (transaction == null || transaction.ReturnDate != null)
                return 0;

            var overdueDays = (DateTime.Now - transaction.DueDate).Days;
            var fine = overdueDays > 0 ? overdueDays * 5 : 0;

            if (fine > 0)
            {
                transaction.FineAmount = fine;
                await UpdateAsync(transaction);
            }

            return fine;
        }

        public async Task<List<Book?>> GetBorrowedBooksAsync()
        {
            return await _db.BorrowingTransactions
                .Where(t => t.ReturnDate == null)
                .Select(t => t.Book)
                .ToListAsync();
        }

        public async Task<List<Book?>> GetBorrowedBooksByMemberAsync(Guid memberId)
        {
            return await _db.BorrowingTransactions
                .Where(t => t.MemberId == memberId && t.ReturnDate == null)
                 .Include(t => t.Book) 
                 .Select(t => t.Book)  
                 .ToListAsync();

        }

        public async Task<List<BorrowingTransaction>> GetOverdueTransactionsAsync()
        {
            return await _db.BorrowingTransactions
                .Where(t => t.ReturnDate == null && t.DueDate < DateTime.Now)
                .Include(t => t.Book)
                .Include(t => t.Member)
                .ToListAsync();
        }

        public async Task ReturnBookAsync(Guid transactionId)
        {
            var transaction = await _db.BorrowingTransactions
                .FirstOrDefaultAsync(t => t.Id == transactionId);

            if (transaction != null)
            {
                transaction.FineAmount = await CalculateFineAsync(transaction.Id);
                transaction.ReturnDate = DateTime.Now;
                await UpdateAsync(transaction);
            }
        }

        public async Task<List<BorrowingTransaction>> SearchAsync(Expression<Func<BorrowingTransaction, bool>> predicate)
        {
            return await _db.BorrowingTransactions
                .Include(t => t.Book)
                .Include(t => t.Member)
                .Where(predicate)
                .OrderByDescending(t => t.BorrowingDate)
                .ToListAsync();
        }
    }
}
