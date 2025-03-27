namespace OnlineLearningPlatform.Application.Interfaces;

public interface IUnitOfWork
{
    Task SaveChangesAsync();
}
