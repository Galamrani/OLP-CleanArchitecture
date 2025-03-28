using OnlineLearningPlatform.Application.DTOs;

namespace OnlineLearningPlatform.Application.Services.UserManagement;

public interface IUserService
{
    Task<CourseDto> EnrollToCourseAsync(Guid userId, Guid courseId);
    Task UnenrollToCourseAsync(Guid userId, Guid courseId);
}