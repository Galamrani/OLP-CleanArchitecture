using OnlineLearningPlatform.Application.DTOs;
using OnlineLearningPlatform.Application.Interfaces;

namespace OnlineLearningPlatform.Infrastructure.Services.LessonManagement;

public class LessonDAO : ILessonDAO
{
    public Task<LessonDto> AddLessonAsync(Guid userId, LessonDto lessonDto)
    {
        throw new NotImplementedException();
    }

    public Task<bool> AddProgressAsync(ProgressDto progressDto)
    {
        throw new NotImplementedException();
    }

    public Task<bool> DeleteLessonAsync(Guid userId, Guid lessonId)
    {
        throw new NotImplementedException();
    }

    public Task<LessonDto> UpdateLessonAsync(Guid userId, LessonDto lessonDto)
    {
        throw new NotImplementedException();
    }
}