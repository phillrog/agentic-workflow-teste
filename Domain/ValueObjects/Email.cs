using System.Text.RegularExpressions;
using Domain.Common;

namespace Domain.ValueObjects;

public record Email
{
    public string Address { get; }

    private Email(string address) => Address = address;

    public static Result<Email> Create(string email)
    {
        if (string.IsNullOrWhiteSpace(email))
            return Result<Email>.Failure("O e-mail não pode ser vazio.");

        if (email.Length > 200)
            return Result<Email>.Failure("O e-mail deve ter no máximo 200 caracteres.");

        // Regex otimizada para validação de e-mail
        var regex = new Regex(@"^[^@\s]+@[^@\s]+\.[^@\s]+$");
        if (!regex.IsMatch(email))
            return Result<Email>.Failure("O formato do e-mail é inválido.");

        return Result<Email>.Success(new Email(email.ToLower().Trim()));
    }

    public override string ToString() => Address;
}