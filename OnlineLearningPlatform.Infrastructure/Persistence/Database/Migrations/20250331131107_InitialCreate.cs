using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace OnlineLearningPlatform.Infrastructure.Persistence.Database.Migrations
{
    /// <inheritdoc />
    public partial class InitialCreate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Users",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    Email = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: false),
                    Password = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Users", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Courses",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    CreatorId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Title = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: false),
                    Description = table.Column<string>(type: "nvarchar(2000)", maxLength: 2000, nullable: true),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false, defaultValueSql: "GETUTCDATE()")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Courses", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Courses_Users_CreatorId",
                        column: x => x.CreatorId,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Enrollments",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    UserId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    CourseId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    EnrolledAt = table.Column<DateTime>(type: "datetime2", nullable: false, defaultValueSql: "GETUTCDATE()")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Enrollments", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Enrollments_Courses_CourseId",
                        column: x => x.CourseId,
                        principalTable: "Courses",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Enrollments_Users_UserId",
                        column: x => x.UserId,
                        principalTable: "Users",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "Lessons",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    CourseId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Title = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: false),
                    Description = table.Column<string>(type: "nvarchar(2000)", maxLength: 2000, nullable: true),
                    VideoUrl = table.Column<string>(type: "nvarchar(2048)", maxLength: 2048, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Lessons", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Lessons_Courses_CourseId",
                        column: x => x.CourseId,
                        principalTable: "Courses",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Progresses",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    UserId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    LessonId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    LastWatchedAt = table.Column<DateTime>(type: "datetime2", nullable: false, defaultValueSql: "GETUTCDATE()")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Progresses", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Progresses_Lessons_LessonId",
                        column: x => x.LessonId,
                        principalTable: "Lessons",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Progresses_Users_UserId",
                        column: x => x.UserId,
                        principalTable: "Users",
                        principalColumn: "Id");
                });

            migrationBuilder.InsertData(
                table: "Users",
                columns: new[] { "Id", "Email", "Name", "Password" },
                values: new object[,]
                {
                    { new Guid("11111111-1111-1111-1111-111111111111"), "john@example.com", "John", "ol7P+JWmNfXtag31+7OW58hUXbIXMvJm4eFqmepOqbesGfnKytuRXpMeGXyZnPIZDYLSBnrjfzWlkPu4MVPtrw==" },
                    { new Guid("22222222-2222-2222-2222-222222222222"), "sarah@example.com", "Sarah", "ol7P+JWmNfXtag31+7OW58hUXbIXMvJm4eFqmepOqbesGfnKytuRXpMeGXyZnPIZDYLSBnrjfzWlkPu4MVPtrw==" },
                    { new Guid("33333333-3333-3333-3333-333333333333"), "bart@example.com", "Bart", "ol7P+JWmNfXtag31+7OW58hUXbIXMvJm4eFqmepOqbesGfnKytuRXpMeGXyZnPIZDYLSBnrjfzWlkPu4MVPtrw==" }
                });

            migrationBuilder.InsertData(
                table: "Courses",
                columns: new[] { "Id", "CreatedAt", "CreatorId", "Description", "Title" },
                values: new object[,]
                {
                    { new Guid("a1a1a1a1-a1a1-a1a1-a1a1-a1a1a1a1a1a1"), new DateTime(2025, 1, 28, 0, 0, 0, 0, DateTimeKind.Unspecified), new Guid("11111111-1111-1111-1111-111111111111"), "A comprehensive course covering the basics of C# programming language, including syntax, data types, and object-oriented concepts.", "Introduction to C# Programming" },
                    { new Guid("a2a2a2a2-a2a2-a2a2-a2a2-a2a2a2a2a2a2"), new DateTime(2025, 2, 7, 0, 0, 0, 0, DateTimeKind.Unspecified), new Guid("11111111-1111-1111-1111-111111111111"), "An in-depth guide to working with Entity Framework Core, covering migrations, relationships, and performance optimization.", "Mastering Entity Framework Core" },
                    { new Guid("a3a3a3a3-a3a3-a3a3-a3a3-a3a3a3a3a3a3"), new DateTime(2025, 2, 14, 0, 0, 0, 0, DateTimeKind.Unspecified), new Guid("11111111-1111-1111-1111-111111111111"), "Learn how to design and develop robust REST APIs using ASP.NET Core, covering controllers, authentication, and best practices.", "Building RESTful APIs with ASP.NET Core" },
                    { new Guid("a4a4a4a4-a4a4-a4a4-a4a4-a4a4a4a4a4a4"), new DateTime(2025, 2, 21, 0, 0, 0, 0, DateTimeKind.Unspecified), new Guid("11111111-1111-1111-1111-111111111111"), "A hands-on course focused on writing effective unit tests in .NET using xUnit, Moq, and Test-Driven Development (TDD) principles.", "Unit Testing in .NET" }
                });

            migrationBuilder.InsertData(
                table: "Enrollments",
                columns: new[] { "Id", "CourseId", "EnrolledAt", "UserId" },
                values: new object[] { new Guid("de441372-a171-42b7-9e29-878631ccf682"), new Guid("a1a1a1a1-a1a1-a1a1-a1a1-a1a1a1a1a1a1"), new DateTime(2025, 2, 2, 0, 0, 0, 0, DateTimeKind.Unspecified), new Guid("22222222-2222-2222-2222-222222222222") });

            migrationBuilder.InsertData(
                table: "Lessons",
                columns: new[] { "Id", "CourseId", "Description", "Title", "VideoUrl" },
                values: new object[,]
                {
                    { new Guid("b1b1b1b1-b1b1-b1b1-b1b1-b1b1b1b1b1b1"), new Guid("a1a1a1a1-a1a1-a1a1-a1a1-a1a1a1a1a1a1"), "Installation and setup of development environment.", "Getting Started with C#", "https://www.youtube.com/watch?v=ravLFzIguCM" },
                    { new Guid("b2b2b2b2-b2b2-b2b2-b2b2-b2b2b2b2b2b2"), new Guid("a1a1a1a1-a1a1-a1a1-a1a1-a1a1a1a1a1a1"), "Understanding variables and different data types in C#.", "Variables and Data Types", "https://www.youtube.com/watch?v=_D-HCF3jZKk" },
                    { new Guid("b3b3b3b3-b3b3-b3b3-b3b3-b3b3b3b3b3b3"), new Guid("a1a1a1a1-a1a1-a1a1-a1a1-a1a1a1a1a1a1"), "Learn about arithmetic, logical, and comparison operators in C#.", "Operators and Expressions", "https://www.youtube.com/watch?v=WL7QEhdqh00" },
                    { new Guid("b4b4b4b4-b4b4-b4b4-b4b4-b4b4b4b4b4b4"), new Guid("a2a2a2a2-a2a2-a2a2-a2a2-a2a2a2a2a2a2"), "Overview of EF Core and setting up the DbContext.", "Introduction to Entity Framework Core", "https://www.youtube.com/watch?v=KcFWOMbGJ4M" },
                    { new Guid("b5b5b5b5-b5b5-b5b5-b5b5-b5b5b5b5b5b5"), new Guid("a2a2a2a2-a2a2-a2a2-a2a2-a2a2a2a2a2a2"), "How to create and apply migrations in EF Core.", "Working with Migrations", "https://www.youtube.com/watch?v=ZoKRFVBsm7E" },
                    { new Guid("b6b6b6b6-b6b6-b6b6-b6b6-b6b6b6b6b6b6"), new Guid("a2a2a2a2-a2a2-a2a2-a2a2-a2a2a2a2a2a2"), "Learn how to use LINQ queries in EF Core to fetch data.", "Querying Data with LINQ", "https://www.youtube.com/watch?v=DuozyaJQQ1U" },
                    { new Guid("b7b7b7b7-b7b7-b7b7-b7b7-b7b7b7b7b7b7"), new Guid("a3a3a3a3-a3a3-a3a3-a3a3-a3a3a3a3a3a3"), "Understanding controllers, routing, and API responses.", "Building RESTful APIs with ASP.NET Core", "https://www.youtube.com/watch?v=JiJeZOHx0ow" },
                    { new Guid("b8b8b8b8-b8b8-b8b8-b8b8-b8b8b8b8b8b8"), new Guid("a3a3a3a3-a3a3-a3a3-a3a3-a3a3a3a3a3a3"), "Implementing authentication and role-based authorization in ASP.NET Core.", "Authentication and Authorization", "https://www.youtube.com/watch?v=eUW2CYAT1Nk" }
                });

            migrationBuilder.InsertData(
                table: "Progresses",
                columns: new[] { "Id", "LastWatchedAt", "LessonId", "UserId" },
                values: new object[,]
                {
                    { new Guid("38048e7e-daa9-485a-96ed-3057c08eeac4"), new DateTime(2025, 2, 3, 0, 0, 0, 0, DateTimeKind.Unspecified), new Guid("b1b1b1b1-b1b1-b1b1-b1b1-b1b1b1b1b1b1"), new Guid("22222222-2222-2222-2222-222222222222") },
                    { new Guid("b929d162-2d4e-489d-9f89-793946e5a4aa"), new DateTime(2025, 2, 3, 0, 0, 0, 0, DateTimeKind.Unspecified), new Guid("b1b1b1b1-b1b1-b1b1-b1b1-b1b1b1b1b1b1"), new Guid("33333333-3333-3333-3333-333333333333") },
                    { new Guid("db354dfd-12c6-448d-a523-5b7245d03674"), new DateTime(2025, 2, 5, 0, 0, 0, 0, DateTimeKind.Unspecified), new Guid("b2b2b2b2-b2b2-b2b2-b2b2-b2b2b2b2b2b2"), new Guid("22222222-2222-2222-2222-222222222222") }
                });

            migrationBuilder.CreateIndex(
                name: "IX_Courses_CreatorId",
                table: "Courses",
                column: "CreatorId");

            migrationBuilder.CreateIndex(
                name: "IX_Enrollments_CourseId",
                table: "Enrollments",
                column: "CourseId");

            migrationBuilder.CreateIndex(
                name: "IX_Enrollments_UserId_CourseId",
                table: "Enrollments",
                columns: new[] { "UserId", "CourseId" },
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Lessons_CourseId",
                table: "Lessons",
                column: "CourseId");

            migrationBuilder.CreateIndex(
                name: "IX_Progresses_LessonId",
                table: "Progresses",
                column: "LessonId");

            migrationBuilder.CreateIndex(
                name: "IX_Progresses_UserId",
                table: "Progresses",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_Users_Email",
                table: "Users",
                column: "Email",
                unique: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Enrollments");

            migrationBuilder.DropTable(
                name: "Progresses");

            migrationBuilder.DropTable(
                name: "Lessons");

            migrationBuilder.DropTable(
                name: "Courses");

            migrationBuilder.DropTable(
                name: "Users");
        }
    }
}
