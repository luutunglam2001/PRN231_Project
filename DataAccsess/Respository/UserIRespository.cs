using DataAccsess.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccsess.Respository
{
    public interface UserIRespository
    {
        public Account GetUserByEmailPass(string email, string password);
        public Account registerUser(Account user);
    }
}
