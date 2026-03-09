using Domain.Entities;
using Domain.ValueObjects;
using Xunit;

namespace Tests.Domain;

public class UsuarioTests
{
    [Fact]
    public void Should_Create_Valid_User()
    {
        // Arrange
        var nome = "João Silva";
        var emailStr = "joao@exemplo.com";
        var email = Email.Create(emailStr).Value;

        // Act
        var usuario = new Usuario(nome, email);

        // Assert
        Assert.Equal(nome, usuario.Nome);
        Assert.Equal(emailStr, usuario.Email.Address);
        Assert.True(usuario.Status);
    }

    [Fact]
    public void Should_Fail_When_Email_Is_Invalid()
    {
        // Act
        var result = Email.Create("email-invalido");

        // Assert
        Assert.True(result.IsFailure);
        Assert.Equal("Formato de e-mail inválido.", result.Error);
    }

    [Fact]
    public void Should_Change_Status_Correctly()
    {
        // Arrange
        var email = Email.Create("teste@teste.com").Value;
        var usuario = new Usuario("Teste", email);

        // Act
        usuario.AlterarStatus(false);

        // Assert
        Assert.False(usuario.Status);
    }
}