using Restaurants.Application.DTO;
using Restaurants.Core.Entities;

namespace Restaurants.Application.ServiceContracts;

public interface IRestaurantsService
{
    Task<IEnumerable<RestaurantDto>> GetAllRestaurants();
    Task<RestaurantDto?> GetRestaurantById(int id);
}