using Investimento.Domain.Commands.Contracts;
using System.Collections.Generic;

namespace Investimento.Domain.Commands
{
    public class InvestimentCommand : ICommand
    {
        public InvestimentCommand()
        { }

        public InvestimentCommand(string description,
                                  IEnumerable<InvestimentItemCommand> investimentItems)
        {
            Description = description;
            InvestimentItems = investimentItems;
        }

        /// <summary>
        /// Descrição
        /// </summary>
        public string Description { get; set; }

        /// <summary>
        /// Lista de investimentos
        /// </summary>
        public IEnumerable<InvestimentItemCommand> InvestimentItems { get; set; }

        public class InvestimentItemCommand
        {
            public InvestimentItemCommand()
            { }

            public InvestimentItemCommand(string ticker,
                                          double quotation,
                                          int amount)
            {
                Ticker = ticker;
                Quotation = quotation;
                Amount = amount;
            }

            /// <summary>
            /// Código único da ação
            /// </summary>
            public string Ticker { get; set; }
            /// <summary>
            /// Valor da ação
            /// </summary>
            public double Quotation { get; set; }
            /// <summary>
            /// Quantidade de ações
            /// </summary>
            public int Amount { get; set; }
        }
    }
}