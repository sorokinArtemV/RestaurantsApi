using MediatR;
using Microsoft.Extensions.Logging;
using Restaurants.Core.Domain.Constants;
using Restaurants.Core.Domain.Entities;
using Restaurants.Core.Domain.RepositoryContracts;
using Restaurants.Core.Domain.ServiceContracts;
using Restaurants.Core.Exceptions;

namespace Restaurants.Application.Dishes.Commands.DeleteDishes;

public class DeleteDishesCommandHandler(
    ILogger<DeleteDishesCommandHandler> logger,
    IRestaurantsRepository restaurantsRepository,
    IRestaurantAuthorizationService authorizationService,
    IDishesRepository dishesRepository) : IRequestHandler<DeleteDishesCommand>
{
    public async Task Handle(DeleteDishesCommand request, CancellationToken cancellationToken)
    {
        logger.LogWarning("Deleting dishes for restaurant: {@RestaurantId}", request.RestaurantId);

        var restaurant = await restaurantsRepository.GetByIdAsync(request.RestaurantId);

        if (restaurant is null) throw new NotFoundException(nameof(Restaurant), request.RestaurantId.ToString());

        if (!authorizationService.Authorize(restaurant, ResourceOperation.Update)) throw new ForbidException();
        
        await dishesRepository.DeleteAsync(restaurant.Dishes);
    }
}