using AutoMapper;
using OnlineLearningPlatform.Application.DTOs;
using OnlineLearningPlatform.Application.Interfaces;
using OnlineLearningPlatform.Domain.Entities;

namespace OnlineLearningPlatform.Application.Services.LessonManagement;

public class LessonService(ILessonDAO lessonDAO, IUnitOfWork unitOfWork, IMapper mapper) : ILessonService
{
    private readonly ILessonDAO lessonDAO = lessonDAO;
    private readonly IUnitOfWork unitOfWork = unitOfWork;
    private readonly IMapper mapper = mapper;

    public async Task<LessonDto> AddLessonAsync(Guid userId, LessonDto lessonDto)
    {
        if (!await IsCreatorByCourseId(userId, lessonDto.CourseId)) throw new UnauthorizedAccessException("You are not allowed to add this lesson. You are not the course creator.");

        Lesson lesson = mapper.Map<Lesson>(lessonDto);
        await lessonDAO.AddLessonAsync(lesson);
        await unitOfWork.SaveChangesAsync();
        return mapper.Map<LessonDto>(lesson);
    }

    public async Task<ProgressDto> AddProgressAsync(ProgressDto progressDto)
    {
        Lesson? lesson = await lessonDAO.GetLessonAsync(progressDto.LessonId);

        if (lesson is null) throw new KeyNotFoundException($"Lesson with ID {progressDto.LessonId} was not found.");

        Progress progress = mapper.Map<Progress>(progressDto);
        lesson.Progresses.Add(progress);
        await unitOfWork.SaveChangesAsync();

        return mapper.Map<ProgressDto>(progress);
    }

    public async Task DeleteLessonAsync(Guid userId, Guid lessonId)
    {
        Lesson? lesson = await lessonDAO.GetLessonAsync(lessonId);

        if (lesson is null) throw new KeyNotFoundException($"Lesson with ID {lessonId} was not found.");
        if (!await IsCreatorByCourseId(userId, lesson.CourseId)) throw new UnauthorizedAccessException("You are not allowed to delete this lesson. You are not the creator.");

        await lessonDAO.DeleteLessonAsync(lesson);
        await unitOfWork.SaveChangesAsync();
    }

    public async Task<LessonDto> UpdateLessonAsync(Guid userId, LessonDto lessonDto)
    {
        Lesson? lesson = await lessonDAO.GetLessonAsync(lessonDto.Id);

        if (lesson is null) throw new KeyNotFoundException($"Lesson with ID {lessonDto.Id} was not found.");
        if (!await IsCreatorByCourseId(userId, lessonDto.CourseId)) throw new UnauthorizedAccessException("You are not allowed to update this lesson. You are not the creator.");

        mapper.Map(lessonDto, lesson);
        await unitOfWork.SaveChangesAsync();

        return mapper.Map<LessonDto>(lesson);
    }

    private async Task<bool> IsCreatorByCourseId(Guid userId, Guid courseId)
    {
        Guid? creatorId = await lessonDAO.GetLessonCreatorIdAsync(courseId);
        if (creatorId is not null && creatorId == userId) return true;
        return false;
    }
}
