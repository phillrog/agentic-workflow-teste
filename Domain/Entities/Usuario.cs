using Domain.ValueObjects;

namespace Domain.Entities;

public class Usuario
{
    public int Id { get; private set; }
    public string Nome { get; private set; } = string.Empty;
    public Email Email { get; private set; } = null!;
    public bool Status { get; private set; }

    // Construtor privado para garantir uso do factory ou instanciamento controlado
    private Usuario() { }

    public Usuario(string nome, Email email, bool status)
    {
        Nome = nome;
        Email = email;
        Status = status;
    }

    public void Update(string nome, Email email, bool status)
    {
        Nome = nome;
        Email = email;
        Status = status;
    }

    public void Ativar() => Status = true;
    public void Desativar() => Status = false;
}