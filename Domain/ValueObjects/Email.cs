using System.Text.RegularExpressions;
using CleanArch.Domain.Common;

namespace CleanArch.Domain.ValueObjects;

public record Email
{
    public string Address { get; }

    private Email(string address) => Address = address;

    public static Result<Email> Create(string email)
    {
        if (string.IsNullOrWhiteSpace(email))
            return Result.Failure<Email>("E-mail não pode ser vazio.");

        if (!Regex.IsMatch(email, @"^[^@\s]+@[^@\s]+\.[^@\s]+$"))
            return Result.Failure<Email>("E-mail em formato inválido.");

        return Result.Success(new Email(email));
    }
}