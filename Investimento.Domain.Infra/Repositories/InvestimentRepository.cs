using Investimento.Domain.Entities;
using Investimento.Domain.Infra.Contexts;
using Investimento.Domain.Queries;
using Investimento.Domain.Repositories;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Investimento.Domain.Infra.Repositories
{
    public class InvestimentRepository : IInvestimentRepository
    {
        private readonly DataContext _context;

        public InvestimentRepository(DataContext context)
        {
            _context = context;
        }

        public async Task<Investiment> Create(Investiment investimento)
        {
            _context.Investiments.Add(investimento);
            await _context.SaveChangesAsync();

            return investimento;
        }

        public async Task<IEnumerable<Investiment>> GetAllByUser(Guid userId)
        {
            return await _context.Investiments
                .AsNoTracking()
                .Include(x => x.InvestimentItems)
                .Where(InvestimentQueries.GetAllByUser(userId))
                .OrderBy(x => x.CreatedAt)
                .ToListAsync();
        }
    }
}