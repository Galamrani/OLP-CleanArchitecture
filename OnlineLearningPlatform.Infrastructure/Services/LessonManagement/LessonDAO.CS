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
            .Include(l => l.Course)
            .Include(l => l.Progresses.Where(p => p.UserId == userId))
            .SingleOrDefaultAsync(l => l.Id == lessonId);
    }
}