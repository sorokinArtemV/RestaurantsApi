using Microsoft.EntityFrameworkCore;
using Restaurants.Core.Entities;

namespace Restaurants.Infrastructure.DatabaseContext;

public class RestaurantsDbContext : DbContext
{
    public RestaurantsDbContext(DbContextOptions options) : base(options)
    {
    }

    public RestaurantsDbContext()
    {
    }

    public DbSet<Restaurant> Restaurants { get; set; }
    public DbSet<Dish> Dishes { get; set; }
    
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        modelBuilder.Entity<Restaurant>().OwnsOne(r => r.Address);
        
        modelBuilder.Entity<Restaurant>()
            .HasMany(r => r.Dishes)
            .WithOne()
            .HasForeignKey(d => d.RestaurantId);
    }
}