using Lib.Domain.Services;
using Lib.Domain.UnitOfWork;
using Lib.Infraestructure;
using Lib.Infraestructure.Services;
using Lib.Infraestructure.UnitOfWork;
using Microsoft.EntityFrameworkCore;

namespace API.Extensions
{
    public static class ApplicationServiceExtensions
    {
        public static void ConfigureCors(this IServiceCollection services)
        {
            services.AddCors(options =>
            {
                options.AddPolicy("CorsPolicy", builder => builder
                .AllowAnyOrigin()
                .AllowAnyMethod()
                .AllowAnyHeader());
            });
        }

        public static void AddApplicationServices(this IServiceCollection services)
        {
            services.AddScoped<IUnitOfWork, UnitOfWork>();
            services.AddTransient(typeof(ITodoTaskService), typeof(TodoTaskService));
        }

        public static void ConfigureServices(this IServiceCollection services, IConfiguration configuration)
        {
            string? connectionString = configuration.GetConnectionString("sqliteConnection");
            services.AddDbContext<TodoTaskDbContext>(options =>
                options.UseSqlite(connectionString, b => b.MigrationsAssembly("Lib.Infraestructure")));
        }
    }

}