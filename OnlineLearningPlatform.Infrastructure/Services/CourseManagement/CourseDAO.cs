using Microsoft.EntityFrameworkCore;
using OnlineLearningPlatform.Application.DTOs;
using OnlineLearningPlatform.Application.Interfaces;
using OnlineLearningPlatform.Domain.Entities;
using OnlineLearningPlatform.Infrastructure.Database;

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

    // TODO: add InvalidOperationException if more then one, to the exception handler
    public Task<Course?> GetBasicCourseAsync(Guid courseId)
    {
        return context.Courses
            .Include(c => c.Lessons)
            .SingleOrDefaultAsync(c => c.Id == courseId);
    }

    // TODO: add InvalidOperationException if more then one, to the exception handler
    public Task<Course?> GetFullCourseAsync(Guid userId, Guid courseId)
    {
        return context.Courses
            .Include(c => c.Lessons)
            .ThenInclude(l => l.Progresses.Where(p => p.UserId == userId))
            .AsNoTracking()
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

    public Task<Course> AddCourseAsync(CourseDto courseDto)
    {
        throw new NotImplementedException();
    }

    public Task<bool> DeleteCourseAsync(Guid userId, Guid courseId)
    {
        throw new NotImplementedException();
    }

    public Task<Course> UpdateCourseAsync(Guid userId, CourseDto courseDto)
    {
        throw new NotImplementedException();
    }
}