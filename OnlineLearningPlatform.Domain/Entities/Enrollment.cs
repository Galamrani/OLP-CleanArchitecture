namespace OnlineLearningPlatform.Domain.Entities;

public class Enrollment
{
    public Guid Id { get; set; }
    public Guid UserId { get; set; }
    public Guid CourseId { get; set; }
    public DateTime EnrolledAt { get; set; }
    public virtual User User { get; set; } = null!;
    public virtual Course Course { get; set; } = null!;
}