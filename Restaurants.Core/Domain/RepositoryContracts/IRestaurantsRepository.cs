using Restaurants.Core.Domain.Entities;

namespace Restaurants.Core.Domain.RepositoryContracts;

public interface IRestaurantsRepository
{
    public Task<IEnumerable<Restaurant>> GetAllAsync();
    public Task<IEnumerable<Restaurant>> GetAllMatchingAsync(string? searchPhrase);
    public Task<Restaurant?> GetByIdAsync(int id);
    public Task<int> AddAsync(Restaurant entity);
    public Task<int> SaveChangesAsync();
    public Task DeleteAsync(Restaurant entity);
}