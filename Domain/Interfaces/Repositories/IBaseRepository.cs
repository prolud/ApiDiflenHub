using System.Linq.Expressions;

namespace Domain.Interfaces.Repositories
{
    public interface IBaseRepository<T>
    {
        public Task<T?> GetAsync(Expression<Func<T, bool>> filter);
        public Task<List<T>> GetListAsync(Expression<Func<T, bool>> filter);
        public Task InsertAsync(T entity);
        public Task InsertRangeAsync(List<T> entities);
    }
}