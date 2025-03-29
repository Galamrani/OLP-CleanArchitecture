namespace OnlineLearningPlatform.Domain.Entities;

public class Course
{
    public Guid Id { get; set; }
    public Guid CreatorId { get; set; }
    public string Title { get; set; } = null!;
    public string? Description { get; set; }
    public DateTime CreatedAt { get; set; }
    public User? Creator { get; set; } = null!;
    public ICollection<Enrollment> Enrollments { get; set; } = new HashSet<Enrollment>();
    public ICollection<Lesson> Lessons { get; set; } = new HashSet<Lesson>();


    public void Update(Guid userId, string title, string? description)
    {
        if (CreatorId != userId)
            throw new UnauthorizedAccessException("You are not allowed to update this course. You are not the creator.");

        Title = title;
        Description = description;
    }

    public void AddLesson(Guid userId, Lesson lessonToAdd)
    {
        if (CreatorId != userId)
            throw new UnauthorizedAccessException("You are not allowed to add lesson to this course. You are not the creator.");

        Lessons.Add(lessonToAdd);
    }

    public void DeleteLesson(Guid userId, Lesson lessonToRemove)
    {
        if (CreatorId != userId)
            throw new UnauthorizedAccessException("You are not allowed to delete lesson from this course. You are not the creator.");

        Lessons.Remove(lessonToRemove);
    }
}