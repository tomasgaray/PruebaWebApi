using Lib.Domain.Entities;
using Lib.Infraestructure.Mappings;
using Microsoft.EntityFrameworkCore;

namespace Lib.Infraestructure
{
    public class TodoTaskDbContext : DbContext
    {
        public TodoTaskDbContext(DbContextOptions<TodoTaskDbContext> options) : base(options) { }
        public DbSet<TodoTask> TodoTask { get; set; }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            new TodoTaskMapping(modelBuilder.Entity<TodoTask>());
            base.OnModelCreating(modelBuilder);
        }
    }
}
