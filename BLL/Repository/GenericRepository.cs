using BLL.Interfaces.ICustamRepository;
using DAL.DataBase;
using BLL.Helper;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Linq.Expressions;

namespace BLL.Repository
{
    public class GenericRepository<T> : IGenericRepository<T> where T : class, new()
    {
        protected readonly ApplicationContext _db;
        protected readonly DbSet<T> _dbSet;

        public GenericRepository(ApplicationContext db)
        {
            _db = db;
            _dbSet = _db.Set<T>();
        }

        public async Task<List<T>> GetAllAsync(Expression<Func<T, bool>>? filter = null)
        {
            return filter == null
                ? await _dbSet.ToListAsync()
                : await _dbSet.Where(filter).ToListAsync();
        }

        public async Task<T?> GetByAsync(Expression<Func<T, bool>> filter)
        {
            return await _dbSet.FirstOrDefaultAsync(filter);
        }

        public async Task<bool> AddAsync(T entity)
        {
            try
            {
                await _dbSet.AddAsync(entity);
                await _db.SaveChangesAsync();
                return true;
            }
            catch (Exception ex)
            {
                ExceptionLogger.Logs(ex.InnerException?.Message ?? ex.Message);
                return false;
            }
        }

        public async Task<bool> UpdateAsync(T entity)
        {
            try
            {
                _dbSet.Update(entity);
                await _db.SaveChangesAsync();
                return true;
            }
            catch (Exception ex)
            {
                ExceptionLogger.Logs(ex.InnerException?.Message ?? ex.Message);
                return false;
            }
        }

        public async Task<bool> DeleteAsync(T entity)
        {
            try
            {
                _dbSet.Remove(entity);
                await _db.SaveChangesAsync();
                return true;
            }
            catch (Exception ex)
            {
                ExceptionLogger.Logs(ex.InnerException?.Message ?? ex.Message);
                return false;
            }
        }

        public async Task<bool> SaveAsync()
        {
            try
            {
                await _db.SaveChangesAsync();
                return true;
            }
            catch (Exception ex)
            {
                ExceptionLogger.Logs(ex.InnerException?.Message ?? ex.Message);
                return false;
            }
        }

        public async Task<IEnumerable<T>> GetAllIncludeAsync(Expression<Func<T, bool>>? filter = null, List<Expression<Func<T, object>>>? includeProperties = null)
        {
            IQueryable<T> query = _dbSet;

            if (filter != null)
            {
                query = query.Where(filter);
            }

            if (includeProperties != null)
            {
                foreach (var includeProperty in includeProperties)
                {
                    query = query.Include(includeProperty);
                }
            }

            return await query.ToListAsync();
        }

        public async Task<T> GetByIncludeAsync(Expression<Func<T, bool>>? filter = null, List<Expression<Func<T, object>>>? includeProperties = null)
        {
            IQueryable<T> query = _dbSet;

            if (filter != null)
            {
                query = query.Where(filter);
            }

            if (includeProperties != null)
            {
                foreach (var includeProperty in includeProperties)
                {
                    query = query.Include(includeProperty);
                }
            }

            return await query.FirstOrDefaultAsync();
        }
    }
}
