using AutoMapper;
using OnlineLearningPlatform.Application.DTOs;
using OnlineLearningPlatform.Application.Interfaces;
using OnlineLearningPlatform.Domain.Entities;

namespace OnlineLearningPlatform.Application.Services.LessonManagement;

public class LessonService(ILessonDAO lessonDAO, ICourseDAO courseDAO, IUnitOfWork unitOfWork, IMapper mapper) : ILessonService
{
    private readonly ILessonDAO lessonDAO = lessonDAO;
    private readonly ICourseDAO courseDAO = courseDAO;
    private readonly IUnitOfWork unitOfWork = unitOfWork;
    private readonly IMapper mapper = mapper;

    public async Task<LessonDto> AddLessonAsync(Guid userId, LessonDto lessonDto)
    {
        Course? course = await courseDAO.GetCourseWithLessonsAsync(lessonDto.CourseId);
        if (course is null) throw new KeyNotFoundException($"Course with ID {lessonDto.CourseId} was not found.");

        Lesson lesson = mapper.Map<Lesson>(lessonDto);

        course.AddLesson(userId, lesson);

        await unitOfWork.SaveChangesAsync();
        return mapper.Map<LessonDto>(lesson);
    }

    public async Task<ProgressDto> AddProgressAsync(ProgressDto progressDto)
    {
        Lesson? lesson = await lessonDAO.GetLessonAsync(progressDto.UserId, progressDto.LessonId);
        if (lesson is null) throw new KeyNotFoundException($"Lesson with ID {progressDto.LessonId} was not found.");

        Progress progress = mapper.Map<Progress>(progressDto);

        lesson.AddProgress(progress);

        await unitOfWork.SaveChangesAsync();
        return mapper.Map<ProgressDto>(progress);
    }

    public async Task DeleteLessonAsync(Guid userId, Guid lessonId)
    {
        Lesson? lesson = await lessonDAO.GetLessonAsync(userId, lessonId);
        if (lesson is null) throw new KeyNotFoundException($"Lesson with ID {lessonId} was not found.");

        Course? course = await courseDAO.GetCourseWithLessonsAsync(lesson.CourseId);
        if (course is null) throw new KeyNotFoundException($"Course with ID {lesson.CourseId} was not found.");

        course.DeleteLesson(userId, lesson);
        await unitOfWork.SaveChangesAsync();
    }

    public async Task<LessonDto> UpdateLessonAsync(Guid userId, LessonDto lessonDto)
    {
        Lesson? lesson = await lessonDAO.GetLessonAsync(userId, lessonDto.Id);
        if (lesson is null) throw new KeyNotFoundException($"Lesson with ID {lessonDto.Id} was not found.");

        lesson.Update(userId, lessonDto.Title, lessonDto.Description, lessonDto.VideoUrl);

        await unitOfWork.SaveChangesAsync();
        return mapper.Map<LessonDto>(lesson);
    }
}
