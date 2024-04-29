using AutoMapper;
using MediatR;
using Microsoft.Extensions.Logging;
using Restaurants.Application.Common;
using Restaurants.Application.Restaurants.DTO;
using Restaurants.Core.Domain.RepositoryContracts;

namespace Restaurants.Application.Restaurants.Queries.GetAllRestaurants;

public class GetAllRestaurantsQueryHandler(
    ILogger<GetAllRestaurantsQueryHandler> logger,
    IRestaurantsRepository repository,
    IMapper mapper) : IRequestHandler<GetAllRestaurantsQuery, PagedResult<RestaurantDto>>
{
    public async Task<IEnumerable<RestaurantDto>> Handle(
        GetAllRestaurantsQuery request,
        CancellationToken cancellationToken)
    {
        logger.LogInformation("Getting all restaurants");

        var restaurants = await repository.GetAllMatchingAsync(
            request.SearchPhrase, request.PageNumber, request.PageSize);
        var restaurantsDto = mapper.Map<IEnumerable<RestaurantDto>>(restaurants);

        var result =
            PagedResult<RestaurantDto>(restaurantsDto, restaurants.TotalCount, request.PageSize, request.PageNumber);

        return restaurantsDto;
    }
}