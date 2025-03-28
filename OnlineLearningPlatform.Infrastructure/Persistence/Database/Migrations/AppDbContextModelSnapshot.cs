﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using OnlineLearningPlatform.Infrastructure.Persistence.Database;

#nullable disable

namespace OnlineLearningPlatform.Infrastructure.Persistence.Database.Migrations
{
    [DbContext(typeof(AppDbContext))]
    partial class AppDbContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "8.0.14")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder);

            modelBuilder.Entity("OnlineLearningPlatform.Domain.Entities.Course", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<DateTime>("CreatedAt")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("datetime2")
                        .HasDefaultValueSql("GETUTCDATE()");

                    b.Property<Guid>("CreatorId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("Description")
                        .HasMaxLength(2000)
                        .HasColumnType("nvarchar(2000)");

                    b.Property<string>("Title")
                        .IsRequired()
                        .HasMaxLength(200)
                        .HasColumnType("nvarchar(200)");

                    b.HasKey("Id");

                    b.HasIndex("CreatorId");

                    b.ToTable("Courses");

                    b.HasData(
                        new
                        {
                            Id = new Guid("1458489f-40dd-44fc-9828-fda93a5a87e3"),
                            CreatedAt = new DateTime(2025, 1, 28, 0, 0, 0, 0, DateTimeKind.Unspecified),
                            CreatorId = new Guid("c282b7cb-b570-4893-b048-6e3b1f8f2c32"),
                            Description = "A comprehensive course covering the basics of C# programming language, including syntax, data types, and object-oriented concepts.",
                            Title = "Introduction to C# Programming"
                        },
                        new
                        {
                            Id = new Guid("14205b37-9561-405b-ab4f-5f75c43bd264"),
                            CreatedAt = new DateTime(2025, 2, 7, 0, 0, 0, 0, DateTimeKind.Unspecified),
                            CreatorId = new Guid("c282b7cb-b570-4893-b048-6e3b1f8f2c32"),
                            Description = "An in-depth guide to working with Entity Framework Core, covering migrations, relationships, and performance optimization.",
                            Title = "Mastering Entity Framework Core"
                        },
                        new
                        {
                            Id = new Guid("45b6441a-5673-4f4d-a094-41573f647978"),
                            CreatedAt = new DateTime(2025, 2, 14, 0, 0, 0, 0, DateTimeKind.Unspecified),
                            CreatorId = new Guid("c282b7cb-b570-4893-b048-6e3b1f8f2c32"),
                            Description = "Learn how to design and develop robust REST APIs using ASP.NET Core, covering controllers, authentication, and best practices.",
                            Title = "Building RESTful APIs with ASP.NET Core"
                        },
                        new
                        {
                            Id = new Guid("45add791-7831-40f2-8ce8-a37cff1343b8"),
                            CreatedAt = new DateTime(2025, 2, 21, 0, 0, 0, 0, DateTimeKind.Unspecified),
                            CreatorId = new Guid("c282b7cb-b570-4893-b048-6e3b1f8f2c32"),
                            Description = "A hands-on course focused on writing effective unit tests in .NET using xUnit, Moq, and Test-Driven Development (TDD) principles.",
                            Title = "Unit Testing in .NET"
                        });
                });

            modelBuilder.Entity("OnlineLearningPlatform.Domain.Entities.Enrollment", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<Guid>("CourseId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<DateTime>("EnrolledAt")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("datetime2")
                        .HasDefaultValueSql("GETUTCDATE()");

                    b.Property<Guid>("UserId")
                        .HasColumnType("uniqueidentifier");

                    b.HasKey("Id");

                    b.HasIndex("CourseId");

                    b.HasIndex("UserId", "CourseId")
                        .IsUnique();

                    b.ToTable("Enrollments");

                    b.HasData(
                        new
                        {
                            Id = new Guid("fc27143d-462d-4792-9be7-e784a57e5c72"),
                            CourseId = new Guid("1458489f-40dd-44fc-9828-fda93a5a87e3"),
                            EnrolledAt = new DateTime(2025, 2, 2, 0, 0, 0, 0, DateTimeKind.Unspecified),
                            UserId = new Guid("f950be07-6c2f-4b0a-b3ac-3dbaca31a363")
                        });
                });

            modelBuilder.Entity("OnlineLearningPlatform.Domain.Entities.Lesson", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<Guid>("CourseId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("Description")
                        .HasMaxLength(2000)
                        .HasColumnType("nvarchar(2000)");

                    b.Property<string>("Title")
                        .IsRequired()
                        .HasMaxLength(200)
                        .HasColumnType("nvarchar(200)");

                    b.Property<string>("VideoUrl")
                        .IsRequired()
                        .HasMaxLength(2048)
                        .HasColumnType("nvarchar(2048)");

                    b.HasKey("Id");

                    b.HasIndex("CourseId");

                    b.ToTable("Lessons");

                    b.HasData(
                        new
                        {
                            Id = new Guid("22280a2d-4a1e-4938-b85e-724b1f447293"),
                            CourseId = new Guid("1458489f-40dd-44fc-9828-fda93a5a87e3"),
                            Description = "Installation and setup of development environment.",
                            Title = "Getting Started with C#",
                            VideoUrl = "https://www.youtube.com/watch?v=ravLFzIguCM"
                        },
                        new
                        {
                            Id = new Guid("be35150e-713b-4c9d-a260-e64cbd71ad35"),
                            CourseId = new Guid("1458489f-40dd-44fc-9828-fda93a5a87e3"),
                            Description = "Understanding variables and different data types in C#.",
                            Title = "Variables and Data Types",
                            VideoUrl = "https://www.youtube.com/watch?v=_D-HCF3jZKk"
                        },
                        new
                        {
                            Id = new Guid("3ac02174-5e49-4ff0-b35d-238f7451b36c"),
                            CourseId = new Guid("1458489f-40dd-44fc-9828-fda93a5a87e3"),
                            Description = "Learn about arithmetic, logical, and comparison operators in C#.",
                            Title = "Operators and Expressions",
                            VideoUrl = "https://www.youtube.com/watch?v=WL7QEhdqh00"
                        },
                        new
                        {
                            Id = new Guid("05763b09-f89d-40db-8e4e-261d82557b66"),
                            CourseId = new Guid("14205b37-9561-405b-ab4f-5f75c43bd264"),
                            Description = "Overview of EF Core and setting up the DbContext.",
                            Title = "Introduction to Entity Framework Core",
                            VideoUrl = "https://www.youtube.com/watch?v=KcFWOMbGJ4M"
                        },
                        new
                        {
                            Id = new Guid("3f82b43b-c1f8-4931-91db-19eab8879c13"),
                            CourseId = new Guid("14205b37-9561-405b-ab4f-5f75c43bd264"),
                            Description = "How to create and apply migrations in EF Core.",
                            Title = "Working with Migrations",
                            VideoUrl = "https://www.youtube.com/watch?v=ZoKRFVBsm7E"
                        },
                        new
                        {
                            Id = new Guid("562936a2-861f-49fa-810c-d1582a572721"),
                            CourseId = new Guid("14205b37-9561-405b-ab4f-5f75c43bd264"),
                            Description = "Learn how to use LINQ queries in EF Core to fetch data.",
                            Title = "Querying Data with LINQ",
                            VideoUrl = "https://www.youtube.com/watch?v=DuozyaJQQ1U"
                        },
                        new
                        {
                            Id = new Guid("4e27abb7-bb13-4142-9775-5c1ac7ad57b3"),
                            CourseId = new Guid("45b6441a-5673-4f4d-a094-41573f647978"),
                            Description = "Understanding controllers, routing, and API responses.",
                            Title = "Building RESTful APIs with ASP.NET Core",
                            VideoUrl = "https://www.youtube.com/watch?v=JiJeZOHx0ow"
                        },
                        new
                        {
                            Id = new Guid("a06e0640-33a3-45e8-addf-39df94a71b98"),
                            CourseId = new Guid("45b6441a-5673-4f4d-a094-41573f647978"),
                            Description = "Implementing authentication and role-based authorization in ASP.NET Core.",
                            Title = "Authentication and Authorization",
                            VideoUrl = "https://www.youtube.com/watch?v=eUW2CYAT1Nk"
                        });
                });

            modelBuilder.Entity("OnlineLearningPlatform.Domain.Entities.Progress", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<DateTime>("LastWatchedAt")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("datetime2")
                        .HasDefaultValueSql("GETUTCDATE()");

                    b.Property<Guid>("LessonId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<Guid>("UserId")
                        .HasColumnType("uniqueidentifier");

                    b.HasKey("Id");

                    b.HasIndex("LessonId");

                    b.HasIndex("UserId");

                    b.ToTable("Progresses");

                    b.HasData(
                        new
                        {
                            Id = new Guid("63c1b13a-5d37-43a5-a0f1-2581ad4455d3"),
                            LastWatchedAt = new DateTime(2025, 2, 3, 0, 0, 0, 0, DateTimeKind.Unspecified),
                            LessonId = new Guid("22280a2d-4a1e-4938-b85e-724b1f447293"),
                            UserId = new Guid("f950be07-6c2f-4b0a-b3ac-3dbaca31a363")
                        },
                        new
                        {
                            Id = new Guid("64c6be67-0111-4249-961f-a693c745693a"),
                            LastWatchedAt = new DateTime(2025, 2, 5, 0, 0, 0, 0, DateTimeKind.Unspecified),
                            LessonId = new Guid("be35150e-713b-4c9d-a260-e64cbd71ad35"),
                            UserId = new Guid("f950be07-6c2f-4b0a-b3ac-3dbaca31a363")
                        },
                        new
                        {
                            Id = new Guid("6edf2a84-895f-4e84-9666-8c4f4aa76235"),
                            LastWatchedAt = new DateTime(2025, 2, 3, 0, 0, 0, 0, DateTimeKind.Unspecified),
                            LessonId = new Guid("22280a2d-4a1e-4938-b85e-724b1f447293"),
                            UserId = new Guid("a4339f40-95a7-4a42-9231-20d5d819bd9c")
                        });
                });

            modelBuilder.Entity("OnlineLearningPlatform.Domain.Entities.User", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("Email")
                        .IsRequired()
                        .HasMaxLength(256)
                        .HasColumnType("nvarchar(256)");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(100)
                        .HasColumnType("nvarchar(100)");

                    b.Property<string>("Password")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.HasIndex("Email")
                        .IsUnique();

                    b.ToTable("Users");

                    b.HasData(
                        new
                        {
                            Id = new Guid("c282b7cb-b570-4893-b048-6e3b1f8f2c32"),
                            Email = "john@example.com",
                            Name = "John",
                            Password = "ol7P+JWmNfXtag31+7OW58hUXbIXMvJm4eFqmepOqbesGfnKytuRXpMeGXyZnPIZDYLSBnrjfzWlkPu4MVPtrw=="
                        },
                        new
                        {
                            Id = new Guid("f950be07-6c2f-4b0a-b3ac-3dbaca31a363"),
                            Email = "sarah@example.com",
                            Name = "Sarah",
                            Password = "ol7P+JWmNfXtag31+7OW58hUXbIXMvJm4eFqmepOqbesGfnKytuRXpMeGXyZnPIZDYLSBnrjfzWlkPu4MVPtrw=="
                        },
                        new
                        {
                            Id = new Guid("a4339f40-95a7-4a42-9231-20d5d819bd9c"),
                            Email = "bart@example.com",
                            Name = "Bart",
                            Password = "ol7P+JWmNfXtag31+7OW58hUXbIXMvJm4eFqmepOqbesGfnKytuRXpMeGXyZnPIZDYLSBnrjfzWlkPu4MVPtrw=="
                        });
                });

            modelBuilder.Entity("OnlineLearningPlatform.Domain.Entities.Course", b =>
                {
                    b.HasOne("OnlineLearningPlatform.Domain.Entities.User", "Creator")
                        .WithMany("CreatedCourses")
                        .HasForeignKey("CreatorId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Creator");
                });

            modelBuilder.Entity("OnlineLearningPlatform.Domain.Entities.Enrollment", b =>
                {
                    b.HasOne("OnlineLearningPlatform.Domain.Entities.Course", "Course")
                        .WithMany("Enrollments")
                        .HasForeignKey("CourseId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("OnlineLearningPlatform.Domain.Entities.User", "User")
                        .WithMany("EnrolledCourses")
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.NoAction)
                        .IsRequired();

                    b.Navigation("Course");

                    b.Navigation("User");
                });

            modelBuilder.Entity("OnlineLearningPlatform.Domain.Entities.Lesson", b =>
                {
                    b.HasOne("OnlineLearningPlatform.Domain.Entities.Course", "Course")
                        .WithMany("Lessons")
                        .HasForeignKey("CourseId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Course");
                });

            modelBuilder.Entity("OnlineLearningPlatform.Domain.Entities.Progress", b =>
                {
                    b.HasOne("OnlineLearningPlatform.Domain.Entities.Lesson", "Lesson")
                        .WithMany("Progresses")
                        .HasForeignKey("LessonId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("OnlineLearningPlatform.Domain.Entities.User", "User")
                        .WithMany("Progresses")
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.NoAction)
                        .IsRequired();

                    b.Navigation("Lesson");

                    b.Navigation("User");
                });

            modelBuilder.Entity("OnlineLearningPlatform.Domain.Entities.Course", b =>
                {
                    b.Navigation("Enrollments");

                    b.Navigation("Lessons");
                });

            modelBuilder.Entity("OnlineLearningPlatform.Domain.Entities.Lesson", b =>
                {
                    b.Navigation("Progresses");
                });

            modelBuilder.Entity("OnlineLearningPlatform.Domain.Entities.User", b =>
                {
                    b.Navigation("CreatedCourses");

                    b.Navigation("EnrolledCourses");

                    b.Navigation("Progresses");
                });
#pragma warning restore 612, 618
        }
    }
}
