using FluentValidation;
using Investimento.Domain.Commands;
using Investimento.Domain.Repositories;

namespace Investimento.Domain.Validators
{
    public class UserValidator : AbstractValidator<UserCommand>
    {
        IUserRepository _userRepository;

        public UserValidator(IUserRepository userRepository)
        {
            _userRepository = userRepository;

            RuleFor(x => x.Username)
                .NotNull()
                .WithName("Usuário")
                .WithMessage("{PropertyName} deve ser informado")
                .NotEmpty()
                .WithMessage("{PropertyName} deve ser informado")
                .MaximumLength(10)
                .WithMessage("{PropertyName} deve ter no máximo {MaxLength}");

            RuleFor(x => x.Password)
                .NotNull()
                .WithName("Senha")
                .WithMessage("{PropertyName} deve ser informado")
                .NotEmpty()
                .WithMessage("{PropertyName} deve ser informado");

            RuleFor(x => x.Username)
                .MustAsync(async (username, cancellation) =>
                {
                    bool exists = await _userRepository.UserExists(username);

                    return !exists;
                }).WithMessage("Usuário já existe");
        }
    }
}