using Xunit;
using Domain;

namespace Tests;

public class ProductTests
{
    [Fact]
    public void Product_ValidData_ShouldReturnTrue()
    {
        // Arrange
        var product = new Product { Id = 1, Name = "Test Product", Price = 10.5m };

        // Act
        var isValid = product.IsValid();

        // Assert
        Assert.True(isValid);
    }

    [Fact]
    public void Product_InvalidPrice_ShouldReturnFalse()
    {
        // Arrange
        var product = new Product { Id = 2, Name = "Invalid Product", Price = -1 };

        // Act
        var isValid = product.IsValid();

        // Assert
        Assert.False(isValid);
    }
}