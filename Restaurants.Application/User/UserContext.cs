using System.Security.Claims;
using Microsoft.AspNetCore.Http;

namespace Restaurants.Application.User;

public class UserContext(IHttpContextAccessor contextAccessor)
{
    public CurrentUser? GetCurrentUser()
    {
        var user = contextAccessor.HttpContext?.User;

        if (user == null) throw new InvalidOperationException("User context is not present");

        if (user.Identity is not { IsAuthenticated: true }) return null;

        var userId = user.FindFirst(x => x.Type == ClaimTypes.NameIdentifier)!.Value;
        var email = user.FindFirst(x => x.Type == ClaimTypes.Email)!.Value;
        var roles = user.Claims.Where(x => x.Type == ClaimTypes.Role).Select(x => x.Value);
        
        return new CurrentUser(userId, email, roles);
    }
}