using Restaurants.Core.Domain.Entities;

namespace Restaurants.Core.Domain.RepositoryContracts;

public interface IRestaurantsRepository
{
    Task<IEnumerable<Restaurant>> GetAllAsync();
    Task<Restaurant?> GetByIdAsync(int id);
    Task<int> AddAsync(Restaurant entity);
    Task<int> SaveChangesAsync();
    Task DeleteAsync(Restaurant entity);
}