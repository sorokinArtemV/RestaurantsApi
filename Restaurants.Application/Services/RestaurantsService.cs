using AutoMapper;
using Microsoft.Extensions.Logging;
using Restaurants.Application.DTO;
using Restaurants.Application.DTO.Restaurant;
using Restaurants.Application.ServiceContracts;
using Restaurants.Core.RepositoryContracts;

namespace Restaurants.Application.Services;

public class RestaurantsService(
    IRestaurantsRepository repository,
    ILogger<RestaurantsService> logger,
    IMapper mapper
) : IRestaurantsService
{
    public async Task<IEnumerable<RestaurantDto>> GetAllRestaurants()
    {
        logger.LogInformation("Getting all restaurants");
        var restaurants = await repository.GetAllAsync();

        var restaurantsDto = mapper.Map<IEnumerable<RestaurantDto>>(restaurants);

        return restaurantsDto;
    }

    public async Task<RestaurantDto?> GetRestaurantById(int id)
    {
        logger.LogInformation("Getting restaurant by id: {Id}", id);

        var restaurant = await repository.GetByIdAsync(id);

        var restaurantDto = mapper.Map<RestaurantDto>(restaurant);

        return restaurantDto;
    }
}