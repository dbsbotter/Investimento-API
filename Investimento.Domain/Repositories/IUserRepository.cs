using Investimento.Domain.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Investimento.Domain.Repositories
{
    public interface IUserRepository
    {
        Task<User> Create(User user);
        Task<IEnumerable<User>> GetAll();
        Task<User> GetByUsername(string username);
        Task<bool> UserExists(string username);
    }
}