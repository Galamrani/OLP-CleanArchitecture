using FluentValidation;
using FluentValidation.AspNetCore;
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
using OnlineLearningPlatform.Infrastructure.Services.Authentication;
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

    // TODO: understand what it does
    // Adds JWT Bearer authentication and configures token validation parameters
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

    builder.Services.AddScoped<IUnitOfWork, UnitOfWork>();
    builder.Services.AddScoped<ICourseDAO, CourseDAO>();
    builder.Services.AddScoped<ILessonDAO, LessonDAO>();
    builder.Services.AddScoped<IUserDAO, UserDAO>();
    builder.Services.AddTransient<ITokenGenerator, TokenGenerator>();

    // Application
    builder.Services.AddAutoMapper(typeof(MappingProfiles).Assembly);
    builder.Services.AddScoped<IAuthenticationService, AuthenticationService>();
    builder.Services.AddScoped<ICourseService, CourseService>();
    builder.Services.AddScoped<ILessonService, LessonService>();
    builder.Services.AddScoped<IUserService, UserService>();

    builder.Services.AddFluentValidationAutoValidation();
    builder.Services.AddValidatorsFromAssemblyContaining<CourseDtoValidator>(); // Scans the assembly containing CourseDtoValidator for all AbstractValidator<T> implementations

    builder.Services.AddControllers();
}

var app = builder.Build();

{
    app.UseCors("LocalDevPolicy");
    app.UseMiddleware<ExceptionHandlingMiddleware>();
    app.UseMiddleware<LoggingMiddleware>();
    app.UseAuthentication();
    app.UseAuthorization();

    app.MapControllers();
}

app.Run();

