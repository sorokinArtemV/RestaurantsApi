using AutoMapper;

namespace Restaurants.Application.DTO.Dish;

public class DishesProfile : Profile
{
    public DishesProfile()
    {
        CreateMap<Core.Entities.Dish, DishDto>().ReverseMap();
    }
}