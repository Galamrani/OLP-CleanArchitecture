namespace OnlineLearningPlatform.Domain.Entities;

public class Enrollment
{
    public Guid Id { get; set; }

    public Guid UserId { get; set; }

    public Guid CourseId { get; set; }

    public DateTime EnrolledAt { get; set; }

    public User User { get; set; } = null!;

    public Course Course { get; set; } = null!;
}