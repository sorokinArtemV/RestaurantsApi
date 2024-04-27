using AutoMapper;
using MediatR;
using Microsoft.Extensions.Logging;
using Restaurants.Core.Domain.Constants;
using Restaurants.Core.Domain.Entities;
using Restaurants.Core.Domain.RepositoryContracts;
using Restaurants.Core.Domain.ServiceContracts;
using Restaurants.Core.Exceptions;

namespace Restaurants.Application.Dishes.Commands.AddDish;

public class AddDishCommandHandler(
    ILogger<AddDishCommandHandler> logger,
    IRestaurantsRepository restaurantsRepository,
    IDishesRepository dishesRepository,
    IMapper mapper,
    IRestaurantAuthorizationService authorizationService
) : IRequestHandler<AddDishCommand, int>
{
    public async Task<int> Handle(AddDishCommand request, CancellationToken cancellationToken)
    {
        logger.LogInformation("Creating new dish: {@DishRequest}", request);

        var restaurant = await restaurantsRepository.GetByIdAsync(request.RestaurantId);

        if (restaurant is null) throw new NotFoundException(nameof(Restaurant), request.RestaurantId.ToString());
        
        if (authorizationService.Authorize(restaurant, ResourceOperation.Update)) throw new ForbidException();
        
        var dish = mapper.Map<Dish>(request);
        
        
        return await dishesRepository.AddAsync(dish);
    }
}