using System.Security.Claims;
using Microsoft.AspNetCore.Mvc;

namespace OnlineLearningPlatform.API.Controllers;

[ApiController]
[Route("api/[controller]")]
public class ApiControllerBase : ControllerBase
{
    protected Guid GetUserId(HttpContext context)
    {
        var userId = context.User.FindFirstValue(ClaimTypes.NameIdentifier);
        System.Console.WriteLine(userId);
        return Guid.TryParse(userId, out var parsedUserId) ? parsedUserId : Guid.Empty;
    }
}