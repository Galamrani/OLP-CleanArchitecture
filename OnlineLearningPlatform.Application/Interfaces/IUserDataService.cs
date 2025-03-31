using OnlineLearningPlatform.Domain.Entities;

namespace OnlineLearningPlatform.Application.Interfaces;

public interface IUserDataService : IBaseDataService
{
    Task<User?> GetUserByIdAsync(Guid userId);
    Task<User?> GetUserByEmailAsync(string email);
    Task<Enrollment?> GetUserEnrollmentAsync(Guid userid, Guid courseId);
    Task RemoveEnrollmentAsync(Enrollment enrollment);
    Task AddEnrollmentAsync(Enrollment enrollment);
    Task AddUserAsync(User user);
    Task<bool> IsEmailTakenAsync(string email);
}
