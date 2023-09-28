using Microsoft.EntityFrameworkCore;

namespace EmployeeManagementSystem.Repository.BaseRepository
{
    public class BaseRepository<T> : IAsyncRepository<T> where T : class
    {
        protected readonly EmployeeDbContext _dbContext;

        public BaseRepository(EmployeeDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public virtual IQueryable<T> GetQueryable()
        {
            return _dbContext.Set<T>();
        }

        public virtual async Task<T?> GetByIdAsync(int id)
        {
            return await _dbContext.Set<T>().FindAsync(id);
        }

        public virtual async Task<IReadOnlyList<T>> ListAllAsync()
        {
            return await _dbContext.Set<T>().ToListAsync();
        }

        public virtual async Task<IReadOnlyList<T>> GetPagedResponseAsync(int page, int size)
        {
            return await _dbContext.Set<T>().Skip((page - 1) * size).Take(size).AsNoTracking().ToListAsync();
        }

        public virtual async Task<int> GetPagedTotalResponseAsync()
        {
            return await _dbContext.Set<T>().CountAsync();
        }

        public virtual async Task<T> AddAsync(T entity)
        {
            await _dbContext.Set<T>().AddAsync(entity);
            try
            {
                await _dbContext.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                throw ex.InnerException!;
            }

            return entity;
        }

        public virtual async Task<T> UpdateAsync(T entity)
        {
            _dbContext.Entry(entity).State = EntityState.Modified;
            try
            {
                await _dbContext.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                throw ex.InnerException!;
            }
            return entity;
        }

        public virtual async Task<T> DeleteAsync(T entity)
        {
            // TODO alter this to set IsEnabled to false, DateModified, etc
            _dbContext.Set<T>().Remove(entity);
            await _dbContext.SaveChangesAsync();
            return entity;
        }
    }
}
