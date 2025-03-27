using OnlineLearningPlatform.Application.DTOs;
using OnlineLearningPlatform.Application.Interfaces;
using OnlineLearningPlatform.Domain.Entities;
using OnlineLearningPlatform.Domain.Utils;

namespace OnlineLearningPlatform.Application.Services.Authentication;

public class AuthenticationService(ITokenGenerator tokenGenerator, IAuthenticationDAO authenticationDAO, IUnitOfWork unitOfWork) : IAuthenticationService
{
    private readonly ITokenGenerator tokenGenerator = tokenGenerator;
    private readonly IAuthenticationDAO authenticationDAO = authenticationDAO;
    private readonly IUnitOfWork unitOfWork = unitOfWork;

    public async Task<string> Login(CredentialsDto credentialsDto)
    {
        User? user = await authenticationDAO.GetUserByEmailAsync(credentialsDto.Email);

        if (user is null) throw new KeyNotFoundException($"User with Email {credentialsDto.Email} was not found.");
        if (user.Password != PasswordHasher.HashPassword(credentialsDto.Password)) throw new UnauthorizedAccessException("Invalid password.");

        return tokenGenerator.GenerateToken(user);
    }

    public async Task<string> Register(RegisterDto registerDto)
    {
        if (await authenticationDAO.IsEmailTakenAsync(registerDto.Email)) throw new InvalidOperationException("Email is already in use.");

        User user = new User
        {
            Name = registerDto.Name,
            Email = registerDto.Email.ToLower(),
            Password = PasswordHasher.HashPassword(registerDto.Password)
        };

        await authenticationDAO.RegisterAsync(user);
        await unitOfWork.SaveChangesAsync();

        return tokenGenerator.GenerateToken(user);
    }
}
