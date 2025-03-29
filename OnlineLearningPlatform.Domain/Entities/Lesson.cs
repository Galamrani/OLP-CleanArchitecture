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


    public void Update(Guid userId, string title, string? description, string videoUrl)
    {
        if (Course.CreatorId != userId) throw new UnauthorizedAccessException("You are not allowed to update this lesson. You are not the creator.");

        Title = title;
        Description = description;
        VideoUrl = videoUrl;
    }

    public void AddProgress(Progress progress)
    {
        Progresses.Add(progress);
    }
}