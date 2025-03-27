using OnlineLearningPlatform.Application.DTOs;
using OnlineLearningPlatform.Domain.Entities;

namespace OnlineLearningPlatform.Application.Interfaces;

public interface ICourseDAO
{
    Task<Course?> GetFullCourseAsync(Guid userId, Guid courseId);
    Task<Course?> GetBasicCourseAsync(Guid courseId);
    Task<List<Course>> GetCoursesAsync();
    Task<List<Course>> GetUserCreatedCoursesAsync(Guid userId);
    Task<List<Course>> GetUserEnrolledCoursesAsync(Guid userId);
    Task<List<Enrollment>> GetUserEnrollmentsAsync(Guid userId);
    Task AddCourseAsync(Course course);
    Task DeleteCourseAsync(Course course);
}