using Microsoft.EntityFrameworkCore;
using OnlineLearningPlatform.Application.Interfaces;
using OnlineLearningPlatform.Domain.Entities;
using OnlineLearningPlatform.Infrastructure.Persistence.Database;
using OnlineLearningPlatform.Infrastructure.Services.Persistence;

namespace OnlineLearningPlatform.Infrastructure.Services.UserManagement;

public class UserDataService : BaseDataService, IUserDataService
{
    public UserDataService(AppDbContext context) : base(context) { }

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

    public async Task<Enrollment?> GetUserEnrollmentAsync(Guid userid, Guid courseId)
    {
        return await context.Enrollments.SingleOrDefaultAsync(e => e.CourseId == courseId && e.UserId == userid);
    }

    public async Task AddUserAsync(User user)
    {
        await context.Users.AddAsync(user);
    }

    public async Task<bool> IsEmailTakenAsync(string email)
    {
        return await context.Users.AnyAsync(u => u.Email == email.ToLower());
    }

    public async Task AddEnrollmentAsync(Enrollment enrollment)
    {
        await context.Enrollments.AddAsync(enrollment);
    }

    public Task RemoveEnrollmentAsync(Enrollment enrollment)
    {
        context.Enrollments.Remove(enrollment);
        return Task.CompletedTask;
    }
}