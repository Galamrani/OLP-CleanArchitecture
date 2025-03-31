using OnlineLearningPlatform.Application.Interfaces;
using OnlineLearningPlatform.Infrastructure.Persistence.Database;

namespace OnlineLearningPlatform.Infrastructure.Services.Persistence;

public abstract class BaseDataService(AppDbContext context) : IBaseDataService
{
    protected readonly AppDbContext context = context;

    public async Task SaveChangesAsync()
    {
        await context.SaveChangesAsync();
    }
}
