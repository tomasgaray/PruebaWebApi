using Lib.Domain.Repositories;

namespace Lib.Domain.UnitOfWork
{
    public interface IUnitOfWork
    {
        ITodoTaskRepository TodoTaskRepository { get; }
        void Commit();
        void Rollback();
        Task CommitAsync();
        Task RollbackAsync();
    }
}
