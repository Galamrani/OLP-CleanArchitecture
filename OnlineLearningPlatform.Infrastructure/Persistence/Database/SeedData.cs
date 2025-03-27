using OnlineLearningPlatform.Domain.Entities;
using OnlineLearningPlatform.Domain.Utils;

namespace OnlineLearningPlatform.Infrastructure.Persistence.Database;
public class SeedData
{
    private readonly Dictionary<string, Guid> _guids;

    public SeedData()
    {
        _guids = new Dictionary<string, Guid>
        {
            // Users
            ["user1"] = Guid.NewGuid(),
            ["user2"] = Guid.NewGuid(),
            ["user3"] = Guid.NewGuid(),

            // Courses
            ["course1"] = Guid.NewGuid(),
            ["course2"] = Guid.NewGuid(),
            ["course3"] = Guid.NewGuid(),
            ["course4"] = Guid.NewGuid(),

            // Lessons
            ["lesson1"] = Guid.NewGuid(),
            ["lesson2"] = Guid.NewGuid(),
            ["lesson3"] = Guid.NewGuid(),
            ["lesson4"] = Guid.NewGuid(),
            ["lesson5"] = Guid.NewGuid(),
            ["lesson6"] = Guid.NewGuid(),
            ["lesson7"] = Guid.NewGuid(),
            ["lesson8"] = Guid.NewGuid()
        };
    }

    public User[] SeedUsersData()
    {
        const string defaultPassword = "Pa$$w0rd";

        return new[]
        {
            CreateUser("user1", "John", "john@example.com", defaultPassword),
            CreateUser("user2", "Sarah", "sarah@example.com", defaultPassword),
            CreateUser("user3", "Bart", "bart@example.com", defaultPassword)
        };
    }

    public Course[] SeedCoursesData()
    {
        return new[]
        {
        CreateCourse(
            "course1",
            "Introduction to C# Programming",
            "A comprehensive course covering the basics of C# programming language, including syntax, data types, and object-oriented concepts.",
            "user1",
            DateTime.Parse("2025-01-28")
        ),
        CreateCourse(
            "course2",
            "Mastering Entity Framework Core",
            "An in-depth guide to working with Entity Framework Core, covering migrations, relationships, and performance optimization.",
            "user1",
            DateTime.Parse("2025-02-07")
        ),
        CreateCourse(
            "course3",
            "Building RESTful APIs with ASP.NET Core",
            "Learn how to design and develop robust REST APIs using ASP.NET Core, covering controllers, authentication, and best practices.",
            "user1",
            DateTime.Parse("2025-02-14")
        ),
        CreateCourse(
            "course4",
            "Unit Testing in .NET",
            "A hands-on course focused on writing effective unit tests in .NET using xUnit, Moq, and Test-Driven Development (TDD) principles.",
            "user1",
            DateTime.Parse("2025-02-21")
        )
        };
    }


    public Lesson[] SeedLessonsData()
    {
        return new[]
        {
        CreateLesson(
            "lesson1",
            "course1",
            "Getting Started with C#",
            "Installation and setup of development environment.",
            "https://www.youtube.com/watch?v=ravLFzIguCM"
        ),
        CreateLesson(
            "lesson2",
            "course1",
            "Variables and Data Types",
            "Understanding variables and different data types in C#.",
            "https://www.youtube.com/watch?v=_D-HCF3jZKk"
        ),
        CreateLesson(
            "lesson3",
            "course1",
            "Operators and Expressions",
            "Learn about arithmetic, logical, and comparison operators in C#.",
            "https://www.youtube.com/watch?v=WL7QEhdqh00"
        ),
        CreateLesson(
            "lesson4",
            "course2",
            "Introduction to Entity Framework Core",
            "Overview of EF Core and setting up the DbContext.",
            "https://www.youtube.com/watch?v=KcFWOMbGJ4M"
        ),
        CreateLesson(
            "lesson5",
            "course2",
            "Working with Migrations",
            "How to create and apply migrations in EF Core.",
            "https://www.youtube.com/watch?v=ZoKRFVBsm7E"
        ),
        CreateLesson(
            "lesson6",
            "course2",
            "Querying Data with LINQ",
            "Learn how to use LINQ queries in EF Core to fetch data.",
            "https://www.youtube.com/watch?v=DuozyaJQQ1U"
        ),
        CreateLesson(
            "lesson7",
            "course3",
            "Building RESTful APIs with ASP.NET Core",
            "Understanding controllers, routing, and API responses.",
            "https://www.youtube.com/watch?v=JiJeZOHx0ow"
        ),
        CreateLesson(
            "lesson8",
            "course3",
            "Authentication and Authorization",
            "Implementing authentication and role-based authorization in ASP.NET Core.",
            "https://www.youtube.com/watch?v=eUW2CYAT1Nk"
        )
        };
    }


    public Enrollment[] SeedEnrollmentsData()
    {
        return new[]
        {
            new Enrollment
            {
                Id = Guid.NewGuid(),
                UserId = _guids["user2"],
                CourseId = _guids["course1"],
                EnrolledAt = DateTime.Parse("2025-02-02"),
            }
        };
    }

    public Progress[] SeedProgressesData()
    {
        // Fixed the duplicate progress records issue
        return new[]
        {
            CreateProgress("user2", "lesson1", DateTime.Parse("2025-02-03")),
            CreateProgress("user2", "lesson2", DateTime.Parse("2025-02-05")),
            CreateProgress("user3", "lesson1", DateTime.Parse("2025-02-03"))
        };
    }

    // Helper methods to create entities with consistent data
    private User CreateUser(string userKey, string name, string email, string password)
    {
        return new User
        {
            Id = _guids[userKey],
            Name = name,
            Email = email,
            Password = PasswordHasher.HashPassword(password)
        };
    }

    private Course CreateCourse(string courseKey, string title, string description, string creatorKey, DateTime createdAt)
    {
        return new Course
        {
            Id = _guids[courseKey],
            Title = title,
            Description = description,
            CreatorId = _guids[creatorKey],
            CreatedAt = createdAt
        };
    }

    private Lesson CreateLesson(string lessonKey, string courseKey, string title, string description, string videoUrl)
    {
        return new Lesson
        {
            Id = _guids[lessonKey],
            CourseId = _guids[courseKey],
            Title = title,
            Description = description,
            VideoUrl = videoUrl
        };
    }

    private Progress CreateProgress(string userKey, string lessonKey, DateTime lastWatchedAt)
    {
        return new Progress
        {
            Id = Guid.NewGuid(),
            UserId = _guids[userKey],
            LessonId = _guids[lessonKey],
            LastWatchedAt = lastWatchedAt
        };
    }
}