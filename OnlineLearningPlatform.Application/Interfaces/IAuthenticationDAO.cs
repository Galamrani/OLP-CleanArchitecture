using OnlineLearningPlatform.Application.DTOs;
using OnlineLearningPlatform.Domain.Entities;

namespace OnlineLearningPlatform.Application.Interfaces;

public interface IAuthenticationDAO
{
    Task RegisterAsync(User user);
    Task<User?> GetUserByEmailAsync(string email);
    Task<bool> IsEmailTakenAsync(string email);

}