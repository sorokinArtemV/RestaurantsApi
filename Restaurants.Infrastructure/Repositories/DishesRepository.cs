using Restaurants.Core.Domain.Entities;
using Restaurants.Core.RepositoryContracts;
using Restaurants.Infrastructure.DatabaseContext;

namespace Restaurants.Infrastructure.Repositories;

public class DishesRepository(RestaurantsDbContext dbContext) : IDishesRepository
{
    public async Task<int> AddAsync(Dish entity)
    {
        await dbContext.Dishes.AddAsync(entity);
        await dbContext.SaveChangesAsync();
        
        return entity.Id;
    }
}