namespace EmployeeManagementSystem.Repository.BaseRepository
{
    public interface IAsyncRepository<T> where T : class
    {
        Task<T?> GetByIdAsync(int Id);
        IQueryable<T> GetQueryable();
        Task<IReadOnlyList<T>> ListAllAsync();
        Task<IReadOnlyList<T>> GetPagedResponseAsync(int page, int size);
        Task<T> AddAsync(T entity);
        Task<T> UpdateAsync(T entity);
        Task<T> DeleteAsync(T entity);
        Task<int> GetPagedTotalResponseAsync();
    }
}
