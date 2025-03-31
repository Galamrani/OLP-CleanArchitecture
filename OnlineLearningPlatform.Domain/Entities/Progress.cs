namespace OnlineLearningPlatform.Domain.Entities;

public class Progress
{
    public Guid Id { get; set; }
    public Guid UserId { get; set; }
    public Guid LessonId { get; set; }
    public DateTime LastWatchedAt { get; set; }
    public virtual User User { get; set; } = null!;
    public virtual Lesson Lesson { get; set; } = null!;
}