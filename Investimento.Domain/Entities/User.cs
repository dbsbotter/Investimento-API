namespace Investimento.Domain.Entities
{
    public class User : Entity
    {
        public User(string username,
                    string password)
        {
            Username = username;
            Password = password;
        }

        public string Username { get; private set; }
        public string Password { get; private set; }

        public void SetPasswordNull()
        {
            this.Password = null;
        }
    }
}