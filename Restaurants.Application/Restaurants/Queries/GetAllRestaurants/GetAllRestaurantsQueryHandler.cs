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
    public async Task<PagedResult<RestaurantDto>> Handle(
        GetAllRestaurantsQuery request,
        CancellationToken cancellationToken)
    {
        logger.LogInformation("Getting all restaurants");

        var (restaurants, totalCount) = await repository.GetAllMatchingAsync(
            request.SearchPhrase, request.PageNumber, request.PageSize, request.SortBy, request.SortDirection);
        var restaurantsDto = mapper.Map<IEnumerable<RestaurantDto>>(restaurants);

        var result =
            new PagedResult<RestaurantDto>(restaurantsDto, totalCount, request.PageSize, request.PageNumber);

        return result;
    }
}