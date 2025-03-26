using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace OnlineLearningPlatform.Infrastructure.Database.Migrations
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
                    { new Guid("00dbcaf4-95f1-425a-857e-101730edea70"), "sarah@example.com", "Sarah", "ol7P+JWmNfXtag31+7OW58hUXbIXMvJm4eFqmepOqbesGfnKytuRXpMeGXyZnPIZDYLSBnrjfzWlkPu4MVPtrw==" },
                    { new Guid("36edcfd5-ba02-4144-9850-3a2b390c53e4"), "john@example.com", "John", "ol7P+JWmNfXtag31+7OW58hUXbIXMvJm4eFqmepOqbesGfnKytuRXpMeGXyZnPIZDYLSBnrjfzWlkPu4MVPtrw==" },
                    { new Guid("555f610b-1169-4bfc-ae8b-3af123f7b480"), "bart@example.com", "Bart", "ol7P+JWmNfXtag31+7OW58hUXbIXMvJm4eFqmepOqbesGfnKytuRXpMeGXyZnPIZDYLSBnrjfzWlkPu4MVPtrw==" }
                });

            migrationBuilder.InsertData(
                table: "Courses",
                columns: new[] { "Id", "CreatedAt", "CreatorId", "Description", "Title" },
                values: new object[,]
                {
                    { new Guid("015a803a-57d8-4746-8f49-b89721371342"), new DateTime(2025, 2, 14, 0, 0, 0, 0, DateTimeKind.Unspecified), new Guid("36edcfd5-ba02-4144-9850-3a2b390c53e4"), "Learn how to design and develop robust REST APIs using ASP.NET Core, covering controllers, authentication, and best practices.", "Building RESTful APIs with ASP.NET Core" },
                    { new Guid("2020690e-f61d-4093-8832-8b1097665fa0"), new DateTime(2025, 1, 28, 0, 0, 0, 0, DateTimeKind.Unspecified), new Guid("36edcfd5-ba02-4144-9850-3a2b390c53e4"), "A comprehensive course covering the basics of C# programming language, including syntax, data types, and object-oriented concepts.", "Introduction to C# Programming" },
                    { new Guid("222a68ba-ef6d-4efd-b4b2-671eea2b89a5"), new DateTime(2025, 2, 7, 0, 0, 0, 0, DateTimeKind.Unspecified), new Guid("36edcfd5-ba02-4144-9850-3a2b390c53e4"), "An in-depth guide to working with Entity Framework Core, covering migrations, relationships, and performance optimization.", "Mastering Entity Framework Core" },
                    { new Guid("c4fad3c9-2447-4801-bfd1-c205624feaed"), new DateTime(2025, 2, 21, 0, 0, 0, 0, DateTimeKind.Unspecified), new Guid("36edcfd5-ba02-4144-9850-3a2b390c53e4"), "A hands-on course focused on writing effective unit tests in .NET using xUnit, Moq, and Test-Driven Development (TDD) principles.", "Unit Testing in .NET" }
                });

            migrationBuilder.InsertData(
                table: "Enrollments",
                columns: new[] { "Id", "CourseId", "EnrolledAt", "UserId" },
                values: new object[] { new Guid("1e2edfb3-b191-4eb0-a283-29d25084d395"), new Guid("2020690e-f61d-4093-8832-8b1097665fa0"), new DateTime(2025, 2, 2, 0, 0, 0, 0, DateTimeKind.Unspecified), new Guid("00dbcaf4-95f1-425a-857e-101730edea70") });

            migrationBuilder.InsertData(
                table: "Lessons",
                columns: new[] { "Id", "CourseId", "Description", "Title", "VideoUrl" },
                values: new object[,]
                {
                    { new Guid("1cd7665c-2550-4c2c-b5b2-e1b12103ac2a"), new Guid("222a68ba-ef6d-4efd-b4b2-671eea2b89a5"), "Learn how to use LINQ queries in EF Core to fetch data.", "Querying Data with LINQ", "https://www.youtube.com/watch?v=DuozyaJQQ1U" },
                    { new Guid("62099240-e2d7-498a-bf61-53b4ddb2a8df"), new Guid("2020690e-f61d-4093-8832-8b1097665fa0"), "Learn about arithmetic, logical, and comparison operators in C#.", "Operators and Expressions", "https://www.youtube.com/watch?v=WL7QEhdqh00" },
                    { new Guid("6511c33a-ac65-429d-8a30-ebe77496bdd5"), new Guid("222a68ba-ef6d-4efd-b4b2-671eea2b89a5"), "How to create and apply migrations in EF Core.", "Working with Migrations", "https://www.youtube.com/watch?v=ZoKRFVBsm7E" },
                    { new Guid("b2ad1223-276e-4456-8417-aea0099ccb0b"), new Guid("222a68ba-ef6d-4efd-b4b2-671eea2b89a5"), "Overview of EF Core and setting up the DbContext.", "Introduction to Entity Framework Core", "https://www.youtube.com/watch?v=KcFWOMbGJ4M" },
                    { new Guid("c0b01fde-4295-4569-a1d3-64940486af83"), new Guid("015a803a-57d8-4746-8f49-b89721371342"), "Implementing authentication and role-based authorization in ASP.NET Core.", "Authentication and Authorization", "https://www.youtube.com/watch?v=eUW2CYAT1Nk" },
                    { new Guid("cb0c4c0b-6138-4209-9a1b-e616dd0a4eaa"), new Guid("2020690e-f61d-4093-8832-8b1097665fa0"), "Installation and setup of development environment.", "Getting Started with C#", "https://www.youtube.com/watch?v=ravLFzIguCM" },
                    { new Guid("df89891c-59ca-48a2-8a06-1e52823bf6c3"), new Guid("2020690e-f61d-4093-8832-8b1097665fa0"), "Understanding variables and different data types in C#.", "Variables and Data Types", "https://www.youtube.com/watch?v=_D-HCF3jZKk" },
                    { new Guid("f46f18df-66ca-43b4-9f76-97b915d765c6"), new Guid("015a803a-57d8-4746-8f49-b89721371342"), "Understanding controllers, routing, and API responses.", "Building RESTful APIs with ASP.NET Core", "https://www.youtube.com/watch?v=JiJeZOHx0ow" }
                });

            migrationBuilder.InsertData(
                table: "Progresses",
                columns: new[] { "Id", "LastWatchedAt", "LessonId", "UserId" },
                values: new object[,]
                {
                    { new Guid("4420877e-a7e1-4228-9e3c-b7bace465b03"), new DateTime(2025, 2, 5, 0, 0, 0, 0, DateTimeKind.Unspecified), new Guid("df89891c-59ca-48a2-8a06-1e52823bf6c3"), new Guid("00dbcaf4-95f1-425a-857e-101730edea70") },
                    { new Guid("83faecf7-f987-433a-b37e-7c179707e899"), new DateTime(2025, 2, 3, 0, 0, 0, 0, DateTimeKind.Unspecified), new Guid("cb0c4c0b-6138-4209-9a1b-e616dd0a4eaa"), new Guid("555f610b-1169-4bfc-ae8b-3af123f7b480") },
                    { new Guid("f008e742-bdba-4f31-bd35-2ce8c7c0d90b"), new DateTime(2025, 2, 3, 0, 0, 0, 0, DateTimeKind.Unspecified), new Guid("cb0c4c0b-6138-4209-9a1b-e616dd0a4eaa"), new Guid("00dbcaf4-95f1-425a-857e-101730edea70") }
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
