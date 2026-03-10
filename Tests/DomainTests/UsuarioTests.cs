using Domain.Entities;
using Domain.ValueObjects;
using Domain.Validators;
using Xunit;

namespace Tests.DomainTests;

public class UsuarioTests
{
    private readonly UsuarioValidator _validator = new();

    [Fact]
    public void CriarUsuario_ComDadosValidos_DevePassarNaValidacao()
    {
        // Arrange
        var emailResult = Email.Create("arquitetura@clean.com");
        var usuario = new Usuario("John Doe", emailResult.Value, true);

        // Act
        var result = _validator.Validate(usuario);

        // Assert
        Assert.True(result.IsValid);
        Assert.True(emailResult.IsSuccess);
    }

    [Fact]
    public void CriarEmail_ComFormatoInvalido_DeveRetornarFalha()
    {
        // Act
        var result = Email.Create("email_invalido.com");

        // Assert
        Assert.True(result.IsFailure);
        Assert.Equal("Formato de e-mail inválido.", result.Error);
    }

    [Fact]
    public void Usuario_ComNomeMuitoLongo_DeveFalharNaValidacao()
    {
        // Arrange
        var nomeLongo = new string('A', 121);
        var email = Email.Create("test@test.com").Value;
        var usuario = new Usuario(nomeLongo, email, true);

        // Act
        var result = _validator.Validate(usuario);

        // Assert
        Assert.False(result.IsValid);
        Assert.Contains(result.Errors, e => e.PropertyName == "Nome");
    }
}