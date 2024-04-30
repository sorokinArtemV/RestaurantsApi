using AutoMapper;
using FluentAssertions;
using Microsoft.Extensions.Logging;
using Moq;
using Restaurants.Application.Restaurants.Commands.AddRestaurant;
using Restaurants.Application.Users;
using Restaurants.Core.Domain.Entities;
using Restaurants.Core.Domain.RepositoryContracts;

namespace z_Restaurants.ApplicationTests.Restaurants.Commands.AddRestaurant;

public class AddRestaurantCommandHandlerTests
{
    [Fact]
    public async Task Handle_ShouldAddRestaurant_WhenCommandIsValid()
    {
        var loggerMock = new Mock<ILogger<AddRestaurantCommandHandler>>();
        var mapperMock = new Mock<IMapper>();

        var command = new AddRestaurantCommand();
        var restaurant = new Restaurant();

        mapperMock.Setup(x => x.Map<Restaurant>(command)).Returns(restaurant);

        var restaurantRepositoryMock = new Mock<IRestaurantsRepository>();
        restaurantRepositoryMock.Setup(x => x.AddAsync(It.IsAny<Restaurant>())).ReturnsAsync(1);

        var userContextMock = new Mock<IUserContext>();
        var currentUser = new CurrentUser("owner-1", "test@qa.com", [], null, null);
        userContextMock.Setup(x => x.GetCurrentUser()).Returns(currentUser);

        var commandHandler = new AddRestaurantCommandHandler(
            loggerMock.Object,
            mapperMock.Object,
            restaurantRepositoryMock.Object,
            userContextMock.Object
        );

        //
        var result = await commandHandler.Handle(command, CancellationToken.None);

        //
        result.Should().Be(1);
        restaurant.OwnerId.Should().Be("owner-1");
        restaurantRepositoryMock.Verify(x => x.AddAsync(restaurant), Times.Once);
    }
}