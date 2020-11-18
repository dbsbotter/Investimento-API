using Investimento.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Investimento.Domain.Repositories
{
    public interface IInvestimentRepository
    {
        Task<Investiment> Create(Investiment investimento);
        Task<IEnumerable<Investiment>> GetAllByUser(Guid userId);
    }
}