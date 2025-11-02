using BLL.Models;
using DAL.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL.Interfaces.ICustamRepository
{
    public interface IBookRepository
    {
        Task<List<Book>> SearchBookAsync(string keyword);
        Task<Book?> GetTopRatingBookAsync();
        Task<Book?> GetLowRatingBookAsync();
        Task<Book?> GetTopBorrowedBookAsync();
        Task<Book?> GetTopReservedBookAsync();

    }
}
