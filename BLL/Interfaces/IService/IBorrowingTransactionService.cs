using BLL.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace BLL.Interfaces.IService
{
    public interface IBorrowingTransactionService
    {
        Task<List<BorrowingTransactionDTO>> GetAllBorrowTransactionsAsync(Expression<Func<BorrowingTransactionDTO, bool>>? filter = null);
        Task<BorrowingTransactionDTO?> GetBorrowTransactionByIdAsync(Guid id);
        Task<bool> AddBorrowTransactionAsync(BorrowingTransactionDTO entity);
        Task<bool> UpdateBorrowTransactionAsync(BorrowingTransactionDTO entity);
        Task<bool> DeleteBorrowTransactionAsync(BorrowingTransactionDTO entity);

        Task BorrowBookAsync(Guid bookId, Guid memberId);
        Task ReturnBookAsync(Guid transactionId);

        Task<List<BookDTO>> GetBorrowedBooksAsync();
        Task<List<BookDTO>> GetBorrowedBooksByMemberAsync(Guid memberId);

        Task<List<BorrowingTransactionDTO>> GetOverdueTransactionsAsync();
        Task<decimal> CalculateFineAsync(Guid transactionId);

        Task<List<BorrowingTransactionDTO>> SearchAsync(Expression<Func<BorrowingTransactionDTO, bool>> predicate);
    }
}
