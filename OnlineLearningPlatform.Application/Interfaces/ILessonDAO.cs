using OnlineLearningPlatform.Application.DTOs;

namespace OnlineLearningPlatform.Application.Interfaces;

public interface ILessonDAO
{
    Task<LessonDto> AddLessonAsync(Guid userId, LessonDto lessonDto);
    Task<LessonDto> UpdateLessonAsync(Guid userId, LessonDto lessonDto);
    Task<bool> AddProgressAsync(ProgressDto progressDto);
    Task<bool> DeleteLessonAsync(Guid userId, Guid lessonId);
}