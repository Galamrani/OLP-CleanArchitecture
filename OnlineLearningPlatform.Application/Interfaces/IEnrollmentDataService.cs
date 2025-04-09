using OnlineLearningPlatform.Domain.Entities;

namespace OnlineLearningPlatform.Application.Interfaces;

public interface IEnrollmentDataService : IBaseDataService
{
    Task<Enrollment?> GetUserEnrollmentAsync(Guid userid, Guid courseId);
    Task RemoveEnrollmentAsync(Enrollment enrollment);
    Task AddEnrollmentAsync(Enrollment enrollment);
}