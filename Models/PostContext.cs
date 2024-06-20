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

    public virtual DbSet<UploadFile> UploadFiles { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<PostList>(entity =>
        {
            entity.Property(e => e.Id).HasDefaultValueSql("(newid())");
            entity.Property(e => e.Author)
                .IsRequired()
                .HasMaxLength(50);
            entity.Property(e => e.DatetimeCreated)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime");
            entity.Property(e => e.Description)
                .IsRequired()
                .HasMaxLength(250);
            entity.Property(e => e.EndTime)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime");
            entity.Property(e => e.ImagePath).IsRequired();
            entity.Property(e => e.StartTime)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime");
            entity.Property(e => e.Title)
                .IsRequired()
                .HasMaxLength(50);
        });

        modelBuilder.Entity<UploadFile>(entity =>
        {
            entity.Property(e => e.Id).HasDefaultValueSql("(newid())");
            entity.Property(e => e.Name).IsRequired();
            entity.Property(e => e.Src).IsRequired();

            entity.HasOne(d => d.Post).WithMany(p => p.UploadFiles)
                .HasForeignKey(d => d.PostId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_UploadFiles_UploadFiles");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
