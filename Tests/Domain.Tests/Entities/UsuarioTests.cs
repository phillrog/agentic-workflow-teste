using Domain.Entities;
using Domain.ValueObjects;
using Xunit;

namespace Domain.Tests.Entities;

public class UsuarioTests
{
    [Fact]
    public void CriarUsuario_ComDadosValidos_DeveRetornarSucesso()
    {
        // Arrange
        var emailStr = "arquitetura@dotnet.com";
        var email = Email.Create(emailStr).Value;
        
        // Act
        var usuario = new Usuario(1, "João Silva", email, true);

        // Assert
        Assert.Equal("João Silva", usuario.Nome);
        Assert.Equal(emailStr, usuario.Email.Address);
    }

    [Fact]
    public void EmailVO_ComFormatoInvalido_DeveRetornarFalha()
    {
        // Act
        var result = Email.Create("email_invalido.com");

        // Assert
        Assert.True(result.IsFailure);
        Assert.Equal("O formato do e-mail é inválido.", result.Error);
    }
}