using System.Security.Claims;
using Microsoft.AspNetCore.Http;

namespace Restaurants.Application.Users;

public interface IUserContext
{
    CurrentUser? GetCurrentUser();
}

public class UserContext(IHttpContextAccessor contextAccessor) : IUserContext
{
    public CurrentUser? GetCurrentUser()
    {
        var user = contextAccessor.HttpContext?.User;

        if (user == null) throw new InvalidOperationException("Users context is not present");

        if (user.Identity is not { IsAuthenticated: true }) return null;

        var userId = user.FindFirst(x => x.Type == ClaimTypes.NameIdentifier)!.Value;
        var email = user.FindFirst(x => x.Type == ClaimTypes.Email)!.Value;
        var roles = user.Claims.Where(x => x.Type == ClaimTypes.Role).Select(x => x.Value);
        var nationality = user.FindFirst(x => x.Type == "Nationality")?.Value;

        var dateOfBirthString = user.FindFirst(x => x.Type == "DateOfBirth")?.Value;
        var dateOfBirth = dateOfBirthString == null
            ? (DateOnly?)null
            : DateOnly.ParseExact(dateOfBirthString, "yyyy MMMM dd");

        return new CurrentUser(userId, email, roles, nationality, dateOfBirth);
    }
}