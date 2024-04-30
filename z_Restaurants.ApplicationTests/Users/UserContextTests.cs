using System.Security.Claims;
using FluentAssertions;
using Microsoft.AspNetCore.Http;
using Moq;
using Restaurants.Application.Users;
using Restaurants.Core.Domain.Constants;

namespace z_Restaurants.ApplicationTests.Users;

public class UserContextTests
{
    [Fact]
    public void GetCurrentUser_ShouldReturnCurrentUser_WhenUserExists()
    {
        //
        var dateOfBirth = new DateOnly(2000, 1, 1);

        var httpContextAccessorMock = new Mock<IHttpContextAccessor>();

        var claims = new List<Claim>
        {
            new(ClaimTypes.NameIdentifier, "1"),
            new(ClaimTypes.Email, "test@qa.com"),
            new(ClaimTypes.Role, "Admin"),
            new(ClaimTypes.Role, "User"),
            new("Nationality", "Japanese"),
            new("DateOfBirth", dateOfBirth.ToString("yyyy-MM-dd"))
        };

        var user = new ClaimsPrincipal(new ClaimsIdentity(claims, "test"));

        httpContextAccessorMock.Setup(x => x.HttpContext).Returns(new DefaultHttpContext { User = user });

        var userContext = new UserContext(httpContextAccessorMock.Object);

        //
        var currentUser = userContext.GetCurrentUser();

        //
        currentUser.Should().NotBeNull();
        currentUser!.Id.Should().Be("1");
        currentUser.Email.Should().Be("test@qa.com");
        currentUser.Roles.Should().ContainInOrder(UserRoles.Admin, UserRoles.User);
        currentUser.Nationality.Should().Be("Japanese");
        currentUser.DateOfBirth.Should().Be(dateOfBirth);
    }
}