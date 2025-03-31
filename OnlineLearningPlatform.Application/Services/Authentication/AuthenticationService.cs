using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using OnlineLearningPlatform.Application.Common;
using OnlineLearningPlatform.Application.Configurations;
using OnlineLearningPlatform.Application.DTOs;
using OnlineLearningPlatform.Application.Interfaces;
using OnlineLearningPlatform.Domain.Entities;

namespace OnlineLearningPlatform.Application.Services.Authentication;

public class AuthenticationService(IUserDataService userDataService, IOptions<JwtSettings> jwtOptions) : IAuthenticationService
{
    private readonly IUserDataService userDataService = userDataService;
    private readonly JwtSettings JwtSettings = jwtOptions.Value;

    public async Task<string> LoginAsync(CredentialsDto credentialsDto)
    {
        User? user = await userDataService.GetUserByEmailAsync(credentialsDto.Email);

        if (user is null) throw new KeyNotFoundException($"User with Email {credentialsDto.Email} was not found.");
        if (user.Password != PasswordHasher.HashPassword(credentialsDto.Password)) throw new UnauthorizedAccessException("Invalid password.");

        return GenerateToken(user);
    }

    public async Task<string> RegisterAsync(RegisterDto registerDto)
    {
        if (await userDataService.IsEmailTakenAsync(registerDto.Email)) throw new ArgumentException("Email is already in use.");

        User user = new User
        {
            Name = registerDto.Name,
            Email = registerDto.Email.ToLower(),
            Password = PasswordHasher.HashPassword(registerDto.Password)
        };

        await userDataService.AddUserAsync(user);
        await userDataService.SaveChangesAsync();

        return GenerateToken(user);
    }

    private string GenerateToken(User user)
    {
        var tokenHandler = new JwtSecurityTokenHandler();
        var key = Encoding.UTF8.GetBytes(JwtSettings.JwtKey);

        var tokenDescriptor = new SecurityTokenDescriptor
        {
            Subject = new ClaimsIdentity(new[]
            {
                new Claim(ClaimTypes.NameIdentifier, user.Id.ToString()), // Added userId (nameId) claim
                new Claim(ClaimTypes.Email, user.Email), // Added email claim
                new Claim(ClaimTypes.Name, user.Name) // Added name claim
            }),
            Expires = DateTime.UtcNow.AddHours(JwtSettings.Lifetime),
            Issuer = JwtSettings.Issuer,
            Audience = JwtSettings.Audience,
            SigningCredentials = new SigningCredentials(
                new SymmetricSecurityKey(key),
                SecurityAlgorithms.HmacSha256Signature
            )
        };

        var token = tokenHandler.CreateToken(tokenDescriptor);
        return tokenHandler.WriteToken(token);
    }
}
