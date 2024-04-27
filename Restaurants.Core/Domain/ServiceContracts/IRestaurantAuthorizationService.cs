using Restaurants.Core.Domain.Entities;
using Restaurants.Infrastructure.Authorization;

namespace Restaurants.Core.Domain.ServiceContracts;

public interface IRestaurantAuthorizationService
{
    bool Authorize(Restaurant restaurant, ResourceOperation operation);
}