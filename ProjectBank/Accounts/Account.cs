using ProjectBank.Bank;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjectBank.Accounts
{
    public class Account
    {
        public string Name { get; set; }
        public string Password { get; set; }
        public int Age { get; set; }
        public int Balance { get; set; }
        public int Income { get; set; }
        public Loan Loan { get; set; }
        public Roles Role { get; set; }

        public override bool Equals(object obj)
        {
            if (obj == null || GetType() != obj.GetType())
                return false;

            Account other = (Account)obj;
            return Name == other.Name &&
                   Age == other.Age &&
                   Role == other.Role &&
                   Balance == other.Balance &&
                   Password == other.Password;
        }
    }
}
