using OnlineLearningPlatform.Application.DTOs;

namespace OnlineLearningPlatform.Application.Services.UserManagement;

public interface IUserService
{
    Task<List<CourseDto>> GetUserCreatedCoursesAsync(Guid userId);
    Task<List<CourseDto>> GetUserEnrolledCoursesAsync(Guid userId);
    Task<CourseDto> GetEnrolledCourseAsync(Guid userId, Guid courseId);
    Task<CourseDto> CreateEnrollmentAsync(Guid userId, Guid courseId);
    Task DeleteEnrollmentAsync(Guid userId, Guid courseId);
}