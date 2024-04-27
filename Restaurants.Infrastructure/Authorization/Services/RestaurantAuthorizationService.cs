using Microsoft.Extensions.Logging;
using Restaurants.Application.Users;
using Restaurants.Core.Domain.Constants;
using Restaurants.Core.Domain.Entities;

namespace Restaurants.Infrastructure.Authorization.Services;

public class RestaurantAuthorizationService(
    ILogger<RestaurantAuthorizationService> logger,
    IUserContext userContext
) : IRestaurantAuthorizationService
{
    public bool Authorize(Restaurant restaurant, ResourceOperation operation)
    {
        var user = userContext.GetCurrentUser();

        logger.LogInformation("Authorizing user: {Email} for operation: {Operation} on restaurant: {Name}",
            user.Email, operation, restaurant.Name);

        switch (operation)
        {
            case ResourceOperation.Create or ResourceOperation.Read:
                logger.LogInformation("Create/read operation is authorized");
                return true;

            case ResourceOperation.Delete when user.IsInRole(UserRoles.Admin):
                logger.LogInformation("Delete operation is authorized");
                return true;

            case ResourceOperation.Delete or ResourceOperation.Update when user.Id == restaurant.OwnerId:
                logger.LogInformation("Restaurant owner is authorized");
                break;
        }

        return false;
    }
}