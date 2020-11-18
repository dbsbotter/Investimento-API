using Investimento.Domain.Commands;
using Investimento.Domain.Entities;
using Investimento.Domain.Handlers.Contracts;
using Investimento.Domain.Repositories;
using System.Threading.Tasks;

namespace Investimento.Domain.Handlers
{
    public class CreateUserHandler : IHandler<UserCommand, Task<User>>
    {
        private readonly IUserRepository _userRepository;

        public CreateUserHandler(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }

        public async Task<User> Handle(UserCommand command)
        {
            string passwordCrypted = BCrypt.Net.BCrypt.HashPassword(command.Password);

            var user = new User(command.Username,
                                passwordCrypted);

            user = await _userRepository.Create(user);

            user.SetPasswordNull();

            return user;
        }
    }
}