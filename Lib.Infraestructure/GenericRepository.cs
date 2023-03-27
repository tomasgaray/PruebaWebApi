using Lib.Domain;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace Lib.Infraestructure
{

    public class GenericRepository<T> : IGenericRepository<T> where T : class
    {
        protected readonly TodoTaskDbContext _dbContext;
        private readonly DbSet<T> _entitiySet;


        public GenericRepository(TodoTaskDbContext dbContext)
        {
            _dbContext = dbContext;
            _entitiySet = _dbContext.Set<T>();
        }


        public async Task AddAsync(T entity, CancellationToken cancellationToken = default)
            => await _dbContext.AddAsync(entity, cancellationToken);


        public void AddRange(IEnumerable<T> entities)
            => _dbContext.AddRange(entities);


        public async Task AddRangeAsync(IEnumerable<T> entities, CancellationToken cancellationToken = default)
            => await _dbContext.AddRangeAsync(entities, cancellationToken);


        public T Get(Expression<Func<T, bool>> expression)
            => _entitiySet.FirstOrDefault(expression);


      
        public async Task<IEnumerable<T>> GetAllAsync(CancellationToken cancellationToken = default)
            => await _entitiySet.ToListAsync(cancellationToken);


        public async Task<IEnumerable<T>> GetAllAsync(Expression<Func<T, bool>> expression, CancellationToken cancellationToken = default)
            => await _entitiySet.Where(expression).ToListAsync(cancellationToken);

        public async Task<T> GetAsync(Expression<Func<T, bool>> expression, CancellationToken cancellationToken = default)
            => await _entitiySet.FirstOrDefaultAsync(expression, cancellationToken);


        public void Remove(T entity)
            => _dbContext.Remove(entity);


        public void RemoveRange(IEnumerable<T> entities)
            => _dbContext.RemoveRange(entities);


        public void Update(T entity)
            => _dbContext.Update(entity);


        public void UpdateRange(IEnumerable<T> entities)
            => _dbContext.UpdateRange(entities);
    }
}
