using Investimento.Domain.Commands.Contracts;

namespace Investimento.Domain.Commands
{
    public class UserCommand : ICommand
    {
        public UserCommand()
        { }

        public UserCommand(string username,
                           string password)
        {
            Username = username;
            Password = password;
        }

        /// <summary>
        /// Usuário
        /// </summary>
        public string Username { get; set; }

        /// <summary>
        /// Senha do usuário
        /// </summary>
        public string Password { get; set; }
    }
}