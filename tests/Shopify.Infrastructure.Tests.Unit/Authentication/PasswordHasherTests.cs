using ErrorOr;
using Shopify.Infrastructure.Authentication;
using Shopify.Infrastructure.Authentication.Errors;
using Shouldly;

namespace Shopify.Infrastructure.Tests.Unit.Authentication;

public sealed class PasswordHasherTests
{
    private readonly PasswordHasher passwordHasher = new();

    [Theory]
    [InlineData("Password123!")]
    [InlineData("1Str0ng#Pass2")]
    [InlineData("Secur3P@ssw0rd!")]
    public void HashPassword_ShouldReturnHash_WhenPasswordIsStrongAndValid(string password)
    {
        // Act
        ErrorOr<string> result = passwordHasher.HashPassword(password);

        // Assert
        result.IsError.ShouldBeFalse();
        result.Value.ShouldNotBeNullOrWhiteSpace();
    }

    [Theory]
    [InlineData("weak")]
    [InlineData("Weak12!")]
    [InlineData("12345678")]
    [InlineData("alllowercase1!")]
    [InlineData("ALLUPPERCASE1!")]
    public void HashPassword_ShouldReturnError_WhenPasswordIsWeak(string password)
    {
        // Act
        ErrorOr<string> result = passwordHasher.HashPassword(password);

        // Assert
        result.IsError.ShouldBeTrue();
        result.FirstError.Code.ShouldBe(PasswordHasherErrors.PasswordTooWeak.Code);
        result.FirstError.Description.ShouldBe(PasswordHasherErrors.PasswordTooWeak.Description);
    }

    [Fact]
    public void IsCorrectPassword_ShouldReturnTrue_WhenPasswordIsValid()
    {
        // Arrange
        string password = "Valid123!";
        string hashPassword = passwordHasher.HashPassword(password).Value;

        // Act
        bool isValid = passwordHasher.IsCorrectPassword(password, hashPassword);

        // Assert
        isValid.ShouldBeTrue();
    }

    [Fact]
    public void IsCorrectPassword_ShouldReturnFalse_WhenPasswordIsInvalid()
    {
        // Arrange
        string password = "Valid123!";
        string wrongPassword = "Wrong123!";
        string hashPassword = passwordHasher.HashPassword(password).Value;

        // Act
        bool isValid = passwordHasher.IsCorrectPassword(wrongPassword, hashPassword);

        // Assert
        isValid.ShouldBeFalse();
    }
}