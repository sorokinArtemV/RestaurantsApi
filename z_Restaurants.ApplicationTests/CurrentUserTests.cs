using FluentAssertions;
using Restaurants.Application.Users;
using Restaurants.Core.Domain.Constants;

namespace z_Restaurants.ApplicationTests;

public class CurrentUserTests
{
    [Theory]
    [InlineData(UserRoles.Admin)]
    [InlineData(UserRoles.User)]
    public void IsInRole_ShouldReturnTrue_WhenRoleMatches(string roleName)
    {
        var currentUser = new CurrentUser("1", "test@qa.com", [UserRoles.Admin, UserRoles.User], null, null);
        
        var isInRole = currentUser.IsInRole(roleName);
        
        isInRole.Should().BeTrue();
    }
    
    [Fact]
    public void IsInRole_ShouldReturnFalse_WhenRoleDoesNotMatch()
    {
        var currentUser = new CurrentUser("1", "test@qa.com", [UserRoles.Admin, UserRoles.User], null, null);
        
        var isInRole = currentUser.IsInRole(UserRoles.Owner);
        
        isInRole.Should().BeFalse();
    }
    
    [Fact]
    public void IsInRole_ShouldReturnFalse_WhenRoleCasingDoesNotMatch()
    {
        var currentUser = new CurrentUser("1", "test@qa.com", [UserRoles.Admin, UserRoles.User], null, null);
        
        var isInRole = currentUser.IsInRole(UserRoles.Admin.ToLower());
        
        isInRole.Should().BeFalse();
    }
    
}