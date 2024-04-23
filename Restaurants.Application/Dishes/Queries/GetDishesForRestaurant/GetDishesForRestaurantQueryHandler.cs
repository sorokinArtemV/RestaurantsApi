using AutoMapper;
using MediatR;
using Microsoft.Extensions.Logging;
using Restaurants.Application.Dishes.DTO;
using Restaurants.Core.Domain.Entities;
using Restaurants.Core.Domain.RepositoryContracts;
using Restaurants.Core.Exceptions;

namespace Restaurants.Application.Dishes.Queries.GetDishesForRestaurant;

public class GetDishesForRestaurantQueryHandler(
    ILogger<GetDishesForRestaurantQueryHandler> logger,
    IRestaurantsRepository restaurantsRepository,
    IMapper mapper
    )
    : IRequestHandler<GetDishesForRestaurantQuery, IEnumerable<DishDto>>
{
    public async Task<IEnumerable<DishDto>> Handle(
        GetDishesForRestaurantQuery request,
        CancellationToken cancellationToken)
    {
        logger.LogInformation("Getting dishes for restaurant: {@RestaurantId}", request.RestaurantId);
        
        var restaurant = await restaurantsRepository.GetByIdAsync(request.RestaurantId);
        
        if (restaurant is null) throw new NotFoundException(nameof(Restaurant), request.RestaurantId.ToString());

        var result = mapper.Map<IEnumerable<DishDto>>(restaurant.Dishes);
        
        return result;
    }
}