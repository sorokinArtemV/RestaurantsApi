using AutoMapper;
using Restaurants.Core.Entities;

namespace Restaurants.Application.Dishes.DTO;

public class DishesProfile : Profile
{
    public DishesProfile()
    {
        CreateMap<Dish, DishDto>().ReverseMap();
    }
}