namespace OnlineLearningPlatform.Domain.Entities;

public class User
{
    public Guid Id { get; set; }
    public string Name { get; set; } = null!;
    public string Email { get; set; } = null!;
    public string Password { get; set; } = null!;
    public virtual ICollection<Enrollment> EnrolledCourses { get; set; } = new HashSet<Enrollment>();
    public virtual ICollection<Progress> Progresses { get; set; } = new HashSet<Progress>();
    public virtual ICollection<Course> CreatedCourses { get; set; } = new HashSet<Course>();
}