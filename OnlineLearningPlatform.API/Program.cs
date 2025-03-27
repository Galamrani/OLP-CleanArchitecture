using System.Text;
using System.Text.Json.Serialization;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using OnlineLearningPlatform.Application.Common;
using OnlineLearningPlatform.Application.Interfaces;
using OnlineLearningPlatform.Application.Services.Authentication;
using OnlineLearningPlatform.Application.Services.CourseManagement;
using OnlineLearningPlatform.Application.Services.LessonManagement;
using OnlineLearningPlatform.Infrastructure.Persistence.Database;
using OnlineLearningPlatform.Infrastructure.Services.Authentication;
using OnlineLearningPlatform.Infrastructure.Services.CourseManagement;
using OnlineLearningPlatform.Infrastructure.Services.LessonManagement;
using OnlineLearningPlatform.Infrastructure.Services.Persistence;

var builder = WebApplication.CreateBuilder(args);

{
    builder.Services.AddCors(options =>
    {
        options.AddPolicy("DevelopmentCorsPolicy", policy =>
            policy.AllowAnyOrigin()
                  .AllowAnyHeader()
                  .AllowAnyMethod());
    });

    builder.Services.Configure<JwtSettings>(builder.Configuration.GetSection("JwtSettings"));
    builder.Services.AddAuthentication(options =>
    {
        options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
        options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
        options.DefaultScheme = JwtBearerDefaults.AuthenticationScheme;
    }).AddJwtBearer(options =>
    {
        var jwtSettings = builder.Configuration.GetSection("JwtSettings").Get<JwtSettings>();
        options.TokenValidationParameters = new TokenValidationParameters
        {
            ValidateIssuer = true,
            ValidateAudience = true,
            ValidateLifetime = true,
            ValidateIssuerSigningKey = true,
            ValidIssuer = jwtSettings?.Issuer,
            ValidAudience = jwtSettings?.Audience,
            IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(jwtSettings?.JwtKey!))
        };
    });


    // Infrastructure
    builder.Services.AddDbContext<AppDbContext>(options => options.UseSqlServer(builder.Configuration.GetConnectionString("DbConnection")));
    builder.Services.AddScoped<IUnitOfWork, UnitOfWork>();
    builder.Services.AddScoped<ICourseDAO, CourseDAO>();
    builder.Services.AddScoped<ILessonDAO, LessonDAO>();
    builder.Services.AddScoped<IAuthenticationDAO, AuthenticationDAO>();
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
    app.UseCors("DevelopmentCorsPolicy");

    app.UseAuthentication();
    app.UseAuthorization();

    app.MapControllers();
}

app.Run();

