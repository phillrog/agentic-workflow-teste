using CleanArch.Domain.Entities;
using Xunit;

namespace CleanArch.Tests.Domain;

public class UsuarioTests
{
    [Fact]
    public void CreateUsuario_ComNomeLongo_DeveRetornarFalha()
    {
        // Arrange
        var nomeLongo = new string('A', 121);
        
        // Act
        var result = Usuario.Create(nomeLongo, "teste@email.com");

        // Assert
        Assert.True(result.IsFailure);
        Assert.Equal("Nome deve ter entre 1 e 120 caracteres.", result.Error);
    }

    [Theory]
    [InlineData("emailinvalido")]
    [InlineData("")]
    [InlineData("usuario@")]
    public void CreateUsuario_ComEmailInvalido_DeveRetornarFalha(string emailInvalido)
    {
        // Act
        var result = Usuario.Create("Nome Valido", emailInvalido);

        // Assert
        Assert.True(result.IsFailure);
        Assert.Contains("E-mail", result.Error);
    }

    [Fact]
    public void CreateUsuario_DadosValidos_DeveRetornarSucesso()
    {
        // Act
        var result = Usuario.Create("Arquiteto .NET", "senior@dotnet.com");

        // Assert
        Assert.True(result.IsSuccess);
        Assert.NotNull(result.Value);
        Assert.Equal(UsuarioStatus.Ativo, result.Value.Status);
    }
}