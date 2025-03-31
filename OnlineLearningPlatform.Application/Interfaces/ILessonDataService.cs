using OnlineLearningPlatform.Application.DTOs;
using OnlineLearningPlatform.Domain.Entities;

namespace OnlineLearningPlatform.Application.Interfaces;

public interface ILessonDataService : IBaseDataService
{
    Task<Lesson?> GetLessonAsync(Guid userId, Guid lessonId);
    Task AddProgressAsync(Progress progress);
    Task DeleteLesson(Lesson lesson);
    Task AddLesson(Lesson lesson);
}