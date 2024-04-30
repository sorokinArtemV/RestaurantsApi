using AutoMapper;
using MediatR;
using Microsoft.Extensions.Logging;
using Restaurants.Application.Users;
using Restaurants.Core.Domain.Entities;
using Restaurants.Core.Domain.RepositoryContracts;

namespace Restaurants.Application.Restaurants.Commands.AddRestaurant;

public class AddRestaurantCommandHandler(
    ILogger<AddRestaurantCommandHandler> logger,
    IMapper mapper,
    IRestaurantsRepository repository,
    IUserContext userContext) : IRequestHandler<AddRestaurantCommand, int>
{
    public async Task<int> Handle(AddRestaurantCommand request, CancellationToken cancellationToken)
    {
        var currentUser = userContext.GetCurrentUser();

        logger.LogInformation("{UserEmail} [{UserId}] is adding new restaurant: {@Restaurant}",
            currentUser.Email, currentUser.Id, request);

        var restaurant = mapper.Map<Restaurant>(request);

        restaurant.OwnerId = currentUser.Id;

        var id = await repository.AddAsync(restaurant);

        return id;
    }
}