using Microsoft.Extensions.Logging;
using Restaurants.Application.ServiceContracts;
using Restaurants.Core.Entities;
using Restaurants.Core.RepositoryContracts;

namespace Restaurants.Application.Services;

public class RestaurantsService(
    IRestaurantsRepository repository, ILogger<RestaurantsService> logger) : IRestaurantsService
{
    public async Task<IEnumerable<Restaurant>> GetAllRestaurants()
    {
        logger.LogInformation("Getting all restaurants");
        var restaurants = await repository.GetAllAsync();
        
        return restaurants;
    }

    public async Task<Restaurant?> GetRestaurantById(int id)
    {
        logger.LogInformation("Getting restaurant by id: {Id}", id);
        
        return await repository.GetByIdAsync(id);
    }
}