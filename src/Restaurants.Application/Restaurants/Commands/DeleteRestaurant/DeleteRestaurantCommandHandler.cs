using MediatR;
using Microsoft.Extensions.Logging;
using Restaurants.Core.Domain.Constants;
using Restaurants.Core.Domain.Entities;
using Restaurants.Core.Domain.RepositoryContracts;
using Restaurants.Core.Domain.ServiceContracts;
using Restaurants.Core.Exceptions;

namespace Restaurants.Application.Restaurants.Commands.DeleteRestaurant;

public class DeleteRestaurantCommandHandler(
    ILogger<DeleteRestaurantCommandHandler> logger,
    IRestaurantsRepository repository,
    IRestaurantAuthorizationService authorizationService
) : IRequestHandler<DeleteRestaurantCommand>
{
    public async Task Handle(DeleteRestaurantCommand request, CancellationToken cancellationToken)
    {
        logger.LogInformation("Deleting restaurant: {Id}", request.Id);

        var restaurant = await repository.GetByIdAsync(request.Id);

        if (restaurant is null) throw new NotFoundException(nameof(Restaurant), request.Id.ToString());

        if (!authorizationService.Authorize(restaurant, ResourceOperation.Delete)) throw new ForbidException();
        
        await repository.DeleteAsync(restaurant);
    }
}