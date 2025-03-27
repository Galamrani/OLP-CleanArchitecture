using AutoMapper;
using OnlineLearningPlatform.Application.DTOs;
using OnlineLearningPlatform.Application.Interfaces;
using OnlineLearningPlatform.Domain.Entities;

namespace OnlineLearningPlatform.Application.Services.CourseManagement;

public class CourseService(ICourseDAO courseDAO, IMapper mapper) : ICourseService
{
    private readonly ICourseDAO courseDAO = courseDAO;
    private readonly IMapper mapper = mapper;

    public async Task<CourseDto> GetBasicCourseAsync(Guid courseId)
    {
        Course? course = await courseDAO.GetBasicCourseAsync(courseId);

        if (course is null) throw new KeyNotFoundException($"Course with ID {courseId} was not found.");

        return mapper.Map<CourseDto>(course);
    }

    // TODO: understand why and if i need userId here?
    public async Task<CourseDto> GetFullCourseAsync(Guid userId, Guid courseId)
    {
        Course? course = await courseDAO.GetFullCourseAsync(userId, courseId);

        if (course is null) throw new KeyNotFoundException($"Course with ID {courseId} was not found.");

        return mapper.Map<CourseDto>(course);
    }

    public async Task<List<CourseDto>> GetCoursesAsync()
    {
        List<Course> courses = await courseDAO.GetCoursesAsync();
        return mapper.Map<List<CourseDto>>(courses);
    }

    public async Task<List<CourseDto>> GetUserCreatedCoursesAsync(Guid userId)
    {
        List<Course> courses = await courseDAO.GetUserCreatedCoursesAsync(userId);
        return mapper.Map<List<CourseDto>>(courses);
    }

    public async Task<List<CourseDto>> GetUserEnrolledCoursesAsync(Guid userId)
    {
        List<Course> courses = await courseDAO.GetUserEnrolledCoursesAsync(userId);
        return mapper.Map<List<CourseDto>>(courses);
    }

    public Task<CourseDto> AddCourseAsync(CourseDto courseDto)
    {
        throw new NotImplementedException();
    }

    public Task<bool> DeleteCourseAsync(Guid userId, Guid courseId)
    {
        throw new NotImplementedException();
    }

    public Task<CourseDto> UpdateCourseAsync(Guid userId, CourseDto courseDto)
    {
        throw new NotImplementedException();
    }
}