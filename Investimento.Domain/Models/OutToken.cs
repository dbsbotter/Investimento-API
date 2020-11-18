using Investimento.Domain.Entities;

namespace Investimento.Domain.Models
{
    public class OutToken
    {
        public OutToken(string token,
                        User user)
        {
            Token = token;
            User = user;
        }

        /// <summary>
        /// Token JWT
        /// </summary>
        public string Token { get; set; }

        /// <summary>
        /// Usuário
        /// </summary>
        public User User { get; set; }
    }
}
