using OnlineLearningPlatform.Application.DTOs;

namespace OnlineLearningPlatform.Application.Services.CourseManagement;

public interface ICourseService
{
    Task<List<CourseDto>> GetCoursesAsync();
    Task<CourseDto> GetBasicCourseAsync(Guid courseId);
    Task<CourseDto> GetFullCourseAsync(Guid userId, Guid courseId);
    Task<List<CourseDto>> GetUserCreatedCoursesAsync(Guid userId);
    Task<List<CourseDto>> GetUserEnrolledCoursesAsync(Guid userId);
    Task<CourseDto> AddCourseAsync(CourseDto courseDto);
    Task<CourseDto> UpdateCourseAsync(Guid userId, CourseDto courseDto);
    Task<CourseDto> EnrollToCourseAsync(Guid userId, Guid courseId);
    Task DeleteCourseAsync(Guid userId, Guid courseId);
    Task UnenrollToCourseAsync(Guid userId, Guid courseId);
}