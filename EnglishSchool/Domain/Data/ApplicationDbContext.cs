using Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace Domain.Data;

public class ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : DbContext(options)
{
    public DbSet<Course> Courses { get; set; }

    public DbSet<Lesson> Lessons { get; set; }

    public DbSet<StudentLesson> StudentLessons { get; set; }

    public DbSet<Homework> Homeworks { get; set; }

    public DbSet<HomeworkAssignment> HomeworkAssignments { get; set; }

    public DbSet<CalendarEvent> CalendarEvents { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        // Course configuration
        modelBuilder.Entity<Course>(entity =>
        {
            entity.HasKey(e => e.Id);
            entity.Property(e => e.Title).IsRequired().HasMaxLength(255);
            entity.Property(e => e.Description).HasMaxLength(1000);
            entity.Property(e => e.Price).IsRequired();
            entity.Property(e => e.NumberOfLessons).IsRequired();
        });

        // Lesson configuration
        modelBuilder.Entity<Lesson>(entity =>
        {
            entity.HasKey(e => e.Id);
            entity.Property(e => e.Title).IsRequired().HasMaxLength(255);
            entity.Property(e => e.Description).IsRequired();
            entity.Property(e => e.DurationMinutes).IsRequired();
            entity.Property(e => e.Type).IsRequired();
            entity.Property(e => e.Status).IsRequired();
            entity.Property(e => e.CreatedAt).IsRequired();
            entity.HasOne(e => e.Course)
                  .WithMany(c => c.Lessons)
                  .HasForeignKey(e => e.CourseId)
                  .OnDelete(DeleteBehavior.SetNull);
        });

        // StudentLesson configuration
        modelBuilder.Entity<StudentLesson>(entity =>
        {
            entity.HasKey(e => e.Id);
            entity.Property(e => e.AttendanceStatus).IsRequired();
            entity.HasOne(e => e.Lesson)
                  .WithMany(l => l.StudentLessons)
                  .HasForeignKey(e => e.LessonId)
                  .OnDelete(DeleteBehavior.Cascade);
        });

        // Homework configuration
        modelBuilder.Entity<Homework>(entity =>
        {
            entity.HasKey(e => e.Id);
            entity.Property(e => e.Title).IsRequired().HasMaxLength(255);
            entity.Property(e => e.Description).IsRequired();
            entity.Property(e => e.Instructions).IsRequired();
            entity.Property(e => e.DueDate).IsRequired();
            entity.Property(e => e.CreatedAt).IsRequired();
            entity.HasOne(e => e.Lesson)
                  .WithMany(l => l.Homeworks)
                  .HasForeignKey(e => e.LessonId)
                  .OnDelete(DeleteBehavior.SetNull);
        });

        // HomeworkAssignment configuration
        modelBuilder.Entity<HomeworkAssignment>(entity =>
        {
            entity.HasKey(e => e.Id);
            entity.Property(e => e.Status).IsRequired();
            entity.HasOne(e => e.Homework)
                  .WithMany(h => h.HomeworkAssignments)
                  .HasForeignKey(e => e.HomeworkId)
                  .OnDelete(DeleteBehavior.Cascade);
        });

        // CalendarEvent configuration
        modelBuilder.Entity<CalendarEvent>(entity =>
        {
            entity.HasKey(e => e.Id);
            entity.Property(e => e.Title).IsRequired().HasMaxLength(255);
            entity.Property(e => e.StartDateTime).IsRequired();
            entity.Property(e => e.EndDateTime).IsRequired();
            entity.Property(e => e.Type).IsRequired();
            entity.HasOne(e => e.Lesson)
                  .WithMany(l => l.CalendarEvents)
                  .HasForeignKey(e => e.LessonId)
                  .OnDelete(DeleteBehavior.Cascade);
        });
    }
}
