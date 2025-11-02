using BLL.Interfaces.ICustamRepository;
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
    public class CategoryRepository : GenericRepository<Category>, ICategoryRepository
    {
        public CategoryRepository(ApplicationContext db) : base(db)
        {
        }

        public async Task<List<Book>> GetBooksInCategoryAsync(Guid categoryId)
        {
            return await _db.Books
                .Where(b => b.CategoryId == categoryId)
                .ToListAsync();
        }

        public async Task<List<Book>> SearchBooksInCategoryAsync(Guid categoryId, Expression<Func<Book, bool>> predicate)
        {
            return await _db.Books
                .Where(b => b.CategoryId == categoryId)
                .Where(predicate)
                .ToListAsync();
        }
    }
}
