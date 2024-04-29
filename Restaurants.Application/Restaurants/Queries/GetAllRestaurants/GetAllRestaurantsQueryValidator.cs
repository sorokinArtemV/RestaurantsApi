using FluentValidation;

namespace Restaurants.Application.Restaurants.Queries.GetAllRestaurants;

public class GetAllRestaurantsQueryValidator : AbstractValidator<GetAllRestaurantsQuery>
{
    private int[] _validPageSizes = [5, 10];

    public GetAllRestaurantsQueryValidator()
    {
        RuleFor(r => r.PageNumber).GreaterThanOrEqualTo(1);
        RuleFor(r => r.PageSize)
            .Must(val => _validPageSizes.Contains(val))
            .WithMessage($"Page size must be in [{string.Join(",", _validPageSizes)}]");
    }
}