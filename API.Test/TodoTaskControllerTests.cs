using API.Controllers;
using API.Test.Fixtures;
using FluentAssertions;
using Lib.Domain.Dto;
using Lib.Domain.Entities;
using Lib.Domain.Services;
using Microsoft.AspNetCore.Mvc;
using Moq;
using Newtonsoft.Json;
using System.Net;
using System.Text;
using System.Web.Http.Results;

namespace API.Test
{
    public class TodoTaskControllerTests
    {
        private readonly Mock<ITodoTaskService> _repository;

        public TodoTaskControllerTests()
        {
            this._repository = new Mock<ITodoTaskService>();
        }
        
        [Fact]
        public async void Get_AllReturnSuccess()
        {
            this._repository
                .Setup(x => x.GetAllAsync())
                .ReturnsAsync(TodoTaskFixture.GetTaskMock());

            TodoTaskController sut = new TodoTaskController(this._repository.Object);
            IActionResult result = await sut.GetAllAsync();
            OkObjectResult objectResults = (OkObjectResult)result;
            objectResults?.Value.Should().BeOfType<TodoTask[]>();
        }

        [Fact]
        public async void Search_ReturnSuccess()
        {
            this._repository
                .Setup(x => x.SearchAsync("a"))
                .ReturnsAsync(TodoTaskFixture.GetTaskMock());

            TodoTaskController sut = new TodoTaskController(this._repository.Object);
            IActionResult result = await sut.GetAllAsync();
            OkObjectResult objectResults = (OkObjectResult)result;
            objectResults?.Value.Should().BeOfType<TodoTask[]>();
        }


        [Fact]
        public async Task Add_ReturnBadRequest_When_Title_IsEmpty()
        {

            TodoTaskDto task = new TodoTaskDto()
            {
                TaskId = 0,
                Completed = false,
                CreatedDate = DateTime.Now,
                Description = "Ir al super",
                Title = string.Empty
            };

            this._repository.Setup(x => x.AddAsync(task));

            var application = new TestApplicationFactory();
            var client = application.CreateClient();
           
            var content = new StringContent(JsonConvert.SerializeObject(task), Encoding.UTF8, "application/json");
            var result = await client.PostAsync("/api/TodoTask", content);
            result.StatusCode.Should().Be(HttpStatusCode.BadRequest);
        }

        [Fact]
        public async Task Add_ReturnBadRequest_When_Description_IsEmpty()
        {

            TodoTaskDto task = new TodoTaskDto()
            {
                TaskId = 0,
                Completed = false,
                CreatedDate = DateTime.Now,
                Description = string.Empty,
                Title = "Reparar Auto"
            };

            this._repository.Setup(x => x.UpdateAsync(task));

            var application = new TestApplicationFactory();
            var client = application.CreateClient();

            var content = new StringContent(JsonConvert.SerializeObject(task), Encoding.UTF8, "application/json");
            var result = await client.PostAsync("/api/TodoTask", content);
            result.StatusCode.Should().Be(HttpStatusCode.BadRequest);
        }


        [Fact]
        public async Task Update_ReturnBadRequest_When_Task_NotExists()
        {

            TodoTaskDto task = new TodoTaskDto()
            {
                TaskId = 999,
                Completed = false,
                CreatedDate = DateTime.Now,
                Description = "Pagar el internet antes del 25",
                Title = "Pagar internet"
            };

            this._repository.Setup(x => x.UpdateAsync(task));

            var application = new TestApplicationFactory();
            var client = application.CreateClient();

            var content = new StringContent(JsonConvert.SerializeObject(task), Encoding.UTF8, "application/json");
            var result = await client.PutAsync("/api/TodoTask", content);
            result.StatusCode.Should().Be(HttpStatusCode.BadRequest);
        }



        [Fact]
        public async Task Add_ReturnSuccess()
        {

            TodoTaskDto task = new TodoTaskDto()
            {
                TaskId = 0,
                Completed = false,
                CreatedDate = DateTime.Now,
                Description = "Llevar el auto al mecanico",
                Title = "Reparar auto"
            };

            this._repository.Setup(x => x.AddAsync(task));

            TodoTaskController sut = new TodoTaskController(this._repository.Object);
            IActionResult result = await sut.AddAsync(task);
            OkObjectResult objectResults = (OkObjectResult)result;
            objectResults?.StatusCode.Should().Be(200);
        }
        
        

        [Fact]
        public async Task Update_ReturnSuccess()
        {
            TodoTaskDto task = new TodoTaskDto()
            {
                TaskId = 1,
                Completed = false,
                CreatedDate = DateTime.Now,
                Description = "Llevar el auto al mecanico el martes",
                Title = "Reparar auto"
            };

            this._repository.Setup(x => x.UpdateAsync(task));

  
            TodoTaskController sut = new TodoTaskController(this._repository.Object);
            IActionResult result = await sut.UpdateAsync(task);
            OkObjectResult objectResults = (OkObjectResult)result;
            objectResults?.StatusCode.Should().Be(200);
        }



        [Fact]
        public async Task Remove_ReturnSuccess()
        {
            int taskId = 1;

            this._repository.Setup(x => x.RemoveAsync(taskId));


            this._repository
             .Setup(x => x.GetAllAsync())
             .ReturnsAsync(TodoTaskFixture.GetTaskMock());

            TodoTaskController sut = new TodoTaskController(this._repository.Object);
            IActionResult result = await sut.RemoveAsync(taskId);
            var okResult = result as Microsoft.AspNetCore.Mvc.OkResult;
            okResult?.StatusCode.Should().Be(200);

        }


        [Fact]
        public async Task Remove_ReturnBadRequest_When_Task_NotExists()
        {

            int taskId = -50;

            this._repository.Setup(x => x.RemoveAsync(taskId));

            var application = new TestApplicationFactory();
            var client = application.CreateClient();

            var result = await client.DeleteAsync($"/api/TodoTask/{taskId}");
            result.StatusCode.Should().Be(HttpStatusCode.BadRequest);
        }

    }
}