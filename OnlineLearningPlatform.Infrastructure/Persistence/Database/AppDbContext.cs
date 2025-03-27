using Microsoft.EntityFrameworkCore;
using OnlineLearningPlatform.Domain.Entities;

namespace OnlineLearningPlatform.Infrastructure.Persistence.Database;
public class AppDbContext : DbContext
{
    public DbSet<User> Users { get; set; }
    public DbSet<Course> Courses { get; set; }
    public DbSet<Lesson> Lessons { get; set; }
    public DbSet<Progress> Progresses { get; set; }
    public DbSet<Enrollment> Enrollments { get; set; }

    public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }


    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        ConfigureUsers(modelBuilder);
        ConfigureCourses(modelBuilder);
        ConfigureLessons(modelBuilder);
        ConfigureProgresses(modelBuilder);
        ConfigureEnrollments(modelBuilder);
        ConfigureRelationships(modelBuilder);
        SeedInitialData(modelBuilder);

        base.OnModelCreating(modelBuilder);
    }

    private void ConfigureUsers(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<User>()
            .Property(x => x.Name).IsRequired().HasMaxLength(100);
        modelBuilder.Entity<User>()
            .Property(x => x.Email).IsRequired().HasMaxLength(256);
        modelBuilder.Entity<User>()
            .Property(x => x.Password).IsRequired();
        modelBuilder.Entity<User>()
            .HasIndex(x => x.Email).IsUnique();
    }

    private void ConfigureCourses(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Course>()
            .Property(x => x.Title).IsRequired().HasMaxLength(200);
        modelBuilder.Entity<Course>()
            .Property(x => x.Description).HasMaxLength(2000);
        modelBuilder.Entity<Course>()
            .Property(x => x.CreatorId).IsRequired();
        modelBuilder.Entity<Course>()
            .Property(x => x.CreatedAt).HasDefaultValueSql("GETUTCDATE()");
    }

    private void ConfigureLessons(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Lesson>()
            .Property(x => x.Title).IsRequired().HasMaxLength(200);
        modelBuilder.Entity<Lesson>()
            .Property(x => x.Description).HasMaxLength(2000);
        modelBuilder.Entity<Lesson>()
            .Property(x => x.CourseId).IsRequired();
        modelBuilder.Entity<Lesson>()
            .Property(x => x.VideoUrl).IsRequired().HasMaxLength(2048);
    }

    private void ConfigureProgresses(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Progress>()
            .Property(x => x.UserId).IsRequired();
        modelBuilder.Entity<Progress>()
            .Property(x => x.LessonId).IsRequired();
        modelBuilder.Entity<Progress>()
            .Property(x => x.LastWatchedAt).HasDefaultValueSql("GETUTCDATE()");
    }

    private void ConfigureEnrollments(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Enrollment>()
            .Property(x => x.UserId).IsRequired();
        modelBuilder.Entity<Enrollment>()
            .Property(x => x.CourseId).IsRequired();
        modelBuilder.Entity<Enrollment>()
            .Property(x => x.EnrolledAt).HasDefaultValueSql("GETUTCDATE()");
        modelBuilder.Entity<Enrollment>()
            .HasIndex(x => new { x.UserId, x.CourseId }).IsUnique();
    }

    private void ConfigureRelationships(ModelBuilder modelBuilder)
    {
        // User -> Course (Creator)
        modelBuilder.Entity<Course>()
            .HasOne(c => c.Creator)
            .WithMany(u => u.CreatedCourses)
            .HasForeignKey(c => c.CreatorId)
            .OnDelete(DeleteBehavior.Restrict); // Prevent cascade deletion of courses when deleting a user

        // Course -> Lesson
        modelBuilder.Entity<Lesson>()
            .HasOne(l => l.Course)
            .WithMany(c => c.Lessons)
            .HasForeignKey(l => l.CourseId)
            .OnDelete(DeleteBehavior.Cascade); // Deleting a course deletes its lessons

        // Course -> Enrollment
        modelBuilder.Entity<Enrollment>()
            .HasOne(e => e.Course)
            .WithMany(c => c.Enrollments)
            .HasForeignKey(e => e.CourseId)
            .OnDelete(DeleteBehavior.Cascade); // Deleting a course deletes enrollments

        // User -> Enrollment
        modelBuilder.Entity<Enrollment>()
            .HasOne(e => e.User)
            .WithMany(u => u.Enrollments)
            .HasForeignKey(e => e.UserId)
            .OnDelete(DeleteBehavior.Cascade); // Deleting a user deletes their enrollments

        // Lesson -> Progress
        modelBuilder.Entity<Progress>()
            .HasOne(p => p.Lesson)
            .WithMany(l => l.Progresses)
            .HasForeignKey(p => p.LessonId)
            .OnDelete(DeleteBehavior.Cascade); // Deleting a lesson deletes progress

        // User -> Progress
        modelBuilder.Entity<Progress>()
            .HasOne(p => p.User)
            .WithMany(u => u.Progresses)
            .HasForeignKey(p => p.UserId)
            .OnDelete(DeleteBehavior.Cascade); // Deleting a user deletes progress
    }

    private void SeedInitialData(ModelBuilder modelBuilder)
    {
        SeedData seedData = new SeedData();
        modelBuilder.Entity<User>().HasData(seedData.SeedUsersData());
        modelBuilder.Entity<Course>().HasData(seedData.SeedCoursesData());
        modelBuilder.Entity<Lesson>().HasData(seedData.SeedLessonsData());
        modelBuilder.Entity<Enrollment>().HasData(seedData.SeedEnrollmentsData());
        modelBuilder.Entity<Progress>().HasData(seedData.SeedProgressesData());
    }
}