using FluentValidation;
using FluentValidation.AspNetCore;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using OnlineLearningPlatform.API.Extensions;
using OnlineLearningPlatform.API.Middlewares;
using OnlineLearningPlatform.Application.Common;
using OnlineLearningPlatform.Application.Configurations;
using OnlineLearningPlatform.Application.Interfaces;
using OnlineLearningPlatform.Application.Services.Authentication;
using OnlineLearningPlatform.Application.Services.CourseManagement;
using OnlineLearningPlatform.Application.Services.LessonManagement;
using OnlineLearningPlatform.Application.Services.UserManagement;
using OnlineLearningPlatform.Application.Validators;
using OnlineLearningPlatform.Infrastructure.Persistence.Database;
using OnlineLearningPlatform.Infrastructure.Services;
using OnlineLearningPlatform.Infrastructure.Services.CourseManagement;
using OnlineLearningPlatform.Infrastructure.Services.LessonManagement;
using OnlineLearningPlatform.Infrastructure.Services.Persistence;
using OnlineLearningPlatform.Infrastructure.Services.UserManagement;

var builder = WebApplication.CreateBuilder(args);

{
    // Configure Serilog for logging
    builder.AddSerilogLogging();

    // Binds the "JwtSettings" section from apps-settings to the JwtSettings class and registers it to the DI as singleton
    builder.Services.Configure<JwtSettings>(builder.Configuration.GetSection("JwtSettings"));

    // Adds JWT Bearer authentication and configures token validation parameters
    // Sets up how the app should authenticate and validate JWT tokens
    builder.Services.AddJwtAuthentication(builder.Configuration);

    builder.Services.AddCors(options =>
    {
        options.AddPolicy("LocalDevPolicy", policy =>
            policy.AllowAnyOrigin()
                  .AllowAnyHeader()
                  .AllowAnyMethod());
    });

    // Infrastructure
    builder.Services.AddDbContext<AppDbContext>(options =>
    {
        options.UseSqlServer(builder.Configuration.GetConnectionString("DbConnection"),
        sqlOptions =>
        {
            // Uses split queries to improve performance
            sqlOptions.UseQuerySplittingBehavior(QuerySplittingBehavior.SplitQuery);
        });
    });

    builder.Services.AddScoped<IBaseDataService, BaseDataService>();
    builder.Services.AddScoped<ICourseDataService, CourseDataService>();
    builder.Services.AddScoped<ILessonDataService, LessonDataService>();
    builder.Services.AddScoped<IUserDataService, UserDataService>();
    builder.Services.AddScoped<IEnrollmentDataService, EnrollmentDataService>();

    // Application
    builder.Services.AddAutoMapper(typeof(MappingProfiles).Assembly);
    builder.Services.AddScoped<IAuthenticationService, AuthenticationService>();
    builder.Services.AddScoped<ICourseService, CourseService>();
    builder.Services.AddScoped<ILessonService, LessonService>();
    builder.Services.AddScoped<IUserService, UserService>();

    builder.Services.AddFluentValidationAutoValidation();
    builder.Services.AddValidatorsFromAssembly(typeof(CourseDtoValidator).Assembly);

    builder.Services.AddControllers();
}

var app = builder.Build();

{
    app.UseCors("LocalDevPolicy");
    app.UseMiddleware<ExceptionHandlingMiddleware>();
    app.UseMiddleware<LoggingMiddleware>();
    // Checks if the request has a valid authentication token (e.g., JWT).
    app.UseAuthentication();
    // After a user is authenticated, it decides whether they’re allowed to access the endpoint.
    app.UseAuthorization();

    app.MapControllers();
}

app.Run();

