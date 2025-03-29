using Microsoft.EntityFrameworkCore;
using OnlineLearningPlatform.Application.Interfaces;
using OnlineLearningPlatform.Domain.Entities;
using OnlineLearningPlatform.Infrastructure.Persistence.Database;

namespace OnlineLearningPlatform.Infrastructure.Services.CourseManagement;

public class CourseDAO(AppDbContext context) : ICourseDAO
{
    private readonly AppDbContext context = context;

    public async Task<List<Course>> GetCoursesAsync()
    {
        return await context.Courses
            .Include(c => c.Lessons)
            .AsNoTracking()
            .ToListAsync();
    }

    public Task<Course?> GetCourseWithLessonsAsync(Guid courseId)
    {
        return context.Courses
            .Include(c => c.Lessons)
            .SingleOrDefaultAsync(c => c.Id == courseId);
    }

    public Task<Course?> GetCourseWithUserLessonProgressAsync(Guid userId, Guid courseId)
    {
        return context.Courses
            .Include(c => c.Lessons)
            .ThenInclude(l => l.Progresses.Where(p => p.UserId == userId))
            .SingleOrDefaultAsync(c => c.Id == courseId);
    }

    public async Task<List<Course>> GetUserCreatedCoursesAsync(Guid userId)
    {
        return await context.Courses
            .Where(c => c.CreatorId == userId)
            .Include(c => c.Lessons)
            .AsNoTracking()
            .ToListAsync();
    }

    public async Task<List<Course>> GetUserEnrolledCoursesAsync(Guid userId)
    {
        return await context.Courses
            .Where(c => c.Enrollments.Any(e => e.UserId == userId))
            .Include(c => c.Lessons)
            .AsNoTracking()
            .ToListAsync();
    }
}