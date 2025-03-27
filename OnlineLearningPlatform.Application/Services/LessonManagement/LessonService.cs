using OnlineLearningPlatform.Application.DTOs;
using OnlineLearningPlatform.Application.Interfaces;

namespace OnlineLearningPlatform.Application.Services.LessonManagement;

public class LessonService(ILessonDAO lessonDAO) : ILessonService
{
    private readonly ILessonDAO lessonDAO = lessonDAO;

    public Task<LessonDto> AddLessonAsync(Guid userId, LessonDto lessonDto)
    {
        throw new NotImplementedException();
    }

    public Task<ProgressDto> AddProgressAsync(ProgressDto progressDto)
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
