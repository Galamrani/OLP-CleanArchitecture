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
                        onDelete: ReferentialAction.Restrict);
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
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
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
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                table: "Users",
                columns: new[] { "Id", "Email", "Name", "Password" },
                values: new object[,]
                {
                    { new Guid("066e8439-8764-4837-b429-310fb72b5384"), "bart@example.com", "Bart", "ol7P+JWmNfXtag31+7OW58hUXbIXMvJm4eFqmepOqbesGfnKytuRXpMeGXyZnPIZDYLSBnrjfzWlkPu4MVPtrw==" },
                    { new Guid("b59beaef-513f-4dd3-aef1-28fd0b94a42d"), "sarah@example.com", "Sarah", "ol7P+JWmNfXtag31+7OW58hUXbIXMvJm4eFqmepOqbesGfnKytuRXpMeGXyZnPIZDYLSBnrjfzWlkPu4MVPtrw==" },
                    { new Guid("db2daef7-1b42-4c9f-9c07-56aba27a27ed"), "john@example.com", "John", "ol7P+JWmNfXtag31+7OW58hUXbIXMvJm4eFqmepOqbesGfnKytuRXpMeGXyZnPIZDYLSBnrjfzWlkPu4MVPtrw==" }
                });

            migrationBuilder.InsertData(
                table: "Courses",
                columns: new[] { "Id", "CreatedAt", "CreatorId", "Description", "Title" },
                values: new object[,]
                {
                    { new Guid("1902028d-4628-4520-8f77-7246d68240e1"), new DateTime(2025, 2, 14, 0, 0, 0, 0, DateTimeKind.Unspecified), new Guid("db2daef7-1b42-4c9f-9c07-56aba27a27ed"), "Learn how to design and develop robust REST APIs using ASP.NET Core, covering controllers, authentication, and best practices.", "Building RESTful APIs with ASP.NET Core" },
                    { new Guid("2a34494a-143b-4951-a7cd-028e8587e921"), new DateTime(2025, 2, 7, 0, 0, 0, 0, DateTimeKind.Unspecified), new Guid("db2daef7-1b42-4c9f-9c07-56aba27a27ed"), "An in-depth guide to working with Entity Framework Core, covering migrations, relationships, and performance optimization.", "Mastering Entity Framework Core" },
                    { new Guid("6f5295bf-6ffc-44ed-96da-af5f75221525"), new DateTime(2025, 2, 21, 0, 0, 0, 0, DateTimeKind.Unspecified), new Guid("db2daef7-1b42-4c9f-9c07-56aba27a27ed"), "A hands-on course focused on writing effective unit tests in .NET using xUnit, Moq, and Test-Driven Development (TDD) principles.", "Unit Testing in .NET" },
                    { new Guid("ef0c94df-ff57-48e7-9073-311d66747dc4"), new DateTime(2025, 1, 28, 0, 0, 0, 0, DateTimeKind.Unspecified), new Guid("db2daef7-1b42-4c9f-9c07-56aba27a27ed"), "A comprehensive course covering the basics of C# programming language, including syntax, data types, and object-oriented concepts.", "Introduction to C# Programming" }
                });

            migrationBuilder.InsertData(
                table: "Enrollments",
                columns: new[] { "Id", "CourseId", "EnrolledAt", "UserId" },
                values: new object[] { new Guid("f9a10b04-ad23-4c3c-af56-b9122a925b7b"), new Guid("ef0c94df-ff57-48e7-9073-311d66747dc4"), new DateTime(2025, 2, 2, 0, 0, 0, 0, DateTimeKind.Unspecified), new Guid("b59beaef-513f-4dd3-aef1-28fd0b94a42d") });

            migrationBuilder.InsertData(
                table: "Lessons",
                columns: new[] { "Id", "CourseId", "Description", "Title", "VideoUrl" },
                values: new object[,]
                {
                    { new Guid("00c42a25-21d8-4935-8a6d-df4505c894b2"), new Guid("2a34494a-143b-4951-a7cd-028e8587e921"), "Learn how to use LINQ queries in EF Core to fetch data.", "Querying Data with LINQ", "https://www.youtube.com/watch?v=DuozyaJQQ1U" },
                    { new Guid("27e63227-4065-4420-bc7d-a12c07d9794d"), new Guid("ef0c94df-ff57-48e7-9073-311d66747dc4"), "Understanding variables and different data types in C#.", "Variables and Data Types", "https://www.youtube.com/watch?v=_D-HCF3jZKk" },
                    { new Guid("a9381301-217c-4632-929d-16117adf76b9"), new Guid("1902028d-4628-4520-8f77-7246d68240e1"), "Understanding controllers, routing, and API responses.", "Building RESTful APIs with ASP.NET Core", "https://www.youtube.com/watch?v=JiJeZOHx0ow" },
                    { new Guid("d93cfa8d-0fa8-48fe-a5df-9dc8401dec7d"), new Guid("2a34494a-143b-4951-a7cd-028e8587e921"), "Overview of EF Core and setting up the DbContext.", "Introduction to Entity Framework Core", "https://www.youtube.com/watch?v=KcFWOMbGJ4M" },
                    { new Guid("dd05067b-62b2-4820-af41-f954b4e24f35"), new Guid("1902028d-4628-4520-8f77-7246d68240e1"), "Implementing authentication and role-based authorization in ASP.NET Core.", "Authentication and Authorization", "https://www.youtube.com/watch?v=eUW2CYAT1Nk" },
                    { new Guid("eca16da4-4b50-47bb-b8c4-ec98ca0eb458"), new Guid("ef0c94df-ff57-48e7-9073-311d66747dc4"), "Learn about arithmetic, logical, and comparison operators in C#.", "Operators and Expressions", "https://www.youtube.com/watch?v=WL7QEhdqh00" },
                    { new Guid("f3bed488-0cab-4ad7-a961-1a055debdf0d"), new Guid("ef0c94df-ff57-48e7-9073-311d66747dc4"), "Installation and setup of development environment.", "Getting Started with C#", "https://www.youtube.com/watch?v=ravLFzIguCM" },
                    { new Guid("fd11e0f0-d972-4e73-8af4-c47ea2d25a82"), new Guid("2a34494a-143b-4951-a7cd-028e8587e921"), "How to create and apply migrations in EF Core.", "Working with Migrations", "https://www.youtube.com/watch?v=ZoKRFVBsm7E" }
                });

            migrationBuilder.InsertData(
                table: "Progresses",
                columns: new[] { "Id", "LastWatchedAt", "LessonId", "UserId" },
                values: new object[,]
                {
                    { new Guid("62a40bc4-a952-48fd-b50b-4971c8da23f1"), new DateTime(2025, 2, 5, 0, 0, 0, 0, DateTimeKind.Unspecified), new Guid("27e63227-4065-4420-bc7d-a12c07d9794d"), new Guid("b59beaef-513f-4dd3-aef1-28fd0b94a42d") },
                    { new Guid("c5289e43-0485-45ea-a1b0-64ae9ae1d32d"), new DateTime(2025, 2, 3, 0, 0, 0, 0, DateTimeKind.Unspecified), new Guid("f3bed488-0cab-4ad7-a961-1a055debdf0d"), new Guid("066e8439-8764-4837-b429-310fb72b5384") },
                    { new Guid("f464c138-8ca6-4e4e-8af8-d9162de73cba"), new DateTime(2025, 2, 3, 0, 0, 0, 0, DateTimeKind.Unspecified), new Guid("f3bed488-0cab-4ad7-a961-1a055debdf0d"), new Guid("b59beaef-513f-4dd3-aef1-28fd0b94a42d") }
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
