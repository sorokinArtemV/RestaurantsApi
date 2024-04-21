using FluentValidation;
using Restaurants.Application.Restaurants.DTO;

namespace Restaurants.Application.Restaurants.Validators;

public class RestaurantAddDtoValidator : AbstractValidator<RestaurantAddDto>
{
    public RestaurantAddDtoValidator()
    {
        RuleFor(dto => dto.Name)
            .Length(3, 100);

        RuleFor(dto => dto.Description)
            .NotEmpty().WithMessage("Description is required");
        
        RuleFor(dto => dto.Category)
            .NotEmpty().WithMessage("Category is required");
        
        RuleFor(dto => dto.ContactEmail)
            .EmailAddress().WithMessage("Invalid email address");
        
        RuleFor(dto => dto.ContactNumber)
            .Matches(@"^\d{10}$")
            .WithMessage("Phone number must be 10 digits");
        
        RuleFor(dto => dto.PostalCode)
            .Matches(@"^[0-9]{6}$").WithMessage("Postal code is required in 6 digits format");
    }
}