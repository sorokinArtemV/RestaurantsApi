using AutoMapper;
using MediatR;
using Microsoft.Extensions.Logging;
using Restaurants.Application.Dishes.DTO;
using Restaurants.Core.Domain.Entities;
using Restaurants.Core.Exceptions;
using Restaurants.Core.RepositoryContracts;

namespace Restaurants.Application.Dishes.Queries.GetDishByIdForRestaurant;

public class GetDishByIdForRestaurantQueryHandler(
    ILogger<GetDishByIdForRestaurantQueryHandler> logger,
    IRestaurantsRepository restaurantsRepository,
    IMapper mapper
)
    : IRequestHandler<GetDishByIdForRestaurantQuery, DishDto>
{
    public async Task<DishDto> Handle(GetDishByIdForRestaurantQuery request, CancellationToken cancellationToken)
    {
        logger.LogInformation("Getting dish: {@DishId}, for restaurant: {@RestaurantId}",
            request.DishId, request.RestaurantId);

        var restaurant = await restaurantsRepository.GetByIdAsync(request.RestaurantId);

        if (restaurant is null) throw new NotFoundException(nameof(Restaurant), request.RestaurantId.ToString());

        var dish = restaurant.Dishes?.FirstOrDefault(x => x.Id == request.DishId);
        
        if (dish is null) throw new NotFoundException(nameof(Dish), request.DishId.ToString());
        
        var result = mapper.Map<DishDto>(dish);

        return result;
    }
}