using Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace Domain.Data;

public class ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : DbContext(options)
{
    public DbSet<User> Users { get; set; }
    public DbSet<Admin> Admins { get; set; }
    public DbSet<Teacher> Teachers { get; set; }
    public DbSet<Student> Students { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        // User configuration
        modelBuilder.Entity<User>(entity =>
        {
            entity.HasKey(e => e.Id);
            entity.Property(e => e.Email).IsRequired().HasMaxLength(255);
            entity.HasIndex(e => e.Email).IsUnique();
            entity.Property(e => e.PasswordHash).IsRequired();
            entity.Property(e => e.CreatedAt).IsRequired();
        });

        // Admin configuration
        modelBuilder.Entity<Admin>(entity =>
        {
            entity.HasKey(e => e.Id);
            entity.HasOne(e => e.User)
                  .WithOne(u => u.Admin)
                  .HasForeignKey<Admin>(e => e.UserId)
                  .OnDelete(DeleteBehavior.Cascade);
        });

        // Teacher configuration
        modelBuilder.Entity<Teacher>(entity =>
        {
            entity.HasKey(e => e.Id);
            entity.Property(e => e.Subject).IsRequired().HasMaxLength(255);
            entity.HasOne(e => e.User)
                  .WithOne(u => u.Teacher)
                  .HasForeignKey<Teacher>(e => e.UserId)
                  .OnDelete(DeleteBehavior.Cascade);
        });

        // Student configuration
        modelBuilder.Entity<Student>(entity =>
        {
            entity.HasKey(e => e.Id);
            entity.Property(e => e.Level).HasMaxLength(50);
            entity.HasOne(e => e.User)
                  .WithOne(u => u.Student)
                  .HasForeignKey<Student>(e => e.UserId)
                  .OnDelete(DeleteBehavior.Cascade);
        });
    }
}
