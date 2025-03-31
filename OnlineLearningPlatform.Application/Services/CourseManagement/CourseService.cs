using AutoMapper;
using OnlineLearningPlatform.Application.DTOs;
using OnlineLearningPlatform.Application.Interfaces;
using OnlineLearningPlatform.Domain.Entities;

namespace OnlineLearningPlatform.Application.Services.CourseManagement;

public class CourseService(ICourseDataService courseDataService, IUserDataService userDataService, IMapper mapper) : ICourseService
{
    private readonly ICourseDataService courseDataService = courseDataService;
    private readonly IUserDataService userDataService = userDataService;
    private readonly IMapper mapper = mapper;

    public async Task<List<CourseDto>> GetCoursesAsync()
    {
        List<Course> courses = await courseDataService.GetCoursesAsync();
        return mapper.Map<List<CourseDto>>(courses);
    }

    public async Task<CourseDto> GetCourseAsync(Guid userId, Guid courseId)
    {
        Course? course;
        if (userId == Guid.Empty) course = await courseDataService.GetCourseAsync(courseId);
        else course = await courseDataService.GetCourseWithUserProgressAsync(userId, courseId);

        if (course is null) throw new KeyNotFoundException($"Course with ID {courseId} was not found.");

        return mapper.Map<CourseDto>(course);
    }

    public async Task<CourseDto> AddCourseAsync(CourseDto courseDto)
    {
        Course course = mapper.Map<Course>(courseDto);

        await courseDataService.AddCourseAsync(course);
        await userDataService.SaveChangesAsync();

        return mapper.Map<CourseDto>(course);
    }

    public async Task DeleteCourseAsync(Guid userId, Guid courseId)
    {
        Course? course = await courseDataService.GetCourseAsync(courseId);
        if (course is null) throw new KeyNotFoundException($"Course with ID {courseId} was not found.");

        await courseDataService.DeleteCourseAsync(course);

        await userDataService.SaveChangesAsync();
    }

    public async Task<CourseDto> UpdateCourseAsync(Guid userId, CourseDto courseDto)
    {
        Course? course = await courseDataService.GetCourseWithUserProgressAsync(userId, courseDto.Id);

        if (course is null) throw new KeyNotFoundException($"Course with ID {courseDto.Id} was not found.");
        if (course.CreatorId != userId) throw new UnauthorizedAccessException("You are not allowed to delete lesson from this course. You are not the creator."); course.Title = courseDto.Title;

        course.Title = courseDto.Title;
        course.Description = courseDto.Description;

        await courseDataService.SaveChangesAsync();

        return mapper.Map<CourseDto>(course);
    }
}