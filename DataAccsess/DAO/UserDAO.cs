using DataAccsess.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccsess.DAO
{
    public class UserDAO
    {
        private static UserDAO instance = null;
        private static readonly object instanceLock = new object();
        private UserDAO() { }
        public static UserDAO Instance
        {
            get
            {
                lock (instanceLock)
                {
                    if (instance == null)
                        instance = new UserDAO();
                }
                return instance;
            }
        }

        public Account GetUserByEmailandPassword(string email, string password)
        {
            Account user;
            try
            {
                using (QLMTCContext context = new QLMTCContext())
                {
                    user = context.Accounts.ToList().FirstOrDefault(s => s.Usename.Equals(email) && s.Password.Equals(password));

                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
            return user;
        }
     
        public Account Regiseduser(Account w)
        {
            Account user;
            try
            {
                using (QLMTCContext context = new QLMTCContext())
                {
                    var cus = new Customer
                    {
                        CompanyName = w.Usename,
                    };
                    context.Customers.Add(cus);
                    context.SaveChanges();
                   // Customer customer = context.Customers.OrderBy(x => x.CustomerId).SingleOrDefault();
                    
                    user = new Account
                    {
                        Usename = w.Usename,
                        Password = w.Password,
                        CustomerId =cus.CustomerId,
                    };
                    context.Accounts.Add(user);
                    context.SaveChanges();
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
            return user;
        }
    }
}
