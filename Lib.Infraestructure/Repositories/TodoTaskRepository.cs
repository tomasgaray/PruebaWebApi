using Lib.Domain.Entities;
using Lib.Domain.Repositories;

namespace Lib.Infraestructure.Repositories
{
    public class TodoTaskRepository : GenericRepository<TodoTask>, ITodoTaskRepository
    {
        public TodoTaskRepository(TodoTaskDbContext dbContext) : base(dbContext)
        {
        }
    }
}
