using Restaurants.Core.Entities;

namespace Restaurants.Core.RepositoryContracts;

public interface IRestaurantsRepository
{
    Task<IEnumerable<Restaurant>> GetAllAsync();
    Task<Restaurant?> GetByIdAsync(int id);
}