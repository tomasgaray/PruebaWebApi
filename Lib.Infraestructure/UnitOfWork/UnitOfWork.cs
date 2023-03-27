using Lib.Domain.Repositories;
using Lib.Domain.UnitOfWork;
using Lib.Infraestructure.Repositories;

namespace Lib.Infraestructure.UnitOfWork
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly TodoTaskDbContext _dbContext;
        private ITodoTaskRepository _todoTaskRepository;


        public UnitOfWork(TodoTaskDbContext dbContext)
        {
            _dbContext = dbContext;
        }


        public ITodoTaskRepository TodoTaskRepository
        {
            get { return _todoTaskRepository = _todoTaskRepository ?? new TodoTaskRepository(_dbContext); }
        }


        public void Commit()
            => _dbContext.SaveChanges();


        public async Task CommitAsync()
            => await _dbContext.SaveChangesAsync();


        public void Rollback()
            => _dbContext.Dispose();


        public async Task RollbackAsync()
            => await _dbContext.DisposeAsync();
    }
}
