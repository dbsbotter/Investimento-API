using Investimento.Domain.Entities;
using System;
using System.Linq.Expressions;

namespace Investimento.Domain.Queries
{
    public static class InvestimentQueries
    {
        public static Expression<Func<Investiment, bool>> GetAllByUser(Guid userId)
        {
            return x => x.UserId == userId;
        }
    }
}
