using AutoMapper;
using OnlineLearningPlatform.Application.DTOs;
using OnlineLearningPlatform.Application.Interfaces;
using OnlineLearningPlatform.Domain.Entities;

namespace OnlineLearningPlatform.Application.Services.CourseManagement;

public class CourseService(ICourseDAO courseDAO, IUnitOfWork unitOfWork, IMapper mapper) : ICourseService
{
    private readonly ICourseDAO courseDAO = courseDAO;
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
        Course course = mapper.Map<Course>(courseDto);
        await courseDAO.AddCourseAsync(course);
        await unitOfWork.SaveChangesAsync();
        return mapper.Map<CourseDto>(course);
    }

    public async Task DeleteCourseAsync(Guid userId, Guid courseId)
    {
        Course? course = await courseDAO.GetBasicCourseAsync(courseId);

        if (course is null) throw new KeyNotFoundException($"Course with ID {courseId} was not found.");
        if (course.CreatorId != userId) throw new UnauthorizedAccessException("You are not allowed to delete this course. You are not the creator.");

        await courseDAO.DeleteCourseAsync(course);
        await unitOfWork.SaveChangesAsync();
    }

    public async Task<CourseDto> UpdateCourseAsync(Guid userId, CourseDto courseDto)
    {
        Course? course = await courseDAO.GetBasicCourseAsync(courseDto.Id);

        if (course is null) throw new KeyNotFoundException($"Course with ID {courseDto.Id} was not found.");
        if (course.CreatorId != userId) throw new UnauthorizedAccessException("You are not allowed to update this course. You are not the creator.");

        mapper.Map(courseDto, course);
        await unitOfWork.SaveChangesAsync();

        return mapper.Map<CourseDto>(course);
    }

    public async Task<CourseDto> EnrollToCourseAsync(Guid userId, Guid courseId)
    {
        Course? course = await courseDAO.GetBasicCourseAsync(courseId);

        if (course is null) throw new KeyNotFoundException($"Course with ID {courseId} was not found.");
        if (course.Enrollments.Any(e => e.UserId == userId)) throw new InvalidOperationException("User is already enrolled in this course.");

        Enrollment enrollment = new Enrollment
        {
            UserId = userId,
            CourseId = courseId
        };

        course.Enrollments.Add(enrollment);
        await unitOfWork.SaveChangesAsync();

        return mapper.Map<CourseDto>(course);
    }

    public async Task UnenrollToCourseAsync(Guid userId, Guid courseId)
    {
        Course? course = await courseDAO.GetBasicCourseAsync(courseId);

        if (course is null) throw new KeyNotFoundException($"Course with ID {courseId} was not found.");

        Enrollment? enrollment = course.Enrollments.FirstOrDefault(e => e.UserId == userId);

        if (enrollment is null) throw new InvalidOperationException("User is not enrolled to this course.");

        course.Enrollments.Remove(enrollment);
        await unitOfWork.SaveChangesAsync();
    }
}