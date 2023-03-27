using Lib.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Lib.Infraestructure.Mappings
{
    public  class TodoTaskMapping 
    {
        public TodoTaskMapping(EntityTypeBuilder<TodoTask> builder)
        {
            builder.HasKey(x => x.TaskId);
            builder.Property(x => x.TaskId).HasColumnName("TaskId");
            builder.Property(x => x.Title).HasColumnName("Title").HasMaxLength(100);
            builder.Property(x => x.Description).HasColumnName("Description").HasMaxLength(100);
            builder.Property(x => x.CreatedDate).HasColumnName("CreatedDate").HasDefaultValue(DateTime.Now);
            builder.Property(x => x.Completed).HasColumnName("Completed");
            builder.ToTable("TodoTask");
        }
    }
}
