using Microsoft.AspNetCore.Identity;
using Restaurants.Core.Domain.Constants;
using Restaurants.Core.Domain.Entities;
using Restaurants.Infrastructure.DatabaseContext;

namespace Restaurants.Infrastructure.Seeders;

public class RestaurantSeeder(RestaurantsDbContext dbContext) : IRestaurantSeeder
{
    public async Task Seed()
    {
        if (await dbContext.Database.CanConnectAsync())
            if (!dbContext.Restaurants.Any())
            {
                var restaurants = GetRestaurants();
                dbContext.Restaurants.AddRange(restaurants);
                await dbContext.SaveChangesAsync();
            }

        if (!dbContext.Roles.Any())
        {
            var roles = GetRoles();
            dbContext.Roles.AddRange(roles);
            await dbContext.SaveChangesAsync();
        }
    }

    private IEnumerable<IdentityRole> GetRoles()
    {
        List<IdentityRole> roles =
        [
            new IdentityRole(UserRoles.User),
            new IdentityRole(UserRoles.Owner),
            new IdentityRole(UserRoles.Admin)
        ];
        
        return roles;
    }

    private IEnumerable<Restaurant> GetRestaurants()
    {
        List<Restaurant> restaurants =
        [
            new Restaurant
            {
                Name = "McDonalds",
                Category = "Fast food",
                Description =
                    "McDonald's Corporation (McDonald's) is one of the world's " +
                    "largest and most recognized fast-food chains, known for its hamburgers, french " +
                    "fries, and name-brand sandwiches.",
                ContactEmail = "mcdonalds@mc.com",
                HasDelivery = true,
                Dishes =
                [
                    new Dish
                    {
                        Name = "Big Mac",
                        Description =
                            "Two all-beef patties, special sauce, lettuce, cheese, pickles, " +
                            "onions on a sesame seed bun.",
                        Price = 5.99m
                    },

                    new Dish
                    {
                        Name = "Big Tasty",
                        Description =
                            "Two all-beef patties, special sauce, lettuce, cheese, pickles, onions on a sesame seed bun.",
                        Price = 6.99m
                    },
                    new Dish
                    {
                        Name = "Oreo McFlurry",
                        Description = "Vanilla soft serve with Oreo cookie pieces mixed in.",
                        Price = 2.99m
                    }
                ]
            },
            new Restaurant
            {
                Name = "Wendy's",
                Category = "Fast food",
                Description =
                    "Wendy's is an international fast-food chain known for its square hamburgers, " +
                    "sea salt fries, and Frosty, a form of soft serve ice cream mixed with starches.",
                ContactEmail = "contact@wendys.com",
                HasDelivery = true,
                Dishes =
                [
                    new Dish
                    {
                        Name = "Baconator",
                        Description =
                            "Half a pound of beef, American cheese, six pieces of crispy bacon, " +
                            "ketchup, and mayo on a bun.",
                        Price = 6.99m
                    },
                    new Dish
                    {
                        Name = "Spicy Chicken Sandwich",
                        Description =
                            "A juicy chicken breast marinated and breaded in a unique, fiery blend of " +
                            "peppers and spices, with lettuce, tomato, and mayo.",
                        Price = 5.49m
                    },
                    new Dish
                    {
                        Name = "Frosty",
                        Description = "Thick and rich milkshake available in chocolate and vanilla flavors.",
                        Price = 1.99m
                    }
                ]
            }
        ];

        return restaurants;
    }
}