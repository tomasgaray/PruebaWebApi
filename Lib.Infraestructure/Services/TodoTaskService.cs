using Lib.Domain.Dto;
using Lib.Domain.Entities;
using Lib.Domain.Services;
using Lib.Domain.UnitOfWork;

namespace Lib.Infraestructure.Services
{
    public class TodoTaskService: ITodoTaskService
    {
        public IUnitOfWork _unitOfWork;
        public TodoTaskService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<IEnumerable<TodoTask>> GetAllAsync()
         => await _unitOfWork.TodoTaskRepository.GetAllAsync();

        public async Task<IEnumerable<TodoTask>> SearchAsync(string text)
        {
            var result = await _unitOfWork.TodoTaskRepository
                .GetAllAsync(x => x.Title.ToLower().Contains(text) || (x.Description ?? "").ToLower().Contains(text));
            return result;
        }

        public async Task<TodoTask> AddAsync(TodoTaskDto task)
        {
            await _unitOfWork.TodoTaskRepository.AddAsync(task);
            await _unitOfWork.CommitAsync();
            return task;
        }

        public async Task<TodoTask> UpdateAsync(TodoTaskDto task)
        {
            var currentTask = await GetTaskAndCheckExists(task.TaskId);
            currentTask.Title = task.Title;
            currentTask.Description = task.Description;
            currentTask.Completed = task.Completed;
            _unitOfWork.TodoTaskRepository.Update(currentTask);
            await _unitOfWork.CommitAsync();
            return task;
        }
       
        public async Task RemoveAsync(int taskId)
        {
            var task = await GetTaskAndCheckExists(taskId);
            _unitOfWork.TodoTaskRepository.Remove(task);
            await _unitOfWork.CommitAsync();
        }

        public async Task<TodoTask> ChangeStatus(int taskId, bool completed)
        {
            var task = await GetTaskAndCheckExists(taskId);
            task.Completed = completed;
            _unitOfWork.TodoTaskRepository.Update(task);
            await _unitOfWork.CommitAsync();
            return task;
        }

        private async Task<TodoTask> GetTaskAndCheckExists(int taskId)
        {
            var exits = await _unitOfWork.TodoTaskRepository.GetAsync(x => x.TaskId == taskId);
            if (exits == null) throw new Exception("La tarea que busca ya no existe");
            return exits;
        }
    }
}
