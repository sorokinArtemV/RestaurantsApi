using AutoMapper;
using Restaurants.Core.Entities;

namespace Restaurants.Application.DTO.DishDtos;

public class DishesProfile : Profile
{
    public DishesProfile()
    {
        CreateMap<Dish, DishDto>().ReverseMap();
    }
}