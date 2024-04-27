using AutoMapper;
using MediatR;
using Microsoft.Extensions.Logging;
using Restaurants.Core.Domain.Constants;
using Restaurants.Core.Domain.Entities;
using Restaurants.Core.Domain.RepositoryContracts;
using Restaurants.Core.Domain.ServiceContracts;
using Restaurants.Core.Exceptions;

namespace Restaurants.Application.Restaurants.Commands.UpdateRestaurant;

public class UpdateRestaurantCommandHandler(
    ILogger<UpdateRestaurantCommandHandler> logger,
    IRestaurantsRepository repository,
    IMapper mapper,
    IRestaurantAuthorizationService authorizationService
) : IRequestHandler<UpdateRestaurantCommand>
{
    public async Task Handle(UpdateRestaurantCommand request, CancellationToken cancellationToken)
    {
        logger.LogInformation("Updating restaurant: {Id} with data: {@Restaurant}", request.Id, request);

        var restaurant = await repository.GetByIdAsync(request.Id);

        if (restaurant is null) throw new NotFoundException(nameof(Restaurant), request.Id.ToString());

        if (authorizationService.Authorize(restaurant, ResourceOperation.Update)) throw new ForbidException();
        
        mapper.Map(request, restaurant);

        await repository.SaveChangesAsync();
    }
}