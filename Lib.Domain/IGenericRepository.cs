using System.Linq.Expressions;

namespace Lib.Domain
{
    public interface IGenericRepository<T> where T : class
    {
        T Get(Expression<Func<T, bool>> expression);
        void AddRange(IEnumerable<T> entities);
        void Remove(T entity);
        void Update(T entity);
        Task<T> GetAsync(Expression<Func<T, bool>> expression, CancellationToken cancellationToken = default);
        Task<IEnumerable<T>> GetAllAsync(CancellationToken cancellationToken = default);
        Task<IEnumerable<T>> GetAllAsync(Expression<Func<T, bool>> expression, CancellationToken cancellationToken = default);
        Task AddAsync(T entity, CancellationToken cancellationToken = default);
    }
}
