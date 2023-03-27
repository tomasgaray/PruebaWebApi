using API.Controllers;
using API.Test.Fixtures;
using FluentAssertions;
using Lib.Domain.Dto;
using Lib.Domain.Entities;
using Lib.Domain.Services;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Infrastructure;
using Microsoft.AspNetCore.TestHost;
using Moq;
using Newtonsoft.Json;
using System.Net;
using System.Text;

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

            var application = new MyWebApplication();
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
                Title = string.Empty
            };

            this._repository.Setup(x => x.UpdateAsync(task));

            var application = new MyWebApplication();
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

            var application = new MyWebApplication();
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
            objectResults?.Value.Should().BeOfType<TodoTask>();
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

            this._repository
           .Setup(x => x.GetAllAsync())
           .ReturnsAsync(TodoTaskFixture.GetTaskMock());

            TodoTaskController sut = new TodoTaskController(this._repository.Object);
            IActionResult result = await sut.UpdateAsync(task);
            OkObjectResult objectResults = (OkObjectResult)result;
            objectResults?.Value.Should().BeOfType<TodoTask>();
        }
    }
}