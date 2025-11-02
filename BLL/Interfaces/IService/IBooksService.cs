using BLL.Interfaces.ICustamRepository;
using BLL.Models;
using DAL.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace BLL.Interfaces.IService
{
    public interface IBooksService 
    {
        Task<List<BookDTO>> GetAllBooksAsync(Expression<Func<BookDTO, bool>>? filter = null);
        Task<BookDTO?> GetBooksByIdAsync(Guid id);
        Task<bool> AddBookAsync(BookDTO entity);
        Task<bool> UpdateBookAsync(BookDTO entity);
        Task<bool> DeleteBookAsync(BookDTO entity);
        Task<List<BookDTO>> SearchBookAsync(string keyword);
        Task<BookDTO?> GetTopRatingBookAsync();
        Task<BookDTO?> GetLowRatingBookAsync();
        Task<BookDTO?> GetTopBorrowedBookAsync();
        Task<BookDTO?> GetTopReservedBookAsync();

    }
}
