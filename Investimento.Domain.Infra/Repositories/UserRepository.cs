using Investimento.Domain.Entities;
using Investimento.Domain.Infra.Contexts;
using Investimento.Domain.Repositories;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Investimento.Domain.Infra.Repositories
{
    public class UserRepository : IUserRepository
    {
        private readonly DataContext _context;

        public UserRepository(DataContext context)
        {
            _context = context;
        }

        public async Task<User> Create(User user)
        {
            _context.Users.Add(user);
            await _context.SaveChangesAsync();

            return user;
        }

        public async Task<IEnumerable<User>> GetAll()
        {
            var result = await _context.Users.AsNoTracking().ToListAsync();

            return result;
        }

        public async Task<User> GetByUsername(string username)
        {
            return await _context.Users.AsNoTracking().Where(x => x.Username == username).FirstOrDefaultAsync();
        }

        public async Task<bool> UserExists(string username)
        {
            return await _context.Users.AsNoTracking().AnyAsync(x => x.Username == username);
        }
    }
}