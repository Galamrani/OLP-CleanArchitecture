using Microsoft.EntityFrameworkCore;
using OnlineLearningPlatform.Application.Interfaces;
using OnlineLearningPlatform.Domain.Entities;
using OnlineLearningPlatform.Infrastructure.Persistence.Database;

namespace OnlineLearningPlatform.Infrastructure.Services.LessonManagement;

public class LessonDAO(AppDbContext context) : ILessonDAO
{
    private readonly AppDbContext context = context;

    public async Task<Lesson?> GetLessonAsync(Guid userId, Guid lessonId)
    {
        return await context.Lessons
            .Include(l => l.Progresses.Where(p => p.UserId == userId))
            .SingleOrDefaultAsync(l => l.Id == lessonId);
    }

    public async Task<Guid> GetLessonCreatorIdAsync(Guid courseId)
    {
        return await context.Courses
            .Where(c => c.Id == courseId)
            .AsNoTracking()
            .Select(c => c.CreatorId)
            .FirstOrDefaultAsync();
    }

    public async Task AddLessonAsync(Lesson lesson)
    {
        await context.Lessons.AddAsync(lesson);
    }

    public async Task AddProgressAsync(Progress progress)
    {
        await context.Progresses.AddAsync(progress);
    }

    public Task DeleteLessonAsync(Lesson lesson)
    {
        context.Lessons.Remove(lesson);
        return Task.CompletedTask;
    }
}