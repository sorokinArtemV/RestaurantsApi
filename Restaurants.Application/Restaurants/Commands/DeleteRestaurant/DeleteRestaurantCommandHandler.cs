using AutoMapper;
using MediatR;
using Microsoft.Extensions.Logging;
using Restaurants.Core.RepositoryContracts;

namespace Restaurants.Application.Restaurants.Commands.DeleteRestaurant;

public class DeleteRestaurantCommandHandler(
    ILogger<DeleteRestaurantCommandHandler> logger,
    IRestaurantsRepository repository,
    IMapper mapper
) : IRequestHandler<DeleteRestaurantCommand, bool>
{
    public async Task<bool> Handle(DeleteRestaurantCommand request, CancellationToken cancellationToken)
    {
        logger.LogInformation("Deleting restaurant: {Id}", request.Id);

        var restaurant = await repository.GetByIdAsync(request.Id);

        if (restaurant is null) return false;

        await repository.DeleteAsync(restaurant);
        
        return true;
    }
}