using OnlineLearningPlatform.Application.DTOs;

namespace OnlineLearningPlatform.Application.Services.Authentication;

public interface IAuthenticationService
{
    public Task<string> Login(CredentialsDto credentialsDto);
    public Task<string> Register(RegisterDto registerDto);
}