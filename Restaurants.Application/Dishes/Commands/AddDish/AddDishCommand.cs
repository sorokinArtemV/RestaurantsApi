using MediatR;

namespace Restaurants.Application.Dishes.Commands.AddDish;

public class AddDishCommand : IRequest
{
    public string Name { get; set; } = default!;
    public string Description { get; set; } = default!;
    public decimal Price { get; set; }

    public int? KiloCalories { get; set; }
}