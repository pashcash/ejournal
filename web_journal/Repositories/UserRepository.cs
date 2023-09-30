using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace web_journal.Repositories
{
    public class UserRepository
    {
        //Проверка авторизации
        public bool AuthentificateUser(string login, string password, string role)
        {
            bool validUser;
            using (var db = new wpf_journalContext())
            {
                var command = db.Users.Select(p => p)
                                      .Where(p => p.Role == role && p.Login==login && p.Password==password);
                validUser = !command.Any() ? false : true;
            }
            return validUser;
        }

        //Поиск студента среди юзеров с указанным id
        public User FindUserById(long userId)
        {
            User user;
            using (var db = new wpf_journalContext())
            {
                user = db.Users.Select(p => p)
                               .Where(p => p.Id == userId)
                               .SingleOrDefault();
            }
            return user;
        }

        //Поиск юзера по логину
        public User FindUserByLogin(string login)
        {
            User user;
            using (var db = new wpf_journalContext())
            {
                user = db.Users.Select(p => p)
                               .Where(p => p.Login == login)
                               .SingleOrDefault();
            }
            return user;
        }


    }
}
