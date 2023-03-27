﻿// <auto-generated />
using System;
using Lib.Infraestructure;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

#nullable disable

namespace Lib.Infraestructure.Migrations
{
    [DbContext(typeof(TodoTaskDbContext))]
    partial class TodoTaskDbContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder.HasAnnotation("ProductVersion", "7.0.4");

            modelBuilder.Entity("Lib.Domain.Entities.TodoTask", b =>
                {
                    b.Property<int>("TaskId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER")
                        .HasColumnName("TaskId");

                    b.Property<bool>("Completed")
                        .HasColumnType("INTEGER")
                        .HasColumnName("Completed");

                    b.Property<DateTime>("CreatedDate")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("TEXT")
                        .HasDefaultValue(new DateTime(2023, 3, 26, 21, 51, 1, 302, DateTimeKind.Local).AddTicks(5544))
                        .HasColumnName("CreatedDate");

                    b.Property<string>("Description")
                        .IsRequired()
                        .HasMaxLength(100)
                        .HasColumnType("TEXT")
                        .HasColumnName("Description");

                    b.Property<string>("Title")
                        .IsRequired()
                        .HasMaxLength(100)
                        .HasColumnType("TEXT")
                        .HasColumnName("Title");

                    b.HasKey("TaskId");

                    b.ToTable("TodoTask", (string)null);
                });
#pragma warning restore 612, 618
        }
    }
}
