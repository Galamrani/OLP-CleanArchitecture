using OnlineLearningPlatform.Application.DTOs;

namespace OnlineLearningPlatform.Application.Services.Authentication;

public interface IAuthenticationService
{
    public Task<string> LoginAsync(CredentialsDto credentialsDto);
    public Task<string> RegisterAsync(RegisterDto registerDto);
}