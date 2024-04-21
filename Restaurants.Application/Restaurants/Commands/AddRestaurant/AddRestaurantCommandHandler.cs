using AutoMapper;
using MediatR;
using Microsoft.Extensions.Logging;
using Restaurants.Core.Entities;
using Restaurants.Core.RepositoryContracts;

namespace Restaurants.Application.Restaurants.Commands.AddRestaurant;

public class AddRestaurantCommandHandler(
    ILogger<AddRestaurantCommand> logger, 
    IMapper mapper, 
    IRestaurantsRepository repository) : IRequestHandler<AddRestaurantCommand, int>
{
    public async Task<int> Handle(AddRestaurantCommand request, CancellationToken cancellationToken)
    {
        logger.LogInformation("Adding new restaurant: {Name}", request.Name);

        var restaurant = mapper.Map<Restaurant>(request);
        var id = await repository.AddAsync(restaurant);

        return id;
    }
}