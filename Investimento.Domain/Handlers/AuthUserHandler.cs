using Investimento.Domain.Commands;
using Investimento.Domain.Exceptions;
using Investimento.Domain.Handlers.Contracts;
using Investimento.Domain.Models;
using Investimento.Domain.Repositories;
using Investimento.Domain.Services;
using Microsoft.Extensions.Configuration;
using System.Threading.Tasks;

namespace Investimento.Domain.Handlers
{
    public class AuthUserHandler : IHandler<SessionCommand, Task<OutToken>>
    {
        private readonly IUserRepository _userRepository;
        private readonly IConfiguration _configuration;

        public AuthUserHandler(IUserRepository userRepository,
                               IConfiguration configuration)
        {
            _userRepository = userRepository;
            _configuration = configuration;
        }

        public async Task<OutToken> Handle(SessionCommand command)
        {
            var user = await _userRepository.GetByUsername(command.Username);

            if (user == null)
                throw new BusinessRuleValidationException("Usuário ou senha inválidos");

            if (!BCrypt.Net.BCrypt.Verify(command.Password, user.Password))
                throw new BusinessRuleValidationException("Usuário ou senha inválidos");

            var token = TokenService.GenerateToken(user, _configuration);

            user.SetPasswordNull();

            return new OutToken(token, user);
        }
    }
}