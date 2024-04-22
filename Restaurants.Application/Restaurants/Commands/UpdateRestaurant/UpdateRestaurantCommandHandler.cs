using AutoMapper;
using MediatR;
using Microsoft.Extensions.Logging;
using Restaurants.Core.Exceptions;
using Restaurants.Core.RepositoryContracts;

namespace Restaurants.Application.Restaurants.Commands.UpdateRestaurant;

public class UpdateRestaurantCommandHandler(
    ILogger<UpdateRestaurantCommandHandler> logger,
    IRestaurantsRepository repository,
    IMapper mapper
) : IRequestHandler<UpdateRestaurantCommand, bool>
{
    public async Task<bool> Handle(UpdateRestaurantCommand request, CancellationToken cancellationToken)
    {
        logger.LogInformation("Updating restaurant: {Id} with data: {@Restaurant}", request.Id, request);

        var restaurant = await repository.GetByIdAsync(request.Id);

        if (restaurant is null) throw new NotFoundException($"Restaurant with {request.Id} does not exist");


        mapper.Map(request, restaurant);

        await repository.SaveChangesAsync();

        return true;
    }
}