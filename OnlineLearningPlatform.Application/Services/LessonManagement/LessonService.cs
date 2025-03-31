using AutoMapper;
using OnlineLearningPlatform.Application.DTOs;
using OnlineLearningPlatform.Application.Interfaces;
using OnlineLearningPlatform.Domain.Entities;

namespace OnlineLearningPlatform.Application.Services.LessonManagement;

public class LessonService(ILessonDataService lessonDataService, ICourseDataService courseDataService, IMapper mapper) : ILessonService
{
    private readonly ILessonDataService lessonDataService = lessonDataService;
    private readonly ICourseDataService courseDataService = courseDataService;

    private readonly IMapper mapper = mapper;

    public async Task<LessonDto> AddLessonAsync(Guid userId, LessonDto lessonDto)
    {
        Course? course = await courseDataService.GetCourseAsync(lessonDto.CourseId);

        if (course is null) throw new KeyNotFoundException($"Course with ID {lessonDto.CourseId} was not found.");
        if (course.CreatorId != userId) throw new UnauthorizedAccessException("You are not allowed to add lesson. You are not the creator.");

        Lesson lesson = mapper.Map<Lesson>(lessonDto);
        await lessonDataService.AddLesson(lesson);
        await courseDataService.SaveChangesAsync();

        return mapper.Map<LessonDto>(lesson);
    }

    public async Task DeleteLessonAsync(Guid userId, Guid lessonId)
    {
        Lesson? lesson = await lessonDataService.GetLessonAsync(userId, lessonId);

        if (lesson is null) throw new KeyNotFoundException($"Lesson with ID {lessonId} was not found.");
        if (lesson.Course.CreatorId != userId) throw new UnauthorizedAccessException("You are not allowed to delete this lesson. You are not the creator.");

        await lessonDataService.DeleteLesson(lesson);

        await lessonDataService.SaveChangesAsync();
    }

    public async Task<LessonDto> UpdateLessonAsync(Guid userId, LessonDto lessonDto)
    {
        Lesson? lesson = await lessonDataService.GetLessonAsync(userId, lessonDto.Id);

        if (lesson is null) throw new KeyNotFoundException($"Lesson with ID {lessonDto.Id} was not found.");
        if (lesson.Course.CreatorId != userId) throw new UnauthorizedAccessException("You are not allowed to update this lesson. You are not the creator.");

        lesson.Title = lessonDto.Title;
        lesson.Description = lessonDto.Description;
        lesson.VideoUrl = lessonDto.VideoUrl;

        await lessonDataService.SaveChangesAsync();

        return mapper.Map<LessonDto>(lesson);
    }

    public async Task<ProgressDto> AddLessonProgressAsync(ProgressDto progressDto)
    {
        Lesson? lesson = await lessonDataService.GetLessonAsync(progressDto.UserId, progressDto.LessonId);

        if (lesson is null) throw new KeyNotFoundException($"Lesson with ID {progressDto.LessonId} was not found.");

        Progress progress = mapper.Map<Progress>(progressDto);

        await lessonDataService.AddProgressAsync(progress);
        await lessonDataService.SaveChangesAsync();

        return mapper.Map<ProgressDto>(progress);
    }
}
