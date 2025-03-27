using OnlineLearningPlatform.Application.Interfaces;
using OnlineLearningPlatform.Infrastructure.Persistence.Database;

namespace OnlineLearningPlatform.Infrastructure.Services.Persistence;

public class UnitOfWork(AppDbContext context) : IUnitOfWork
{
    private readonly AppDbContext context = context;

    public async Task SaveChangesAsync()
    {
        await context.SaveChangesAsync();
    }
}
