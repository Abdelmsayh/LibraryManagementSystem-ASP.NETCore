using BLL.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace BLL.Interfaces.IService
{
    public interface ICategoryService
    {
        Task<List<CategoryDTO>> GetAllCategoriesAsync(Expression<Func<CategoryDTO, bool>>? filter = null);
        Task<CategoryDTO?> GetCategoryByIdAsync(Guid id);
        Task<CategoryDTO?> GetSingleCategoryAsync(Expression<Func<CategoryDTO, bool>> filter);
        Task<bool> AddCategoryAsync(CategoryDTO entity);
        Task<bool> UpdateCategoryAsync(CategoryDTO entity);
        Task<bool> DeleteCategoryAsync(CategoryDTO entity);

        Task<List<BookDTO>> GetBooksInCategoryAsync(Guid categoryId);
        Task<List<BookDTO>> SearchBooksInCategoryAsync(Guid categoryId, Expression<Func<BookDTO, bool>> predicate);
    }
}
