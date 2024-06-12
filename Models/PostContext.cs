using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace Post.Models;

public partial class PostContext : DbContext
{
    public PostContext()
    {
    }

    public PostContext(DbContextOptions<PostContext> options)
        : base(options)
    {
    }

    public virtual DbSet<PostList> PostLists { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<PostList>(entity =>
        {
            entity.Property(e => e.Id).HasDefaultValueSql("(newid())");
            entity.Property(e => e.Author).HasMaxLength(50);
            entity.Property(e => e.DatetimeCreated).HasColumnType("datetime");
            entity.Property(e => e.Description).HasMaxLength(250);
            entity.Property(e => e.Title).HasMaxLength(50);
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
