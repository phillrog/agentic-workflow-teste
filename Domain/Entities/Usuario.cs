using Domain.Common;
using Domain.ValueObjects;

namespace Domain.Entities;

public class Usuario
{
    public int Id { get; private set; }
    public string Nome { get; private set; }
    public Email Email { get; private set; }
    public bool Status { get; private set; }

    // Construtor para o EF Core
    protected Usuario() { }

    public Usuario(int id, string nome, Email email, bool status)
    {
        Id = id;
        Nome = nome;
        Email = email;
        Status = status;
    }

    public void UpdateInfo(string nome, bool status)
    {
        Nome = nome;
        Status = status;
    }
}