using AutoMapper;
using Microsoft.EntityFrameworkCore;
using OnlineLearningPlatform.Application.Common;
using OnlineLearningPlatform.Application.Interfaces;
using OnlineLearningPlatform.Application.Services.Authentication;
using OnlineLearningPlatform.Application.Services.CourseManagement;
using OnlineLearningPlatform.Application.Services.LessonManagement;
using OnlineLearningPlatform.Infrastructure.Database;
using OnlineLearningPlatform.Infrastructure.Services.Authentication;
using OnlineLearningPlatform.Infrastructure.Services.CourseManagement;
using OnlineLearningPlatform.Infrastructure.Services.LessonManagement;

var builder = WebApplication.CreateBuilder(args);

{
    builder.Services.Configure<JwtSettings>(builder.Configuration.GetSection("JwtSettings"));

    // Infrastructure
    builder.Services.AddDbContext<AppDbContext>(options => options.UseSqlServer(builder.Configuration.GetConnectionString("DbConnection")));
    builder.Services.AddScoped<ICourseDAO, CourseDAO>();
    builder.Services.AddScoped<ILessonDAO, LessonDAO>();
    builder.Services.AddSingleton<ITokenGenerator, TokenGenerator>();

    // Application
    builder.Services.AddAutoMapper(typeof(MappingProfiles).Assembly);
    builder.Services.AddScoped<IAuthenticationService, AuthenticationService>();
    builder.Services.AddScoped<ICourseService, CourseService>();
    builder.Services.AddScoped<ILessonService, LessonService>();
    builder.Services.AddControllers();
}

var app = builder.Build();

{
    app.UseHttpsRedirection();
    app.MapControllers();
}

app.Run();

