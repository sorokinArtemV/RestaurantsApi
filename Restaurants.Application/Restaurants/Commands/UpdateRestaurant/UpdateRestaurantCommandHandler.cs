using AutoMapper;
using MediatR;
using Microsoft.Extensions.Logging;
using Restaurants.Core.Domain.Entities;
using Restaurants.Core.Exceptions;
using Restaurants.Core.RepositoryContracts;

namespace Restaurants.Application.Restaurants.Commands.UpdateRestaurant;

public class UpdateRestaurantCommandHandler(
    ILogger<UpdateRestaurantCommandHandler> logger,
    IRestaurantsRepository repository,
    IMapper mapper
) : IRequestHandler<UpdateRestaurantCommand>
{
    public async Task Handle(UpdateRestaurantCommand request, CancellationToken cancellationToken)
    {
        logger.LogInformation("Updating restaurant: {Id} with data: {@Restaurant}", request.Id, request);

        var restaurant = await repository.GetByIdAsync(request.Id);

        if (restaurant is null) throw new NotFoundException(nameof(Restaurant), request.Id.ToString());


        mapper.Map(request, restaurant);

        await repository.SaveChangesAsync();
    }
}