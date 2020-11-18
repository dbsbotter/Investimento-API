using System;

namespace Investimento.Domain.Entities
{
    public class InvestimentItem : Entity
    {
        public InvestimentItem(string ticker,
                               double quotation,
                               int amount)
        {
            Ticker = ticker;
            Quotation = quotation;
            Amount = amount;

            SetTotal();
        }

        public Guid InvestimentId { get; private set; }
        public Investiment Investiment { get; private set; }
        public string Ticker { get; private set; }
        public double Quotation { get; private set; }
        public int Amount { get; private set; }
        public double Total { get; private set; }

        private void SetTotal()
        {
            Total = this.Quotation * this.Amount;
        }
    }
}
