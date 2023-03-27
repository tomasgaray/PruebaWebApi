using Lib.Domain.Dto;
using Lib.Domain.Entities;

namespace Lib.Domain.Services
{
    public interface ITodoTaskService
    {
        public Task<IEnumerable<TodoTask>> GetAllAsync();
        public Task<IEnumerable<TodoTask>> SearchAsync(string text);
        public Task<TodoTask> AddAsync(TodoTaskDto task);
        public Task<TodoTask> UpdateAsync(TodoTaskDto task);
        public Task RemoveAsync(int taskId);
        public Task<TodoTask> ChangeStatus(int taskId, bool completed);
        
    }
}
