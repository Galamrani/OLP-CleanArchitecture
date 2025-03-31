namespace OnlineLearningPlatform.Domain.Entities;

public class Course
{
    public Guid Id { get; set; }
    public Guid CreatorId { get; set; }
    public string Title { get; set; } = null!;
    public string? Description { get; set; }
    public DateTime CreatedAt { get; set; }
    public virtual User? Creator { get; set; } = null!;
    public virtual ICollection<Enrollment> Enrollments { get; set; } = new HashSet<Enrollment>();
    public virtual ICollection<Lesson> Lessons { get; set; } = new HashSet<Lesson>();
}