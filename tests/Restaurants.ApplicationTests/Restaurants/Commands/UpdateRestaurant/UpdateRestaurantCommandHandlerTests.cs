using AutoMapper;
using FluentAssertions;
using Microsoft.Extensions.Logging;
using Moq;
using Restaurants.Application.Restaurants.Commands.UpdateRestaurant;
using Restaurants.Core.Domain.Constants;
using Restaurants.Core.Domain.Entities;
using Restaurants.Core.Domain.RepositoryContracts;
using Restaurants.Core.Domain.ServiceContracts;
using Restaurants.Core.Exceptions;

namespace z_Restaurants.ApplicationTests.Restaurants.Commands.UpdateRestaurant;

public class UpdateRestaurantCommandHandlerTests
{
    private readonly Mock<IRestaurantAuthorizationService> _authorizationServiceMock;

    private readonly UpdateRestaurantCommandHandler _handler;
    private readonly Mock<ILogger<UpdateRestaurantCommandHandler>> _loggerMock;
    private readonly Mock<IMapper> _mapperMock;
    private readonly Mock<IRestaurantsRepository> _restaurantRepositoryMock;

    public UpdateRestaurantCommandHandlerTests()
    {
        _loggerMock = new Mock<ILogger<UpdateRestaurantCommandHandler>>();
        _restaurantRepositoryMock = new Mock<IRestaurantsRepository>();
        _mapperMock = new Mock<IMapper>();
        _authorizationServiceMock = new Mock<IRestaurantAuthorizationService>();

        _handler = new UpdateRestaurantCommandHandler(
            _loggerMock.Object,
            _restaurantRepositoryMock.Object,
            _mapperMock.Object,
            _authorizationServiceMock.Object
        );
    }

    [Fact]
    public async Task Handle_ShouldUpdateRestaurant_WhenCommandIsValid()
    {
        const int RestaurantId = 1;
        var command = new UpdateRestaurantCommand
        {
            Id = RestaurantId,
            Name = "Test",
            Description = "Test",
            HasDelivery = true
        };

        var restaurant = new Restaurant
        {
            Id = RestaurantId,
            Name = "Test",
            Description = "Test"
        };

        _restaurantRepositoryMock.Setup(x => x.GetByIdAsync(RestaurantId)).ReturnsAsync(restaurant);
        _authorizationServiceMock.Setup(x => x.Authorize(restaurant, ResourceOperation.Update)).Returns(true);

        //
        await _handler.Handle(command, CancellationToken.None);

        //
        _restaurantRepositoryMock.Verify(x => x.SaveChangesAsync(), Times.Once);
        _mapperMock.Verify(x => x.Map(command, restaurant), Times.Once);
    }

    [Fact]
    public async Task Handle_ShouldThrowNotFoundException_WhenRestaurantNotFound()
    {
        const int RestaurantId = 2;
        var command = new UpdateRestaurantCommand
        {
            Id = RestaurantId,
        };

        _restaurantRepositoryMock.Setup(x => x.GetByIdAsync(RestaurantId)).ReturnsAsync((Restaurant?)null);
        
        //
        var act = async () => await _handler.Handle(command, CancellationToken.None);
        
        //
        await act.Should().ThrowAsync<NotFoundException>()
            .WithMessage($"Restaurant with id {RestaurantId} does not exist.");
    }

    [Fact]
    public async Task Handle_ShouldThrowForbidException_WhenUserIsNotAuthorized()
    {
        const int RestaurantId = 3;
      
        var command = new UpdateRestaurantCommand
        {
            Id = RestaurantId,
        };

        var restaurant = new Restaurant
        {
            Id = RestaurantId,
        };
        
        _restaurantRepositoryMock.Setup(x => x.GetByIdAsync(RestaurantId)).ReturnsAsync(restaurant);
        _authorizationServiceMock.Setup(x => x.Authorize(restaurant, ResourceOperation.Update)).Returns(false);
        
        //
        var act = async () => await _handler.Handle(command, CancellationToken.None);

        //
        await act.Should().ThrowAsync<ForbidException>();
    }
}