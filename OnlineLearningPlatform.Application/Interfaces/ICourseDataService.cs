using OnlineLearningPlatform.Application.DTOs;
using OnlineLearningPlatform.Domain.Entities;

namespace OnlineLearningPlatform.Application.Interfaces;

public interface ICourseDataService : IBaseDataService
{
    Task<Course?> GetEnrolledCourseWithProgressAsync(Guid userId, Guid courseId);
    Task<Course?> GetCourseAsync(Guid courseId);
    Task<List<Course>> GetCoursesAsync();
    Task<List<Course>> GetUserCreatedCoursesAsync(Guid userId);
    Task<List<Course>> GetUserEnrolledCoursesAsync(Guid userId);
    Task AddCourseAsync(Course course);
    Task DeleteCourseAsync(Course course);
}