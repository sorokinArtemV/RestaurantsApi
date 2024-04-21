using MediatR;
using Restaurants.Application.Restaurants.DTO;

namespace Restaurants.Application.Restaurants.Queries.GetRestaurantById;

public class GetRestaurantByIdQuery : IRequest<RestaurantDto?>
{
    public int Id { get; set; }
}