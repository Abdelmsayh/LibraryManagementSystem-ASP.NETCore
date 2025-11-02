using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace BLL.Interfaces.ICustamRepository
{
    public interface IGenericRepository<T> where T : class, new()
    {
        Task<List<T>> GetAllAsync(Expression<Func<T, bool>>? filter = null);
        Task<T?> GetByAsync(Expression<Func<T, bool>> filter);
        Task<bool> AddAsync(T entity);
        Task<bool> UpdateAsync(T entity);
        Task<bool> DeleteAsync(T entity);
        Task<bool> SaveAsync();

        public Task<IEnumerable<T>> GetAllIncludeAsync(
             Expression<Func<T, bool>>? filter = null,
               List<Expression<Func<T, object>>>? includeProperties = null);

        public Task<T> GetByIncludeAsync(
               Expression<Func<T, bool>>? filter = null,
                List<Expression<Func<T, object>>>? includeProperties = null);

    }
}
