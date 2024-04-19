using Restaurants.Core.Entities;

namespace Restaurants.Application.ServiceContracts;

public interface IRestaurantsService
{
    Task<IEnumerable<Restaurant>> GetAllRestaurants();
    Task<Restaurant?> GetRestaurantById(int id);
}