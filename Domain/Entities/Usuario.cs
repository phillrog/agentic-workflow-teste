using Domain.Common;
using Domain.ValueObjects;

namespace Domain.Entities;

public class Usuario
{
    public int Id { get; private set; }
    public string Nome { get; private set; } = string.Empty;
    public Email Email { get; private set; } = null!;
    public bool Status { get; private set; }

    // Construtor para EF
    protected Usuario() { }

    public Usuario(string nome, Email email)
    {
        Update(nome, email);
        Status = true; // Ativo por padrão
    }

    public void Update(string nome, Email email)
    {
        if (string.IsNullOrWhiteSpace(nome)) throw new ArgumentException("Nome é obrigatório");
        if (nome.Length > 120) throw new ArgumentException("Nome muito longo");

        Nome = nome;
        Email = email;
    }

    public void AlterarStatus(bool novoStatus)
    {
        Status = novoStatus;
    }
}