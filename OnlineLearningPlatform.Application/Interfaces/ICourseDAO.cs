using OnlineLearningPlatform.Application.DTOs;
using OnlineLearningPlatform.Domain.Entities;

namespace OnlineLearningPlatform.Application.Interfaces;

public interface ICourseDAO
{
    Task<Course?> GetCourseWithUserLessonProgressAsync(Guid userId, Guid courseId);
    Task<Course?> GetCourseWithLessonsAsync(Guid courseId);
    Task<List<Course>> GetCoursesAsync();
    Task<List<Course>> GetUserCreatedCoursesAsync(Guid userId);
    Task<List<Course>> GetUserEnrolledCoursesAsync(Guid userId);
}