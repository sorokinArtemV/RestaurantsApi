using Restaurants.Core.Domain.Entities;

namespace Restaurants.Core.Domain.RepositoryContracts;

public interface IDishesRepository
{
    Task<int> AddAsync(Dish entity);
    Task DeleteAsync(IEnumerable<Dish> entities);
}