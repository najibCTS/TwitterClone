using _20_MVC_Assignment_01.Models;
using System;
using System.Linq;
using log4net;

namespace _20_MVC_Assignment_01
{
    public class AccountRepository
    {
        private PRODEntities2 pRODEntities = new PRODEntities2();
        private static readonly ILog Log = LogManager.GetLogger(typeof(AccountRepository));
        public int Register(Person person)
        {
            pRODEntities.People.Add(person);
            try
            {
                return pRODEntities.SaveChanges();
            }
            catch (Exception ex)
            {
                Log.Fatal("Exception at Account Repositiry - Register", ex);
                throw;
            }
        }

        public bool AuthenticateUser(LoginViewModel model)
        {
            try
            {
                string encodedPassword = Helper.EncodePasswordMd5(model.Password);
                return pRODEntities.People.Where(a => a.user_id == model.Username && a.password == encodedPassword).Count() == 1 ? true : false;
            }
            catch (Exception ex)
            {
                Log.Fatal("Exception at Account Repositiry - AuthenticateUser", ex);
                throw;
            }
        }
    }
}