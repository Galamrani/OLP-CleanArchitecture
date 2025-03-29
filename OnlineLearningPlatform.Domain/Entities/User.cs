namespace OnlineLearningPlatform.Domain.Entities;

public class User
{
    public Guid Id { get; set; }
    public string Name { get; set; } = null!;
    public string Email { get; set; } = null!;
    public string Password { get; set; } = null!;
    public ICollection<Enrollment> EnrolledCourses { get; set; } = new HashSet<Enrollment>();
    public ICollection<Progress> Progresses { get; set; } = new HashSet<Progress>();
    public ICollection<Course> CreatedCourses { get; set; } = new HashSet<Course>();


    public Course CreateCourse(string title, string? description)
    {
        Course course = new Course
        {
            CreatorId = Id,
            Title = title,
            Description = description
        };

        CreatedCourses.Add(course);
        return course;
    }

    public void DeleteCourse(Course courseToDelete)
    {
        if (Id != courseToDelete.CreatorId) throw new UnauthorizedAccessException("You are not allowed to delete this course. You are not the creator.");

        CreatedCourses.Remove(courseToDelete);
    }

    public void Enroll(Guid courseId)
    {
        if (EnrolledCourses.Any(e => e.CourseId == courseId)) throw new ArgumentException("User is already enrolled in this course.");

        Enrollment enrollment = new Enrollment
        {
            UserId = Id,
            CourseId = courseId
        };

        EnrolledCourses.Add(enrollment);
    }

    public Enrollment RemoveEnrollment(Guid courseId)
    {
        // Manual removal from the db

        Enrollment? enrollment = EnrolledCourses.FirstOrDefault(e => e.CourseId == courseId);
        if (enrollment is null) throw new ArgumentException("User is not enrolled to this course.");

        EnrolledCourses.Remove(enrollment);
        return enrollment;
    }

}