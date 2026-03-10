using Domain.Entities;
using FluentValidation;

namespace Domain.Validators;

public class UsuarioValidator : AbstractValidator<Usuario>
{
    public UsuarioValidator()
    {
        RuleFor(x => x.Nome)
            .NotEmpty().WithMessage("O nome é obrigatório.")
            .MaximumLength(120).WithMessage("O nome deve ter no máximo 120 caracteres.");

        RuleFor(x => x.Email)
            .NotNull().WithMessage("O objeto de e-mail é obrigatório.");

        RuleFor(x => x.Email.Address)
            .MaximumLength(200).WithMessage("O e-mail deve ter no máximo 200 caracteres.");
    }
}