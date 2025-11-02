using DAL.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace BLL.Interfaces.ICustamRepository
{
    public interface ICategoryRepository
    {

        Task<List<Book>> GetBooksInCategoryAsync(Guid categoryId);

        Task<List<Book>> SearchBooksInCategoryAsync(Guid categoryId, Expression<Func<Book, bool>> predicate);
    }
}
