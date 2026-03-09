using Domain.Common;
using System.Text.RegularExpressions;

namespace Domain.ValueObjects;

public record Email
{
    public string Address { get; }

    private Email(string address) => Address = address;

    public static Result<Email> Create(string email)
    {
        if (string.IsNullOrWhiteSpace(email))
            return Result.Failure<Email>("O e-mail não pode ser vazio.");

        if (!Regex.IsMatch(email, @"^[^@\s]+@[^@\s]+\.[^@\s]+$"))
            return Result.Failure<Email>("Formato de e-mail inválido.");

        return Result.Success(new Email(email.ToLower().Trim()));
    }

    public override string ToString() => Address;
}