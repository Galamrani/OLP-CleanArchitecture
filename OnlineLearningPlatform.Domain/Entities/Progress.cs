namespace OnlineLearningPlatform.Domain.Entities;

public class Progress
{
    public Guid Id { get; set; }

    public Guid UserId { get; set; }

    public Guid LessonId { get; set; }

    public DateTime LastWatchedAt { get; set; }

    public User User { get; set; } = null!;

    public Lesson Lesson { get; set; } = null!;
}