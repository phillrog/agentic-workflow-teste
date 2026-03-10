using CleanArch.Domain.Common;
using CleanArch.Domain.ValueObjects;

namespace CleanArch.Domain.Entities;

public enum UsuarioStatus { Ativo, Inativo }

public class Usuario
{
    public Guid Id { get; private set; }
    public string Nome { get; private set; }
    public Email Email { get; private set; }
    public UsuarioStatus Status { get; private set; }

    private Usuario() { } // Para EF Core

    private Usuario(Guid id, string nome, Email email)
    {
        Id = id;
        Nome = nome;
        Email = email;
        Status = UsuarioStatus.Ativo;
    }

    public static Result<Usuario> Create(string nome, string emailRaw)
    {
        if (string.IsNullOrWhiteSpace(nome) || nome.Length > 120)
            return Result.Failure<Usuario>("Nome deve ter entre 1 e 120 caracteres.");

        var emailResult = Email.Create(emailRaw);
        if (emailResult.IsFailure)
            return Result.Failure<Usuario>(emailResult.Error);

        return Result.Success(new Usuario(Guid.NewGuid(), nome, emailResult.Value));
    }

    public void Inativar() => Status = UsuarioStatus.Inativo;
    
    public void Update(string nome)
    {
        if (!string.IsNullOrWhiteSpace(nome) && nome.Length <= 120)
            Nome = nome;
    }
}