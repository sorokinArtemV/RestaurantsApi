using Microsoft.AspNetCore.Identity;
using Restaurants.Core.Domain.Entities;

namespace Restaurants.Core.Domain.Identity;

public class User : IdentityUser
{
    public DateOnly? DateOfBirth { get; set; }
    public string? Nationality { get; set; }
    
    public List<Restaurant> OwnedRestaurants { get; set; } = [];
}