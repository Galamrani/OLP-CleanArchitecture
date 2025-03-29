using OnlineLearningPlatform.Application.DTOs;
using OnlineLearningPlatform.Domain.Entities;

namespace OnlineLearningPlatform.Application.Interfaces;

public interface ILessonDAO
{
    Task<Lesson?> GetLessonAsync(Guid userId, Guid lessonId);
}