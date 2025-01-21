using ProjectBank.Accounts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjectBank.Bank
{
    public class Loan
    {
        public string Name { get; set; }
        public int LoanedMoney { get; set; }
        public int MonthlyIncome { get; set; }
        public int MonthlyPayment { get; set; }
        public int MoneyToPay { get; set; }
        public int MoneyPaid { get; set; }
        public int Months { get; set; }
        public string DateOfGet { get; set; }
        public string DateOfSet { get; set; }

        public override bool Equals(object obj)
        {
            if (obj == null || GetType() != obj.GetType())
                return false;

            Loan other = (Loan)obj;
            return Name == other.Name &&
                   LoanedMoney == other.LoanedMoney &&
                   MonthlyIncome == other.MonthlyIncome &&
                   MonthlyPayment == other.MonthlyPayment &&
                   MoneyToPay == other.MoneyToPay &&
                   MoneyPaid == other.MoneyPaid;
        }
    }
}
