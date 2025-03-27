using OnlineLearningPlatform.Application.DTOs;

namespace OnlineLearningPlatform.Application.Services.LessonManagement;

public interface ILessonService
{
    Task<LessonDto> AddLessonAsync(Guid userId, LessonDto lessonDto);
    Task<LessonDto> UpdateLessonAsync(Guid userId, LessonDto lessonDto);
    Task<ProgressDto> AddProgressAsync(ProgressDto progressDto);
    Task DeleteLessonAsync(Guid userId, Guid lessonId);
}