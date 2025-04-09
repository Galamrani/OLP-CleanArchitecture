using Microsoft.EntityFrameworkCore;
using OnlineLearningPlatform.Application.Interfaces;
using OnlineLearningPlatform.Domain.Entities;
using OnlineLearningPlatform.Infrastructure.Persistence.Database;
using OnlineLearningPlatform.Infrastructure.Services.Persistence;

namespace OnlineLearningPlatform.Infrastructure.Services;

public class EnrollmentDataService : BaseDataService, IEnrollmentDataService
{
    public EnrollmentDataService(AppDbContext context) : base(context) { }

    public async Task<Enrollment?> GetUserEnrollmentAsync(Guid userid, Guid courseId)
    {
        return await context.Enrollments.SingleOrDefaultAsync(e => e.CourseId == courseId && e.UserId == userid);
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