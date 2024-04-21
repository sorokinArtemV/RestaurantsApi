using Restaurants.Application.Restaurants.DTO;

namespace Restaurants.Application.Restaurants.ServiceContracts;

public interface IRestaurantsService
{
    Task<IEnumerable<RestaurantDto>> GetAllRestaurants();
    Task<RestaurantDto?> GetRestaurantById(int id);
    Task<int> AddRestaurant(RestaurantAddDto dto);
}