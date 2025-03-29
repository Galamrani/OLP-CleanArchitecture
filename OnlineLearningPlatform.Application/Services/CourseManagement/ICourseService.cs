using OnlineLearningPlatform.Application.DTOs;

namespace OnlineLearningPlatform.Application.Services.CourseManagement;

public interface ICourseService
{
    Task<List<CourseDto>> GetCoursesAsync();
    Task<CourseDto> GetCourseWithLessonsAsync(Guid courseId);
    Task<CourseDto> GetCourseWithUserLessonProgressAsync(Guid userId, Guid courseId);
    Task<List<CourseDto>> GetUserCreatedCoursesAsync(Guid userId);
    Task<List<CourseDto>> GetUserEnrolledCoursesAsync(Guid userId);
    Task<CourseDto> AddCourseAsync(CourseDto courseDto);
    Task<CourseDto> UpdateCourseAsync(Guid userId, CourseDto courseDto);
    Task DeleteCourseAsync(Guid userId, Guid courseId);
}