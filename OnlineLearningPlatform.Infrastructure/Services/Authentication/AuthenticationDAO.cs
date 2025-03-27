using Microsoft.EntityFrameworkCore;
using OnlineLearningPlatform.Application.Interfaces;
using OnlineLearningPlatform.Domain.Entities;
using OnlineLearningPlatform.Infrastructure.Persistence.Database;

namespace OnlineLearningPlatform.Infrastructure.Services.Authentication;

public class AuthenticationDAO(AppDbContext context) : IAuthenticationDAO
{
    private readonly AppDbContext context = context;

    public async Task<User?> GetUserByEmailAsync(string email)
    {
        return await context.Users.SingleOrDefaultAsync(u => u.Email == email.ToLower());
    }

    public async Task<bool> IsEmailTakenAsync(string email)
    {
        return await context.Users.AnyAsync(u => u.Email == email.ToLower());
    }

    public async Task RegisterAsync(User user)
    {
        await context.Users.AddAsync(user);
    }
}
