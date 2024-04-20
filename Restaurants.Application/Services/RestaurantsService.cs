using Microsoft.Extensions.Logging;
using Restaurants.Application.DTO;
using Restaurants.Application.ServiceContracts;
using Restaurants.Core.Entities;
using Restaurants.Core.RepositoryContracts;

namespace Restaurants.Application.Services;

public class RestaurantsService(
    IRestaurantsRepository repository, ILogger<RestaurantsService> logger) : IRestaurantsService
{
    public async Task<IEnumerable<RestaurantDto>> GetAllRestaurants()
    {
        logger.LogInformation("Getting all restaurants");
        var restaurants = await repository.GetAllAsync();
        
        return restaurants;
    }

    public async Task<RestaurantDto?> GetRestaurantById(int id)
    {
        logger.LogInformation("Getting restaurant by id: {Id}", id);
        
        return await repository.GetByIdAsync(id);
    }
}