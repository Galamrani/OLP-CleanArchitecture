using Microsoft.EntityFrameworkCore;
using OnlineLearningPlatform.Application.Interfaces;
using OnlineLearningPlatform.Domain.Entities;
using OnlineLearningPlatform.Infrastructure.Persistence.Database;
using OnlineLearningPlatform.Infrastructure.Services.Persistence;

namespace OnlineLearningPlatform.Infrastructure.Services.LessonManagement;

public class LessonDataService : BaseDataService, ILessonDataService
{
    public LessonDataService(AppDbContext context) : base(context) { }

    public async Task<Lesson?> GetLessonAsync(Guid userId, Guid lessonId)
    {
        return await context.Lessons
            .Include(l => l.Course)
            .SingleOrDefaultAsync(l => l.Id == lessonId);
    }

    public async Task AddLesson(Lesson lesson)
    {
        await context.Lessons.AddAsync(lesson);
    }

    public Task DeleteLesson(Lesson lesson)
    {
        context.Lessons.Remove(lesson);
        return Task.CompletedTask;
    }

    public async Task AddProgressAsync(Progress progress)
    {
        await context.Progresses.AddAsync(progress);
    }
}