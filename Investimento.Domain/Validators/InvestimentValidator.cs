using FluentValidation;
using Investimento.Domain.Commands;

namespace Investimento.Domain.Validators
{
    public class InvestimentValidator : AbstractValidator<InvestimentCommand>
    {
        public InvestimentValidator()
        {
            RuleFor(x => x.Description)
                .NotNull()
                .WithName("Descrição")
                .WithMessage("{PropertyName} deve ser informado")
                .NotEmpty()
                .WithMessage("{PropertyName} deve ser informado")
                .MaximumLength(50)
                .WithMessage("{PropertyName} deve ter no máximo {MaxLength}");

            RuleFor(x => x.InvestimentItems)
                .NotNull()
                .WithName("Lista de ações")
                .WithMessage("{PropertyName} deve ser informado")
                .NotEmpty()
                .WithMessage("{PropertyName} deve ser informado");

            RuleForEach(x => x.InvestimentItems).ChildRules(items =>
            {
                items.RuleFor(x => x.Amount)
                     .GreaterThan(0)
                     .WithMessage("Quantidade de ações deve ser maior que zero");

                items.RuleFor(x => x.Quotation)
                     .GreaterThan(0)
                     .WithMessage("Valor da cotação deve ser maior que zero");

                items.RuleFor(x => x.Ticker)
                     .NotNull()
                     .WithMessage("{PropertyName} deve ser informado")
                     .NotEmpty()
                     .WithMessage("{PropertyName} deve ser informado");
            });
        }
    }
}