using FluentValidation;

namespace Restaurants.Application.Restaurants.Commands.AddRestaurant;

public class AddRestaurantCommandValidator : AbstractValidator<AddRestaurantCommand>
{
    private readonly List<string> _validCategories = 
        ["Italian", "Indian", "Mexican", "Japanese", "French", "American", "Fast food"];
        
    public AddRestaurantCommandValidator()
    {
        RuleFor(dto => dto.Name)
            .Length(3, 100);

        RuleFor(dto => dto.Description)
            .NotEmpty()
            .WithMessage("Description is required");
        
        RuleFor(dto => dto.Category)
            .Must(_validCategories.Contains)
            .WithMessage("Invalid category. Choose from: " + string.Join(", ", _validCategories));

        
        RuleFor(dto => dto.ContactEmail)
            .EmailAddress().WithMessage("Invalid email address");
        
        RuleFor(dto => dto.ContactNumber)
            .Matches(@"^\d{10}$")
            .WithMessage("Phone number must be 10 digits");
        
        RuleFor(dto => dto.PostalCode)
            .Matches(@"^[0-9]{6}$").WithMessage("Postal code is required in 6 digits format");
    }
}