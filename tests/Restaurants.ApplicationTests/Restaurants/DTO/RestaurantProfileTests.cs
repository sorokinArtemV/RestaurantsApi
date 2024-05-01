using AutoMapper;
using FluentAssertions;
using Restaurants.Application.Restaurants.Commands.AddRestaurant;
using Restaurants.Application.Restaurants.Commands.UpdateRestaurant;
using Restaurants.Application.Restaurants.DTO;
using Restaurants.Core.Domain.Entities;

namespace z_Restaurants.ApplicationTests.Restaurants.DTO;

public class RestaurantProfileTests
{
    private readonly IMapper _mapper;

    public RestaurantProfileTests()
    {
        var config = new MapperConfiguration(cfg => cfg.AddProfile<RestaurantProfile>());

        _mapper = config.CreateMapper();
    }


    [Fact]
    public void CreateMap_ShouldMapCorrectly_ForRestaurantToRestaurantDto()
    {
        var restaurant = new Restaurant
        {
            Id = 1,
            Name = "Test",
            Description = "Test",
            Category = "Fast food",
            HasDelivery = true,
            ContactEmail = "test@qa.com",
            ContactNumber = "1234567890",
            Address = new Address { City = "Test", PostalCode = "123456", Street = "Test" }
        };

        var restaurantDto = _mapper.Map<RestaurantDto>(restaurant);

        restaurantDto.Should().NotBeNull();
        restaurantDto.Id.Should().Be(restaurant.Id);
        restaurantDto.Name.Should().Be(restaurant.Name);
        restaurantDto.Description.Should().Be(restaurant.Description);
        restaurantDto.Category.Should().Be(restaurant.Category);
        restaurantDto.HasDelivery.Should().Be(restaurant.HasDelivery);
        restaurantDto.City.Should().Be(restaurant.Address.City);
        restaurantDto.PostalCode.Should().Be(restaurant.Address.PostalCode);
        restaurantDto.Street.Should().Be(restaurant.Address.Street);
    }

    [Fact]
    public void CreateMap_ShouldMapCorrectly_ForRestaurantCommandToRestaurant()
    {
        var command = new AddRestaurantCommand
        {
            Name = "Test",
            Category = "Fast food",
            Description = "Test",
            HasDelivery = true,
            ContactEmail = "test@qa.com",
            ContactNumber = "1234567890",
            PostalCode = "123456",
            Street = "Test",
            City = "Test"
        };

        var restaurant = _mapper.Map<Restaurant>(command);

        restaurant.Should().NotBeNull();
        restaurant.Name.Should().Be(command.Name);
        restaurant.Category.Should().Be(command.Category);
        restaurant.Description.Should().Be(command.Description);
        restaurant.HasDelivery.Should().Be(command.HasDelivery);
        restaurant.ContactEmail.Should().Be(command.ContactEmail);
        restaurant.ContactNumber.Should().Be(command.ContactNumber);
        restaurant.Address.Should().NotBeNull();
        restaurant.Address.City.Should().Be(command.City);
        restaurant.Address.PostalCode.Should().Be(command.PostalCode);
        restaurant.Address.Street.Should().Be(command.Street);
    }

    [Fact]
    public void CreateMap_ShouldMapCorrectly_ForUpdateRestaurantCommandToRestaurant()
    {
        var command = new UpdateRestaurantCommand
        {
            Id = 1,
            Name = "Test",
            Description = "Test",
            HasDelivery = false
        };

        var restaurant = _mapper.Map<Restaurant>(command);

        restaurant.Should().NotBeNull();
        restaurant.Id.Should().Be(command.Id);
        restaurant.Name.Should().Be(command.Name);
        restaurant.Description.Should().Be(command.Description);
        restaurant.HasDelivery.Should().Be(command.HasDelivery);
    }
}