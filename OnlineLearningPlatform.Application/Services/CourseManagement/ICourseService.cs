using OnlineLearningPlatform.Application.DTOs;
using OnlineLearningPlatform.Domain.Entities;

namespace OnlineLearningPlatform.Application.Services.CourseManagement;

public interface ICourseService
{
    Task<List<CourseDto>> GetCoursesAsync();
    Task<CourseDto> GetCourseAsync(Guid courseId);
    Task<CourseDto> AddCourseAsync(CourseDto courseDto);
    Task<CourseDto> UpdateCourseAsync(Guid userId, CourseDto courseDto);
    Task DeleteCourseAsync(Guid userId, Guid courseId);
}