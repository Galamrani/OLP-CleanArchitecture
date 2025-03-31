using OnlineLearningPlatform.Application.Common;
using OnlineLearningPlatform.Domain.Entities;

namespace OnlineLearningPlatform.Infrastructure.Persistence.Database;
public class SeedData
{
    private readonly Dictionary<string, Guid> _guids;

    public SeedData()
    {
        _guids = new Dictionary<string, Guid>
        {
            // Users
            ["user1"] = new Guid("11111111-1111-1111-1111-111111111111"),
            ["user2"] = new Guid("22222222-2222-2222-2222-222222222222"),
            ["user3"] = new Guid("33333333-3333-3333-3333-333333333333"),

            // Courses
            ["course1"] = new Guid("a1a1a1a1-a1a1-a1a1-a1a1-a1a1a1a1a1a1"),
            ["course2"] = new Guid("a2a2a2a2-a2a2-a2a2-a2a2-a2a2a2a2a2a2"),
            ["course3"] = new Guid("a3a3a3a3-a3a3-a3a3-a3a3-a3a3a3a3a3a3"),
            ["course4"] = new Guid("a4a4a4a4-a4a4-a4a4-a4a4-a4a4a4a4a4a4"),

            // Lessons
            ["lesson1"] = new Guid("b1b1b1b1-b1b1-b1b1-b1b1-b1b1b1b1b1b1"),
            ["lesson2"] = new Guid("b2b2b2b2-b2b2-b2b2-b2b2-b2b2b2b2b2b2"),
            ["lesson3"] = new Guid("b3b3b3b3-b3b3-b3b3-b3b3-b3b3b3b3b3b3"),
            ["lesson4"] = new Guid("b4b4b4b4-b4b4-b4b4-b4b4-b4b4b4b4b4b4"),
            ["lesson5"] = new Guid("b5b5b5b5-b5b5-b5b5-b5b5-b5b5b5b5b5b5"),
            ["lesson6"] = new Guid("b6b6b6b6-b6b6-b6b6-b6b6-b6b6b6b6b6b6"),
            ["lesson7"] = new Guid("b7b7b7b7-b7b7-b7b7-b7b7-b7b7b7b7b7b7"),
            ["lesson8"] = new Guid("b8b8b8b8-b8b8-b8b8-b8b8-b8b8b8b8b8b8")
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