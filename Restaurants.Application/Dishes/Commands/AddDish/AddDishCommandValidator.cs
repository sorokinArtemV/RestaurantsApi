using FluentValidation;

namespace Restaurants.Application.Dishes.Commands.AddDish;

public class AddDishCommandValidator : AbstractValidator<AddDishCommand>
{
    public AddDishCommandValidator()
    {
        RuleFor(dish => dish.Price)
            .GreaterThanOrEqualTo(0)
            .WithMessage("Price must be greater than or equal to zero");
        
        RuleFor(dish => dish.KiloCalories)
            .GreaterThanOrEqualTo(0)
            .WithMessage("KiloCalories must be greater than or equal to zero");
    }
}