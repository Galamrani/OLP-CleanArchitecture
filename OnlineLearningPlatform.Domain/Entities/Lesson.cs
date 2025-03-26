namespace OnlineLearningPlatform.Domain.Entities;


public class Lesson
{
    public Guid Id { get; set; }

    public Guid CourseId { get; set; }

    public string Title { get; set; } = null!;

    public string? Description { get; set; }

    public string VideoUrl { get; set; } = null!;

    public Course Course { get; set; } = null!;

    public ICollection<Progress> Progresses { get; set; } = new HashSet<Progress>();
}