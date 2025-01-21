using ProjectBank.Accounts;
using ProjectBank.FileMeneger;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjectBank.Bank
{
    internal class Bank
    {
        public static bool Withdraw(int ammount)
        {
            if (Signed.account !=  null && Signed.account.Balance >= ammount)
            {
                Signed.account.Balance -= ammount;
                Auth.Save();
                return true;
            }
            return false;
        }

        public static bool Invest(int ammount)
        {
            if (Signed.account != null)
            {
                Signed.account.Balance += ammount;
                Auth.Save();
                return true;
            }
            return false;
        }

        public static bool Transfer(string name, int amount)
        {
            string path = Environment.GetFolderPath(Environment.SpecialFolder.Desktop) + "/Bank/AccountData/Accounts.xml";
            try
            {
                Account account = XmlMethods.FindInXML(name, path);
                if(Signed.account.Balance >= amount)
                {
                    XmlMethods.RemoveFromXML(account, path);
                    account.Balance += amount;
                    account.Serialize(path);
                    Auth.Save();
                    return true;
                }
                return false;
            }
            catch (Exception)
            {
                return false;
            }
        }

        public static bool RequestLoan(int Money, int Months)
        {
            string path = Environment.GetFolderPath(Environment.SpecialFolder.Desktop) + "/Bank/BankData/RequestedLoans.xml";
            if(Signed.account.Loan == null)
            {
                int moneyWithPersent = Money + (Money * 20) / 100;
                Loan loan = new Loan() { MonthlyPayment = moneyWithPersent / Months, LoanedMoney = Money, MonthlyIncome = Signed.account.Income, Months = Months, MoneyToPay = moneyWithPersent, MoneyPaid = 0, Name = Signed.account.Name };
                loan.Serialize(path);
                return true;
            }
            else
            {
                return false;
            }
        }

        public static bool ApproveLoan(string name)
        {
            try
            {
                string path = Environment.GetFolderPath(Environment.SpecialFolder.Desktop) + "/Bank/BankData/RequestedLoans.xml";
                string pathAccounts = Environment.GetFolderPath(Environment.SpecialFolder.Desktop) + "/Bank/AccountData/Accounts.xml";
                string pathLoans = Environment.GetFolderPath(Environment.SpecialFolder.Desktop) + "/Bank/BankData/Loans.xml";
                Loan loan = XmlMethods.FindLoanInXML(name, path);
                Account account = XmlMethods.FindInXML(name, pathAccounts);
                account.RemoveFromXML(pathAccounts);
                account.Loan = loan;
                account.Balance = loan.LoanedMoney;
                DateTime a = DateTime.Now;
                DateTime b = DateTime.Now.AddMonths(loan.Months);
                loan.DateOfGet = a.ToString("HH:mm:ss dd/MM/yyyy");
                loan.DateOfSet = b.ToString("HH:mm:ss dd/MM/yyyy");
                account.Serialize(pathAccounts);
                loan.Serialize(pathLoans);
                loan.RemoveFromXML(path);
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }

        public static bool CancelLoan(string name)
        {
            try
            {
                string path = Environment.GetFolderPath(Environment.SpecialFolder.Desktop) + "/Bank/BankData/RequestedLoans.xml";
                Loan loan = XmlMethods.FindLoanInXML(name, path);
                loan.RemoveFromXML(path);
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }

        public static List<Loan> GetListOfRequstedLoans()
        {
            string path = Environment.GetFolderPath(Environment.SpecialFolder.Desktop) + "/Bank/BankData/RequestedLoans.xml";
            try
            {
                List<Loan> loans = XmlMethods.Deserialize<Loan>(path);
                if(loans.Count != 0)
                {
                    return loans;
                }
                return null;
            }
            catch (Exception)
            {
                return null;
            }
        }

        public static List<Account> GetWorkers()
        {
            try
            {
                string path = Environment.GetFolderPath(Environment.SpecialFolder.Desktop) + "/Bank/AccountData/Accounts.xml";
                List<Account> workers = XmlMethods.Deserialize<Account>(path);
                List<Account> result = new List<Account>();
                foreach  (Account i in workers)
                {
                    if(i.Role == Roles.Worker)
                    {
                        result.Add(i);
                    }
                }
                return result;
            }
            catch (Exception)
            {
                return null;
            }
        }
    }
}
