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

            public string Ticker { get; set; }
            public double Quotation { get; set; }
            public int Amount { get; set; }
        }
    }
}