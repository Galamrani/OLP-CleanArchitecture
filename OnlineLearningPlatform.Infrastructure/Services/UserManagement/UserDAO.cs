using Microsoft.EntityFrameworkCore;
using OnlineLearningPlatform.Application.Interfaces;
using OnlineLearningPlatform.Domain.Entities;
using OnlineLearningPlatform.Infrastructure.Persistence.Database;

namespace OnlineLearningPlatform.Infrastructure.Services.UserManagement;

public class UserDAO(AppDbContext context) : IUserDAO
{
    private readonly AppDbContext context = context;

    public async Task<User?> GetUserByEmailAsync(string email)
    {
        return await context.Users.SingleOrDefaultAsync(u => u.Email == email.ToLower());
    }

    public async Task<User?> GetUserByIdAsync(Guid userId)
    {
        return await context.Users
            .Include(u => u.EnrolledCourses)
            .SingleOrDefaultAsync(u => u.Id == userId);
    }

    public async Task AddUserAsync(User user)
    {
        await context.Users.AddAsync(user);
    }

    public async Task<bool> IsEmailTakenAsync(string email)
    {
        return await context.Users.AnyAsync(u => u.Email == email.ToLower());
    }

    public Task RemoveEnrollment(Enrollment enrollment)
    {
        context.Enrollments.Remove(enrollment);
        return Task.CompletedTask;
    }
}