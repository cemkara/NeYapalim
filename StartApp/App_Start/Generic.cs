using StartAppModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace StartApp
{
    public class Generic
    {
        public bool EmailControl(string email)
        {
            StartAppEntities db = new StartAppEntities();
            if (db.Users.Any(x => x.Email == email))
            {
                return false;
            }
            return true;
        }

        public bool PhoneControl(string phone)
        {
            StartAppEntities db = new StartAppEntities();
            if (db.Users.Any(x => x.Phone == phone))
            {
                return false;
            }
            return true;
        }

        public string GetRandomPassword()
        {
            Random random = new Random();
            const string chars = "ABCDEFGHIJKLMNOPRSTUVYZabcdefghijklmnoprstuvyz0123456789";
            return new string(Enumerable.Repeat(chars, 10)
              .Select(s => s[random.Next(s.Length)]).ToArray());
        }
    }
}