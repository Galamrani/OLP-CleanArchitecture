using Microsoft.EntityFrameworkCore;
using OnlineLearningPlatform.Application.Interfaces;
using OnlineLearningPlatform.Domain.Entities;
using OnlineLearningPlatform.Infrastructure.Persistence.Database;
using OnlineLearningPlatform.Infrastructure.Services.Persistence;

namespace OnlineLearningPlatform.Infrastructure.Services.CourseManagement;

public class CourseDataService : BaseDataService, ICourseDataService
{
    public CourseDataService(AppDbContext context) : base(context) { }

    public async Task<List<Course>> GetCoursesAsync()
    {
        return await context.Courses
            .AsNoTracking()
            .ToListAsync();
    }

    public async Task<Course?> GetCourseAsync(Guid courseId)
    {
        return await context.Courses
            .Include(c => c.Lessons)
            .SingleOrDefaultAsync(c => c.Id == courseId);
    }

    public async Task<Course?> GetEnrolledCourseWithProgressAsync(Guid userId, Guid courseId)
    {
        return await context.Courses
            .Include(c => c.Lessons)
            .ThenInclude(l => l.Progresses.Where(p => p.UserId == userId))
            .SingleOrDefaultAsync(c => c.Id == courseId);
    }

    public async Task<List<Course>> GetUserCreatedCoursesAsync(Guid userId)
    {
        return await context.Courses
            .Where(c => c.CreatorId == userId)
            .AsNoTracking()
            .ToListAsync();
    }

    public async Task<List<Course>> GetUserEnrolledCoursesAsync(Guid userId)
    {
        return await context.Courses
            .Where(c => c.Enrollments.Any(e => e.UserId == userId))
            .AsNoTracking()
            .ToListAsync();
    }

    public async Task AddCourseAsync(Course course)
    {
        await context.Courses.AddAsync(course);
    }

    public Task DeleteCourseAsync(Course course)
    {
        context.Courses.Remove(course);
        return Task.CompletedTask;
    }
}