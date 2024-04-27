using Restaurants.Core.Domain.Entities;

namespace Restaurants.Infrastructure.Authorization.Services;

public interface IRestaurantAuthorizationService
{
    bool Authorize(Restaurant restaurant, ResourceOperation operation);
}