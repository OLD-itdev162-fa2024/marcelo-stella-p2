using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace TaskAngularProject.Models;

public partial class TaskListContext : DbContext
{
    public TaskListContext()
    {
    }

    public TaskListContext(DbContextOptions<TaskListContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Task> Tasks { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder) { }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Task>(entity =>
        {
            entity.HasKey(e => e.TaskId).HasName("PK__Tasks__DD5D55A2B256DAA5");

            entity.Property(e => e.TaskId).HasColumnName("taskID");
            entity.Property(e => e.TaskName)
                .HasMaxLength(40)
                .IsUnicode(false)
                .HasColumnName("taskName");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
