using OnlineLearningPlatform.Domain.Entities;

namespace OnlineLearningPlatform.Application.Interfaces;

public interface IUserDAO
{
    Task<User?> GetUserByIdAsync(Guid userId);
    Task<User?> GetUserByEmailAsync(string email);
    Task RemoveEnrollment(Enrollment enrollment);
    Task AddUserAsync(User user);
    Task<bool> IsEmailTakenAsync(string email);
}
