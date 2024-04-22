using AutoMapper;
using MediatR;
using Microsoft.Extensions.Logging;
using Restaurants.Core.Domain.Entities;
using Restaurants.Core.Exceptions;
using Restaurants.Core.RepositoryContracts;

namespace Restaurants.Application.Dishes.Commands.AddDish;

public class AddDishCommandHandler(
    ILogger<AddDishCommandHandler> logger,
    IRestaurantsRepository restaurantsRepository,
    IDishesRepository dishesRepository,
    IMapper mapper
) : IRequestHandler<AddDishCommand>
{
    public async Task Handle(AddDishCommand request, CancellationToken cancellationToken)
    {
        logger.LogInformation("Creating new dish: {@DishRequest}", request);

        var restaurant = await restaurantsRepository.GetByIdAsync(request.RestaurantId);

        if (restaurant is null) throw new NotFoundException(nameof(Restaurant), request.RestaurantId.ToString());
        
        var dish = mapper.Map<Dish>(request);
        
        await dishesRepository.AddAsync(dish);
    }
}