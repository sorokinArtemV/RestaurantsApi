using Restaurants.Core.Entities;

namespace Restaurants.Core.RepositoryContracts;

public interface IRestaurantsRepository
{
    Task<IEnumerable<Restaurant>> GetAllAsync();
    Task<Restaurant?> GetByIdAsync(int id);

    Task<int> AddAsync(Restaurant entity);

    // Task<int> UpdateAsync(Restaurant entity);
    Task DeleteAsync(Restaurant entity);
}