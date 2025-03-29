using AutoMapper;
using OnlineLearningPlatform.Application.DTOs;
using OnlineLearningPlatform.Application.Interfaces;
using OnlineLearningPlatform.Domain.Entities;

namespace OnlineLearningPlatform.Application.Services.CourseManagement;

public class CourseService(ICourseDAO courseDAO, IUserDAO userDAO, IUnitOfWork unitOfWork, IMapper mapper) : ICourseService
{
    private readonly ICourseDAO courseDAO = courseDAO;
    private readonly IUserDAO userDAO = userDAO;
    private readonly IUnitOfWork unitOfWork = unitOfWork;
    private readonly IMapper mapper = mapper;

    public async Task<CourseDto> GetBasicCourseAsync(Guid courseId)
    {
        Course? course = await courseDAO.GetBasicCourseAsync(courseId);
        if (course is null) throw new KeyNotFoundException($"Course with ID {courseId} was not found.");

        return mapper.Map<CourseDto>(course);
    }

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

    public async Task<CourseDto> AddCourseAsync(CourseDto courseDto)
    {
        User? creator = await userDAO.GetUserByIdAsync(courseDto.CreatorId);
        if (creator is null) throw new KeyNotFoundException($"User with ID {courseDto.CreatorId} was not found.");

        Course course = creator.CreateCourse(courseDto.Title, courseDto.Description);

        await unitOfWork.SaveChangesAsync();
        return mapper.Map<CourseDto>(course);
    }

    public async Task DeleteCourseAsync(Guid userId, Guid courseId)
    {
        User? user = await userDAO.GetUserByIdAsync(userId);
        if (user is null) throw new KeyNotFoundException($"User with ID {userId} was not found.");

        Course? course = await courseDAO.GetBasicCourseAsync(courseId);
        if (course is null) throw new KeyNotFoundException($"Course with ID {courseId} was not found.");

        user.DeleteCourse(course);
        await unitOfWork.SaveChangesAsync();
    }

    public async Task<CourseDto> UpdateCourseAsync(Guid userId, CourseDto courseDto)
    {
        Course? course = await courseDAO.GetFullCourseAsync(userId, courseDto.Id);
        if (course is null) throw new KeyNotFoundException($"Course with ID {courseDto.Id} was not found.");

        course.Update(userId, courseDto.Title, courseDto.Description);

        await unitOfWork.SaveChangesAsync();
        return mapper.Map<CourseDto>(course);
    }
}