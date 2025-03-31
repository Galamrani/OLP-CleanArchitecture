using AutoMapper;
using OnlineLearningPlatform.Application.DTOs;
using OnlineLearningPlatform.Application.Interfaces;
using OnlineLearningPlatform.Domain.Entities;

namespace OnlineLearningPlatform.Application.Services.UserManagement;

public class UserService(IUserDataService userDataService, ICourseDataService courseDataService, IMapper mapper) : IUserService
{
    private readonly IUserDataService userDataService = userDataService;
    private readonly ICourseDataService courseDataService = courseDataService;

    public async Task<List<CourseDto>> GetUserCreatedCoursesAsync(Guid userId)
    {
        List<Course> courses = await courseDataService.GetUserCreatedCoursesAsync(userId);
        return mapper.Map<List<CourseDto>>(courses);
    }

    public async Task<List<CourseDto>> GetUserEnrolledCoursesAsync(Guid userId)
    {
        List<Course> courses = await courseDataService.GetUserEnrolledCoursesAsync(userId);
        return mapper.Map<List<CourseDto>>(courses);
    }

    public async Task<CourseDto> CreateEnrollmentAsync(Guid userId, Guid courseId)
    {
        User? user = await userDataService.GetUserByIdAsync(userId);
        if (user is null) throw new KeyNotFoundException($"User with ID {userId} was not found.");
        if (user.EnrolledCourses.Any(c => c.CourseId == courseId)) throw new InvalidOperationException($"User already enrolled to course ID {courseId}.");

        Enrollment enrollment = new Enrollment { UserId = userId, CourseId = courseId };
        await userDataService.AddEnrollmentAsync(enrollment);

        await userDataService.SaveChangesAsync();

        Course? course = await courseDataService.GetCourseWithUserProgressAsync(userId, courseId);
        if (course is null) throw new KeyNotFoundException($"Course with ID {courseId} was not found.");

        return mapper.Map<CourseDto>(course);
    }

    public async Task DeleteEnrollmentAsync(Guid userId, Guid courseId)
    {
        Enrollment? enrollment = await userDataService.GetUserEnrollmentAsync(userId, courseId);
        if (enrollment is null) throw new KeyNotFoundException($"Enrollment with user ID {userId} and Course ID {courseId} was not found.");

        await userDataService.RemoveEnrollmentAsync(enrollment);
        await courseDataService.SaveChangesAsync();
    }
}