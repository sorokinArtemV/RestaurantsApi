using FluentValidation.TestHelper;
using Restaurants.Application.Restaurants.Commands.AddRestaurant;

namespace z_Restaurants.ApplicationTests.Restaurants.Commands.AddRestaurant;

public class AddRestaurantCommandValidatorTests
{
    [Fact]
    public void Validator_ShouldNotHaveAnyErrors_WhenCommandIsValid()
    {
        var command = new AddRestaurantCommand
        {
            Name = "Test",
            Category = "Fast food",
            Description = "Test",
            ContactEmail = "test@qa.com",
            PostalCode = "123456",
            ContactNumber = "1234567890"
        };

        var validator = new AddRestaurantCommandValidator();

        var result = validator.TestValidate(command);

        result.ShouldNotHaveAnyValidationErrors();
    }

    [Fact]
    public void Validator_ShouldHaveErrors_WhenCommandIsNotValid()
    {
        var command = new AddRestaurantCommand
        {
            Name = "T",
            Category = "FastFood",
            ContactEmail = "test.com",
            PostalCode = "12345",
            ContactNumber = "123456789"
        };

        var validator = new AddRestaurantCommandValidator();

        var result = validator.TestValidate(command);

        result.ShouldHaveValidationErrorFor(x => x.Name);
        result.ShouldHaveValidationErrorFor(x => x.Category);
        result.ShouldHaveValidationErrorFor(x => x.ContactEmail);
        result.ShouldHaveValidationErrorFor(x => x.PostalCode);
        result.ShouldHaveValidationErrorFor(x => x.ContactNumber);
    }

    [Theory]
    [InlineData("Italian")]
    [InlineData("Indian")]
    [InlineData("Mexican")]
    [InlineData("Japanese")]
    [InlineData("French")]
    [InlineData("American")]
    [InlineData("Fast food")]
    public void Validator_ShouldNotHaveErrors_WhenCategoryIsValid(string category)
    {
        var validator = new AddRestaurantCommandValidator();

        var command = new AddRestaurantCommand { Category = category };
        
        var result = validator.TestValidate(command);
        
        result.ShouldNotHaveValidationErrorFor(x => x.Category);
    }
    
    [Theory]
    [InlineData("123")]
    [InlineData("1234567")]
    [InlineData("123-456")]
    public void Validator_ShouldHaveErrors_WhenPostalCodeIsNotValid(string postalCode)
    {
        var validator = new AddRestaurantCommandValidator();

        var command = new AddRestaurantCommand { PostalCode = postalCode };
        
        var result = validator.TestValidate(command);
        
        result.ShouldHaveValidationErrorFor(x => x.PostalCode);
    }
}