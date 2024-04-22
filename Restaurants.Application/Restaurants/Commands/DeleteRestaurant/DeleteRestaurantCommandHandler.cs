using MediatR;
using Microsoft.Extensions.Logging;
using Restaurants.Core.Exceptions;
using Restaurants.Core.RepositoryContracts;

namespace Restaurants.Application.Restaurants.Commands.DeleteRestaurant;

public class DeleteRestaurantCommandHandler(
    ILogger<DeleteRestaurantCommandHandler> logger,
    IRestaurantsRepository repository
) : IRequestHandler<DeleteRestaurantCommand, bool>
{
    public async Task<bool> Handle(DeleteRestaurantCommand request, CancellationToken cancellationToken)
    {
        logger.LogInformation("Deleting restaurant: {Id}", request.Id);

        var restaurant = await repository.GetByIdAsync(request.Id);

        if (restaurant is null) throw new NotFoundException($"Restaurant with {request.Id} does not exist");

        await repository.DeleteAsync(restaurant);

        return true;
    }
}