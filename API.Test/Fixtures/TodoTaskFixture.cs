using Lib.Domain.Entities;

namespace API.Test.Fixtures
{
    public static class TodoTaskFixture
    {
        public static IEnumerable<TodoTask> GetTaskMock()
        {
            return new[]
            {
                new TodoTask
                {
                    Completed = true,
                    CreatedDate= DateTime.Now,
                    Description = "Ir al super mañana",
                    TaskId = 1,
                    Title = "Ir al supermercado"
                },
                new TodoTask
                {
                    Completed = false,
                    CreatedDate= DateTime.Now,
                    Description = "Cargar el celular",
                    TaskId = 2,
                    Title = "Cargar Iphone"
                },
                new TodoTask
                {
                    Completed = false,
                    CreatedDate= DateTime.Now,
                    Description = "Terminar la prueba antes del martes",
                    TaskId = 3,
                    Title = "Completar prueba técnica"
                },
                new TodoTask
                {
                    Completed = false,
                    CreatedDate= DateTime.Now,
                    Description = "Llevar auto al taller",
                    TaskId = 4,
                    Title = "Reparar auto"
                },
            };
        }
    }
}
