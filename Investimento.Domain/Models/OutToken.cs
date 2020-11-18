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

        public string Token { get; set; }
        public User User { get; set; }
    }
}
