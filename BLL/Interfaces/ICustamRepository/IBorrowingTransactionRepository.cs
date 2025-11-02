using DAL.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace BLL.Interfaces.ICustamRepository
{
    public interface IBorrowingTransactionRepository
    {
        Task BorrowBookAsync(Guid bookId, Guid memberId);
        Task ReturnBookAsync(Guid transactionId);

        Task<List<Book>> GetBorrowedBooksAsync();
        Task<List<Book>> GetBorrowedBooksByMemberAsync(Guid memberId);

        Task<List<BorrowingTransaction>> GetOverdueTransactionsAsync();
        Task<decimal> CalculateFineAsync(Guid transactionId);

        Task<List<BorrowingTransaction>> SearchAsync(Expression<Func<BorrowingTransaction, bool>> predicate);
    }
}
