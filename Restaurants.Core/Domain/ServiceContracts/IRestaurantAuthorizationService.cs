using Restaurants.Core.Domain.Constants;
using Restaurants.Core.Domain.Entities;

namespace Restaurants.Core.Domain.ServiceContracts;

public interface IRestaurantAuthorizationService
{
    bool Authorize(Restaurant restaurant, ResourceOperation operation);
}