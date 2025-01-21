using ProjectBank.FileMeneger;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjectBank.Accounts
{
    internal class Auth
    {
        public static bool SignIn(string name, string password)
        {
            string path = Environment.GetFolderPath(Environment.SpecialFolder.Desktop) + "/Bank/AccountData/Accounts.xml";
            Account account = XmlMethods.FindInXML(name, path);
            if(account != null && account.Password == password)
            {
                Signed.account = account;
                return true;
            }
            return false;
        }

        public static void SignOut()
        {
            if(Signed.account != null)
            {
                Save();
                Signed.account = null;
            }
        }

        public static void Save()
        {
            if(Signed.account != null)
            {
                Remove(Signed.account.Name);
                string path = Environment.GetFolderPath(Environment.SpecialFolder.Desktop) + "/Bank/AccountData/Accounts.xml";
                Signed.account.Serialize(path);
            }
        }

        public static void Register(string Name, string Password, int Age , int income, int balance = 0) { 
            Account account = new Account() { Password = Password, Age = Age, Balance = balance, Name = Name, Role = Roles.User, Income = income, Loan = null};
            string path = Environment.GetFolderPath(Environment.SpecialFolder.Desktop) + "/Bank/AccountData/Accounts.xml";
            account.Serialize(path);
        }

        public static void Remove(string name)
        {
                string path = Environment.GetFolderPath(Environment.SpecialFolder.Desktop) + "/Bank/AccountData/Accounts.xml";
                Account account = XmlMethods.FindInXML(name, path);
                account.RemoveFromXML(path);
        }
    }
}
