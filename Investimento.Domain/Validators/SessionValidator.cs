using FluentValidation;
using Investimento.Domain.Commands;
using Investimento.Domain.Repositories;

namespace Investimento.Domain.Validators
{
    public class SessionValidator : AbstractValidator<SessionCommand>
    {
        public SessionValidator()
        {
            RuleFor(x => x.Username)
                .NotNull()
                .WithName("Usuário")
                .WithMessage("{PropertyName} deve ser informado.")
                .NotEmpty()
                .WithMessage("{PropertyName} deve ser informado.");

            RuleFor(x => x.Password)
                .NotNull()
                .WithName("Senha")
                .WithMessage("{PropertyName} deve ser informado.")
                .NotEmpty()
                .WithMessage("{PropertyName} deve ser informado.");
        }
    }
}