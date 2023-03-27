using API.FilterAttributes;
using Lib.Domain.Dto;
using Lib.Domain.Services;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
    public class TodoTaskController : BaseApiController
    {
        public ITodoTaskService _todoTaskService { get; set; }
        public TodoTaskController(ITodoTaskService todoTaskService)
        {
            _todoTaskService = todoTaskService;
        }
        [HttpGet]
        public async Task<IActionResult> GetAllAsync()
        {
            try
            {
                var result = await _todoTaskService.GetAllAsync();
                return Ok(result);
            }
            catch (Exception ex)
            {
                return BadRequest(new { message = ex.Message });
            }            
        }
        [HttpGet("Search/{text}")]
        public async Task<IActionResult> Search(string text)
        {
            try
            {
                var result = await _todoTaskService.SearchAsync(text);
                return Ok(result);
            }
            catch (Exception ex)
            {
                return BadRequest(new { message = ex.Message });
            }
        }

        [MyValidator]
        [HttpPost]
        public async Task<IActionResult> AddAsync([FromBody] TodoTaskDto task)
        {
            try
            {
                var result = await _todoTaskService.AddAsync(task);
                return Ok(result);
            }
            catch (Exception ex)
            {
                return BadRequest(new { message = ex.Message });
            }
        }

        [MyValidator]
        [HttpPut]
        public async Task<IActionResult> UpdateAsync([FromBody] TodoTaskDto task)
        {
            try
            {
                var result = await _todoTaskService.UpdateAsync(task);
                return Ok(result);
            }
            catch (Exception ex)
            {
                return BadRequest(new { message = ex.Message });
            }
        }


        [HttpDelete("{TaskId}")]
        public async Task<IActionResult> RemoveAsync(int taskId)
        {
            try
            {
                await _todoTaskService.RemoveAsync(taskId);
                return Ok();
            }
            catch (Exception ex)
            {
                return BadRequest(new { message = ex.Message });
            }
        }


        [HttpPut("changeStatus/{taskId}/{completed}")]
        public async Task<IActionResult> ChangeStatus(int taskId, bool completed)
        {
            try
            {
                var result = await _todoTaskService.ChangeStatus(taskId, completed);
                return Ok(result);
            }
            catch (Exception ex)
            {
                return BadRequest(new { message = ex.Message });
            }
        }
    }
}
