using Restaurants.Core.Domain.Constants;
using Restaurants.Core.Domain.Entities;

namespace Restaurants.Core.Domain.RepositoryContracts;

public interface IRestaurantsRepository
{
    public Task<IEnumerable<Restaurant>> GetAllAsync();

    public Task<(IEnumerable<Restaurant>, int)> GetAllMatchingAsync(
        string? searchPhrase,
        int pageNumber,
        int pageSize,
        string? sortBy,
        SortDirection sortDirection);

    public Task<Restaurant?> GetByIdAsync(int id);
    public Task<int> AddAsync(Restaurant entity);
    public Task<int> SaveChangesAsync();
    public Task DeleteAsync(Restaurant entity);
}