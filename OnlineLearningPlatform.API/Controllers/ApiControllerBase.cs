using System.Security.Claims;
using Microsoft.AspNetCore.Mvc;

namespace OnlineLearningPlatform.API.Controllers;

[ApiController]
public class ApiControllerBase : ControllerBase
{
    protected Guid GetUserId(HttpContext context)
    {
        var userId = context.User.FindFirstValue(ClaimTypes.NameIdentifier);
        return Guid.TryParse(userId, out var parsedUserId) ? parsedUserId : Guid.Empty;
    }
}