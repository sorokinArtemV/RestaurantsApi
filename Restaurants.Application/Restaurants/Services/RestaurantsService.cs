using AutoMapper;
using Microsoft.Extensions.Logging;
using Restaurants.Application.Restaurants.DTO;
using Restaurants.Application.Restaurants.ServiceContracts;
using Restaurants.Core.RepositoryContracts;

namespace Restaurants.Application.Restaurants.Services;

public class RestaurantsService(
    IRestaurantsRepository repository,
    ILogger<RestaurantsService> logger,
    IMapper mapper
) : IRestaurantsService
{
    public async Task<RestaurantDto?> GetRestaurantById(int id)
    {
        logger.LogInformation("Getting restaurant by id: {Id}", id);

        var restaurant = await repository.GetByIdAsync(id);

        var restaurantDto = mapper.Map<RestaurantDto>(restaurant);

        return restaurantDto;
    }
    
}