using DataAccsess.DAO;
using DataAccsess.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccsess.Respository
{
    public class UserRespository : UserIRespository
    {
        public Account GetUserByEmailPass(string email, string password)
        {
            return UserDAO.Instance.GetUserByEmailandPassword(email, password);
        }

        public Account registerUser(Account user)
        {
            return UserDAO.Instance.Regiseduser(user);
        }
    }
}
