using Investimento.Domain.Commands.Contracts;

namespace Investimento.Domain.Handlers.Contracts
{
    public interface IHandler<in TCommand, out TResult> where TCommand : ICommand
    {
        TResult Handle(TCommand command);
    }
}