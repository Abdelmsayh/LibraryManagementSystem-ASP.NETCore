using BLL.Interfaces.ICustamRepository;
using BLL.Models;
using DAL.DataBase;
using DAL.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL.Repository
{
    public class BookRepository : GenericRepository<Book>, IBookRepository
    {
        public BookRepository(ApplicationContext db) : base(db)
        {
        }



        public async Task<List<Book>> SearchBookAsync(string keyword)
        {
            keyword = keyword.ToLower();

            return await _db.Books
                .Where(b =>
                    (b.Title ?? "").ToLower().Contains(keyword) ||
                    (b.Author ?? "").ToLower().Contains(keyword) ||
                    (b.ISBN ?? "").ToLower().Contains(keyword))
                .ToListAsync();
        }

        public async Task<Book?> GetTopRatingBookAsync()
        {
            return await _db.Books
                .OrderByDescending(b => b.Rating)
                .FirstOrDefaultAsync();
        }

        public async Task<Book?> GetLowRatingBookAsync()
        {
            return await _db.Books
                .OrderBy(b => b.Rating)
                .FirstOrDefaultAsync();
        }

        public async Task<Book?> GetTopBorrowedBookAsync()
        {
            return await _db.Books
                .Include(b => b.BorrowingTransactions)
                .OrderByDescending(b => b.BorrowingTransactions.Count)
                .FirstOrDefaultAsync();
        }

        public async Task<Book?> GetTopReservedBookAsync()
        {
            return await _db.Books
                .Include(b => b.ReservationTransactions)
                .OrderByDescending(b => b.ReservationTransactions.Count)
                .FirstOrDefaultAsync();
        }

       
    }
}
