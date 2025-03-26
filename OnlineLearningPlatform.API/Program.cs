using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using OnlineLearningPlatform.Application.Interfaces;
using OnlineLearningPlatform.Application.Services.Authentication;
using OnlineLearningPlatform.Infrastructure.Authentication;
using OnlineLearningPlatform.Infrastructure.Database;

var builder = WebApplication.CreateBuilder(args);

{
    builder.Services.Configure<JwtSettings>(builder.Configuration.GetSection("JwtSettings"));

    builder.Services.AddDbContext<AppDbContext>(options => options.UseSqlServer(builder.Configuration.GetConnectionString("DbConnection")));
    builder.Services.AddScoped<IAuthenticationService, AuthenticationService>();
    builder.Services.AddSingleton<ITokenGenerator, TokenGenerator>();



    builder.Services.AddControllers();
}

var app = builder.Build();

{
    app.UseHttpsRedirection();
    app.MapControllers();
}

app.Run();

