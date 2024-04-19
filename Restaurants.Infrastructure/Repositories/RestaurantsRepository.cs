using Microsoft.EntityFrameworkCore;
using Restaurants.Core.Entities;
using Restaurants.Core.RepositoryContracts;
using Restaurants.Infrastructure.DatabaseContext;

namespace Restaurants.Infrastructure.Repositories;

public class RestaurantsRepository(RestaurantsDbContext dbContext) : IRestaurantsRepository
{
    public async Task<IEnumerable<Restaurant>> GetAllAsync()
    {
        return await dbContext.Restaurants.ToListAsync();
    }

    public async Task<Restaurant?> GetByIdAsync(int id)
    {
        return await dbContext.Restaurants.FirstOrDefaultAsync(x => x.Id == id);
    }
}