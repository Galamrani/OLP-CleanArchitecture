using OnlineLearningPlatform.Application.DTOs;

namespace OnlineLearningPlatform.Application.Services.LessonManagement;

public interface ILessonService
{
    Task<LessonDto> AddLessonAsync(Guid userId, LessonDto lessonDto);
    Task<LessonDto> UpdateLessonAsync(Guid userId, LessonDto lessonDto);
    Task DeleteLessonAsync(Guid userId, Guid lessonId);
    Task<ProgressDto> AddLessonProgressAsync(ProgressDto progressDto);

}