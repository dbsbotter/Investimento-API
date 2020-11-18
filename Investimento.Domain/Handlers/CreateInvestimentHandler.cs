using Investimento.Domain.Commands;
using Investimento.Domain.Entities;
using Investimento.Domain.Exceptions;
using Investimento.Domain.Handlers.Contracts;
using Investimento.Domain.Repositories;
using Microsoft.AspNetCore.Http;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace Investimento.Domain.Handlers
{
    public class CreateInvestimentHandler : IHandler<InvestimentCommand, Task<Investiment>>
    {
        private readonly IInvestimentRepository _investimentRepository;
        private readonly IHttpContextAccessor _httpContextAccessor;

        public CreateInvestimentHandler(IInvestimentRepository investimentRepository,
                                        IHttpContextAccessor httpContextAccessor)
        {
            _investimentRepository = investimentRepository;
            _httpContextAccessor = httpContextAccessor;
        }

        public async Task<Investiment> Handle(InvestimentCommand command)
        {
            var investiment = new Investiment(command.Description,
                                              Guid.Parse(_httpContextAccessor.HttpContext.User.Identity.Name));

            command.InvestimentItems.ToList().ForEach(x => investiment.AddItem(new InvestimentItem(x.Ticker,
                                                                                                   x.Quotation,
                                                                                                   x.Amount)));

            #region Bisiness
            var superiores = investiment.InvestimentItems.Where(x => x.Total > 50000).ToList();

            if (superiores.Count > 0)
                throw new BusinessRuleValidationException(string.Join(Environment.NewLine, superiores.Select(x => $"{x.Ticker} é superior a R$ 50.000,00.")));

            if (investiment.Total > 50000)
                throw new BusinessRuleValidationException("Investimento total não pode ser superior a R$ 50.000,00");
            #endregion

            return await _investimentRepository.Create(investiment);
        }
    }
}