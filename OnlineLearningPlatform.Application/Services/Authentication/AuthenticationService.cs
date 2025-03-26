using OnlineLearningPlatform.Application.DTOs;
using OnlineLearningPlatform.Application.Interfaces;
using OnlineLearningPlatform.Domain.Entities;
using OnlineLearningPlatform.Domain.Utils;

namespace OnlineLearningPlatform.Application.Services.Authentication;

public class AuthenticationService(ITokenGenerator tokenGenerator) : IAuthenticationService
{
    private readonly ITokenGenerator tokenGenerator = tokenGenerator;

    public async Task<string> Login(CredentialsDto credentialsDto)
    {
        // TODO: use auto mapper
        // TODO: change to real logic
        User user = new User()
        {
            Name = "Test",
            Email = credentialsDto.Email.ToLower(),
            Password = PasswordHasher.HashPassword(credentialsDto.Password)
        };

        return tokenGenerator.GenerateToken(user);
    }

    public async Task<string> Register(RegisterDto registerDto)
    {
        // TODO: use auto mapper
        User user = new User()
        {
            Name = registerDto.Name,
            Email = registerDto.Email.ToLower(),
            Password = PasswordHasher.HashPassword(registerDto.Password)
        };

        return tokenGenerator.GenerateToken(user);
    }
}
