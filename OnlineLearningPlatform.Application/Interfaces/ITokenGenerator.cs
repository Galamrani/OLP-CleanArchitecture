using OnlineLearningPlatform.Domain.Entities;

namespace OnlineLearningPlatform.Application.Interfaces;

public interface ITokenGenerator
{
    public string GenerateToken(User user);
}