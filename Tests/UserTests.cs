using Application.DTOs;
using Application.Services;
using Domain.Entities;
using Domain.Interfaces;
using Moq;
using Xunit;

namespace Tests;

public class UserTests
{
    private readonly Mock<IUserRepository> _repositoryMock;
    private readonly UserService _userService;

    public UserTests()
    {
        _repositoryMock = new Mock<IUserRepository>();
        _userService = new UserService(_repositoryMock.Object);
    }

    [Fact]
    public async Task CreateUser_ShouldReturnSuccess_WhenDataIsValid()
    {
        // Arrange
        var request = new UserRequest("John Doe", "john@example.com");

        // Act
        var result = await _userService.CreateAsync(request);

        // Assert
        Assert.True(result.IsSuccess);
        Assert.Equal("John Doe", result.Value!.Name);
        _repositoryMock.Verify(r => r.AddAsync(It.IsAny<User>()), Times.Once);
    }

    [Fact]
    public async Task GetById_ShouldReturnFailure_WhenUserDoesNotExist()
    {
        // Arrange
        _repositoryMock.Setup(r => r.GetByIdAsync(It.IsAny<Guid>()))
            .ReturnsAsync((User?)null);

        // Act
        var result = await _userService.GetByIdAsync(Guid.NewGuid());

        // Assert
        Assert.True(result.IsFailure);
        Assert.Equal("User not found", result.Error);
    }
}