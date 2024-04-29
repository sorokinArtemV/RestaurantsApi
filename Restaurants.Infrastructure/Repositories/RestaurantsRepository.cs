using Microsoft.EntityFrameworkCore;
using Restaurants.Core.Domain.Entities;
using Restaurants.Core.Domain.RepositoryContracts;
using Restaurants.Infrastructure.DatabaseContext;

namespace Restaurants.Infrastructure.Repositories;

public class RestaurantsRepository(RestaurantsDbContext dbContext) : IRestaurantsRepository
{
    public async Task<IEnumerable<Restaurant>> GetAllAsync()
    {
        return await dbContext.Restaurants.ToListAsync();
    }

    public async Task<(IEnumerable<Restaurant>, int)> GetAllMatchingAsync(string? searchPhrase, int pageNumber, int pageSize)
    {
        var searchPhraseLower = searchPhrase?.ToLower();

        var baseQuery = dbContext.Restaurants
            .Where(r => searchPhraseLower == null || r.Name.ToLower().Contains(searchPhraseLower) ||
                        r.Description.ToLower().Contains(searchPhraseLower));
        
        var totalCount = await baseQuery.CountAsync();
        
        var restaurants = await baseQuery
            .Where(r => searchPhraseLower == null || r.Name.ToLower().Contains(searchPhraseLower) ||
                        r.Description.ToLower().Contains(searchPhraseLower))
            .Skip((pageNumber - 1) * pageSize)
            .Take(pageSize)
            .ToListAsync();


        return (restaurants, totalCount);
    }

    public async Task<Restaurant?> GetByIdAsync(int id)
    {
        return await dbContext.Restaurants
            .Include(r => r.Dishes)
            .FirstOrDefaultAsync(x => x.Id == id);
    }

    public async Task<int> AddAsync(Restaurant entity)
    {
        await dbContext.Restaurants.AddAsync(entity);
        await dbContext.SaveChangesAsync();

        return entity.Id;
    }

    public async Task<int> SaveChangesAsync()
    {
        return await dbContext.SaveChangesAsync();
    }

    public async Task DeleteAsync(Restaurant entity)
    {
        dbContext.Restaurants.Remove(entity);
        await dbContext.SaveChangesAsync();
    }
}