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
                    { new Guid("a4339f40-95a7-4a42-9231-20d5d819bd9c"), "bart@example.com", "Bart", "ol7P+JWmNfXtag31+7OW58hUXbIXMvJm4eFqmepOqbesGfnKytuRXpMeGXyZnPIZDYLSBnrjfzWlkPu4MVPtrw==" },
                    { new Guid("c282b7cb-b570-4893-b048-6e3b1f8f2c32"), "john@example.com", "John", "ol7P+JWmNfXtag31+7OW58hUXbIXMvJm4eFqmepOqbesGfnKytuRXpMeGXyZnPIZDYLSBnrjfzWlkPu4MVPtrw==" },
                    { new Guid("f950be07-6c2f-4b0a-b3ac-3dbaca31a363"), "sarah@example.com", "Sarah", "ol7P+JWmNfXtag31+7OW58hUXbIXMvJm4eFqmepOqbesGfnKytuRXpMeGXyZnPIZDYLSBnrjfzWlkPu4MVPtrw==" }
                });

            migrationBuilder.InsertData(
                table: "Courses",
                columns: new[] { "Id", "CreatedAt", "CreatorId", "Description", "Title" },
                values: new object[,]
                {
                    { new Guid("14205b37-9561-405b-ab4f-5f75c43bd264"), new DateTime(2025, 2, 7, 0, 0, 0, 0, DateTimeKind.Unspecified), new Guid("c282b7cb-b570-4893-b048-6e3b1f8f2c32"), "An in-depth guide to working with Entity Framework Core, covering migrations, relationships, and performance optimization.", "Mastering Entity Framework Core" },
                    { new Guid("1458489f-40dd-44fc-9828-fda93a5a87e3"), new DateTime(2025, 1, 28, 0, 0, 0, 0, DateTimeKind.Unspecified), new Guid("c282b7cb-b570-4893-b048-6e3b1f8f2c32"), "A comprehensive course covering the basics of C# programming language, including syntax, data types, and object-oriented concepts.", "Introduction to C# Programming" },
                    { new Guid("45add791-7831-40f2-8ce8-a37cff1343b8"), new DateTime(2025, 2, 21, 0, 0, 0, 0, DateTimeKind.Unspecified), new Guid("c282b7cb-b570-4893-b048-6e3b1f8f2c32"), "A hands-on course focused on writing effective unit tests in .NET using xUnit, Moq, and Test-Driven Development (TDD) principles.", "Unit Testing in .NET" },
                    { new Guid("45b6441a-5673-4f4d-a094-41573f647978"), new DateTime(2025, 2, 14, 0, 0, 0, 0, DateTimeKind.Unspecified), new Guid("c282b7cb-b570-4893-b048-6e3b1f8f2c32"), "Learn how to design and develop robust REST APIs using ASP.NET Core, covering controllers, authentication, and best practices.", "Building RESTful APIs with ASP.NET Core" }
                });

            migrationBuilder.InsertData(
                table: "Enrollments",
                columns: new[] { "Id", "CourseId", "EnrolledAt", "UserId" },
                values: new object[] { new Guid("fc27143d-462d-4792-9be7-e784a57e5c72"), new Guid("1458489f-40dd-44fc-9828-fda93a5a87e3"), new DateTime(2025, 2, 2, 0, 0, 0, 0, DateTimeKind.Unspecified), new Guid("f950be07-6c2f-4b0a-b3ac-3dbaca31a363") });

            migrationBuilder.InsertData(
                table: "Lessons",
                columns: new[] { "Id", "CourseId", "Description", "Title", "VideoUrl" },
                values: new object[,]
                {
                    { new Guid("05763b09-f89d-40db-8e4e-261d82557b66"), new Guid("14205b37-9561-405b-ab4f-5f75c43bd264"), "Overview of EF Core and setting up the DbContext.", "Introduction to Entity Framework Core", "https://www.youtube.com/watch?v=KcFWOMbGJ4M" },
                    { new Guid("22280a2d-4a1e-4938-b85e-724b1f447293"), new Guid("1458489f-40dd-44fc-9828-fda93a5a87e3"), "Installation and setup of development environment.", "Getting Started with C#", "https://www.youtube.com/watch?v=ravLFzIguCM" },
                    { new Guid("3ac02174-5e49-4ff0-b35d-238f7451b36c"), new Guid("1458489f-40dd-44fc-9828-fda93a5a87e3"), "Learn about arithmetic, logical, and comparison operators in C#.", "Operators and Expressions", "https://www.youtube.com/watch?v=WL7QEhdqh00" },
                    { new Guid("3f82b43b-c1f8-4931-91db-19eab8879c13"), new Guid("14205b37-9561-405b-ab4f-5f75c43bd264"), "How to create and apply migrations in EF Core.", "Working with Migrations", "https://www.youtube.com/watch?v=ZoKRFVBsm7E" },
                    { new Guid("4e27abb7-bb13-4142-9775-5c1ac7ad57b3"), new Guid("45b6441a-5673-4f4d-a094-41573f647978"), "Understanding controllers, routing, and API responses.", "Building RESTful APIs with ASP.NET Core", "https://www.youtube.com/watch?v=JiJeZOHx0ow" },
                    { new Guid("562936a2-861f-49fa-810c-d1582a572721"), new Guid("14205b37-9561-405b-ab4f-5f75c43bd264"), "Learn how to use LINQ queries in EF Core to fetch data.", "Querying Data with LINQ", "https://www.youtube.com/watch?v=DuozyaJQQ1U" },
                    { new Guid("a06e0640-33a3-45e8-addf-39df94a71b98"), new Guid("45b6441a-5673-4f4d-a094-41573f647978"), "Implementing authentication and role-based authorization in ASP.NET Core.", "Authentication and Authorization", "https://www.youtube.com/watch?v=eUW2CYAT1Nk" },
                    { new Guid("be35150e-713b-4c9d-a260-e64cbd71ad35"), new Guid("1458489f-40dd-44fc-9828-fda93a5a87e3"), "Understanding variables and different data types in C#.", "Variables and Data Types", "https://www.youtube.com/watch?v=_D-HCF3jZKk" }
                });

            migrationBuilder.InsertData(
                table: "Progresses",
                columns: new[] { "Id", "LastWatchedAt", "LessonId", "UserId" },
                values: new object[,]
                {
                    { new Guid("63c1b13a-5d37-43a5-a0f1-2581ad4455d3"), new DateTime(2025, 2, 3, 0, 0, 0, 0, DateTimeKind.Unspecified), new Guid("22280a2d-4a1e-4938-b85e-724b1f447293"), new Guid("f950be07-6c2f-4b0a-b3ac-3dbaca31a363") },
                    { new Guid("64c6be67-0111-4249-961f-a693c745693a"), new DateTime(2025, 2, 5, 0, 0, 0, 0, DateTimeKind.Unspecified), new Guid("be35150e-713b-4c9d-a260-e64cbd71ad35"), new Guid("f950be07-6c2f-4b0a-b3ac-3dbaca31a363") },
                    { new Guid("6edf2a84-895f-4e84-9666-8c4f4aa76235"), new DateTime(2025, 2, 3, 0, 0, 0, 0, DateTimeKind.Unspecified), new Guid("22280a2d-4a1e-4938-b85e-724b1f447293"), new Guid("a4339f40-95a7-4a42-9231-20d5d819bd9c") }
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
