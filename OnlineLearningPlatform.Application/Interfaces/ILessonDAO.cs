using OnlineLearningPlatform.Application.DTOs;
using OnlineLearningPlatform.Domain.Entities;

namespace OnlineLearningPlatform.Application.Interfaces;

public interface ILessonDAO
{
    Task<Lesson?> GetLessonAsync(Guid lessonId);
    Task<Guid> GetLessonCreatorIdAsync(Guid courseId);
    Task AddLessonAsync(Lesson lesson);
    Task AddProgressAsync(Progress progress);
    Task DeleteLessonAsync(Lesson lesson);

}