using Restaurants.Application.DTO.RestaurantDtos;

namespace Restaurants.Application.ServiceContracts;

public interface IRestaurantsService
{
    Task<IEnumerable<RestaurantDto>> GetAllRestaurants();
    Task<RestaurantDto?> GetRestaurantById(int id);
    Task<int> AddRestaurant(RestaurantAddDto dto);
}