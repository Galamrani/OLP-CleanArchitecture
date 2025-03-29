using OnlineLearningPlatform.Application.Common;
using OnlineLearningPlatform.Application.DTOs;
using OnlineLearningPlatform.Application.Interfaces;
using OnlineLearningPlatform.Domain.Entities;

namespace OnlineLearningPlatform.Application.Services.Authentication;

public class AuthenticationService(ITokenGenerator tokenGenerator, IUserDAO userDAO, IUnitOfWork unitOfWork) : IAuthenticationService
{
    private readonly ITokenGenerator tokenGenerator = tokenGenerator;
    private readonly IUserDAO userDAO = userDAO;
    private readonly IUnitOfWork unitOfWork = unitOfWork;

    public async Task<string> Login(CredentialsDto credentialsDto)
    {
        User? user = await userDAO.GetUserByEmailAsync(credentialsDto.Email);

        if (user is null) throw new KeyNotFoundException($"User with Email {credentialsDto.Email} was not found.");
        if (user.Password != PasswordHasher.HashPassword(credentialsDto.Password)) throw new UnauthorizedAccessException("Invalid password.");

        return tokenGenerator.GenerateToken(user);
    }

    public async Task<string> Register(RegisterDto registerDto)
    {
        if (await userDAO.IsEmailTakenAsync(registerDto.Email)) throw new ArgumentException("Email is already in use.");

        User user = new User
        {
            Name = registerDto.Name,
            Email = registerDto.Email.ToLower(),
            Password = PasswordHasher.HashPassword(registerDto.Password)
        };

        await userDAO.AddUserAsync(user);
        await unitOfWork.SaveChangesAsync();

        return tokenGenerator.GenerateToken(user);
    }
}
