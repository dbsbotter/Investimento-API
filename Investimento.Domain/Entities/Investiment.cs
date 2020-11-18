using System;
using System.Collections.Generic;
using System.Linq;

namespace Investimento.Domain.Entities
{
    public class Investiment : Entity
    {
        public Investiment(string description,
                           Guid userId)
        {
            Description = description;
            UserId = userId;
            InvestimentItems = new List<InvestimentItem>();
        }

        public string Description { get; private set; }
        public Guid UserId { get; private set; }
        public ICollection<InvestimentItem> InvestimentItems { get; private set; }
        public double Total { get; private set; }

        private void SetTotal()
        {
            Total = InvestimentItems.Sum(x => x.Total);
        }

        public void AddItem(InvestimentItem item)
        {
            InvestimentItems.Add(item);
            SetTotal();
        }
    }
}
