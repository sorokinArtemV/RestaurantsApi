using FluentValidation;
using Restaurants.Application.Restaurants.DTO;

namespace Restaurants.Application.Restaurants.Queries.GetAllRestaurants;

public class GetAllRestaurantsQueryValidator : AbstractValidator<GetAllRestaurantsQuery>
{
    private readonly int[] _validPageSizes = [5, 10];
    private readonly string[] _validSortByColumnNames = 
        [
            nameof(RestaurantDto.Name), 
            nameof(RestaurantDto.Category),
            nameof(RestaurantDto.Description)
        ];
  

    public GetAllRestaurantsQueryValidator()
    {
        RuleFor(r => r.PageNumber).GreaterThanOrEqualTo(1);
        
        RuleFor(r => r.PageSize)
            .Must(val => _validPageSizes.Contains(val))
            .WithMessage($"Page size must be in [{string.Join(",", _validPageSizes)}]");
        
        RuleFor(r => r.SortBy)
            .Must(val => _validSortByColumnNames.Contains(val))
            .When(r => !string.IsNullOrEmpty(r.SortBy))
            .WithMessage($"Sort by field is optional or must be in [{string.Join(",", _validSortByColumnNames)}]");
    }
}