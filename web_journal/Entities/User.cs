using System;
using System.Collections.Generic;

#nullable disable

namespace web_journal
{
    public partial class User
    {
        public User(string login, string password, string role)
        {
            Login = login;
            Password = password;
            Role = role;
        }

        public long Id { get; set; }
        public string Login { get; set; }
        public string Password { get; set; }
        public string Role { get; set; }

        public virtual Student Student { get; set; }
        public virtual Teacher Teacher { get; set; }
    }
}
