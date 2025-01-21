using ProjectBank.Accounts;
using ProjectBank.Design;
using ProjectBank.FileMeneger;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Channels;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace ProjectBank.Bank
{
    internal class Menu
    {
        public static void LoanMenu()
        {
            while (true)
            {
                Costom.Line();
                List<Loan> loans = Bank.GetListOfRequstedLoans();
                Console.WriteLine(" Actions: ");
                Say.Green("1", "Not approved loans");
                Say.Green("2", "Approve");
                Say.Green("3", "Cancel");
                Say.Red("4", "Back");
                Console.Write(" Option: ");
                int option = int.Parse(Console.ReadLine());
                if (option != 1 && option != 2 && option != 3 && option != 4)
                {
                    Say.Red("Error", "Enter correct number please!");
                }
                else if (option == 1)
                {
                    if (loans != null && loans.Count != 0)
                    {
                        for (int i = 0; i < loans.Count; i++)
                        {
                            Console.WriteLine($" ID {i + 1}: [Name: {loans[i].Name}] - [Money: {loans[i].LoanedMoney}] - [Monthly income: {loans[i].MonthlyIncome}] - [Montly payment: {loans[i].MonthlyPayment}]");
                        }
                    }
                    else
                    {
                        Say.Red("Error", "No loans to approve founded!");
                    }
                }
                else if (option == 2)
                {
                    if (loans.Count != 0)
                    {
                        Console.WriteLine(" Enter name of person to approve");
                        Console.Write(" Name: ");
                        string name = Console.ReadLine();
                        if(Bank.ApproveLoan(name))
                        {
                            Say.Green("Bank", "Loan approved successfully!");
                        }
                        else
                        {
                            Say.Red("Bank", "Unable to approve, loan not found!");
                        }
                    }
                }
                else if (option == 3)
                {
                    if (loans.Count != 0)
                    {
                        Console.Write(" Name: ");
                        string name = Console.ReadLine();
                        if(Bank.CancelLoan(name))
                        {
                            Say.Green("Bank", "Loan canceled successfully!");
                        }
                        else
                        {
                            Say.Red("Bank", "Unable to cancel loan!");
                        }
                    }
                }
                else if (option == 4)
                {
                    break;
                }
            }
        }

        public static void AccountOptions()
        {
            while (true)
            {
                Costom.Line();
                Console.WriteLine(" Choose option: ");
                Say.Green("1", "Sign Out");
                Say.Green("2", "Delete Account");
                Say.Green("3", "Edit");
                Say.Red("4", "Back");
                Console.Write(" Option: ");
                int option = int.Parse(Console.ReadLine());
                if (option != 1 && option != 2 && option != 3)
                {
                    Say.Red("Error", "Not correct number!");
                }
                else if (option == 1)
                {
                    Auth.SignOut();
                    Say.Green("Auth", "Signed out successfully!");
                    break;
                }
                else if (option == 2)
                {
                    string name = Signed.account.Name;
                    Auth.SignOut();
                    Auth.Remove(name);
                    break;
                }
                else if (option == 3)
                {
                    while (true)
                    {
                        Console.WriteLine("Options:");
                        Say.Green("1", "Password");
                        Say.Green("2", "Age");
                        Say.Green("3", "Income");
                        Say.Red("4", "Back");
                        Console.Write(" Option: ");
                        int optiona = int.Parse(Console.ReadLine());
                        if (optiona != 1 && optiona != 2 && optiona != 3 && optiona != 4)
                        {
                            Say.Red("Error", "Enter correct number!");
                        }
                        if (optiona == 1)
                        {
                            Console.Write(" Password: ");
                            string password = Console.ReadLine();
                            Signed.account.Password = password;
                        }
                        if (optiona == 2)
                        {
                            Console.Write(" Age: ");
                            int Age = int.Parse(Console.ReadLine());
                            Signed.account.Age = Age;
                        }
                        if (optiona == 3)
                        {
                            Console.Write(" Income: ");
                            int income = int.Parse(Console.ReadLine());
                            Signed.account.Income = income;
                        }
                        if (optiona == 4)
                        {
                            Auth.Save();
                            break;
                        }
                    }
                }
                else if (option == 4)
                {
                    break;
                }

            }
        }
        public static void start()
        {
            string AccountsPath = Environment.GetFolderPath(Environment.SpecialFolder.Desktop) + "/Bank/AccountData/Accounts.xml";

            Background.TurnOn();
            Console.ForegroundColor = ConsoleColor.Yellow;
            Say.Animate(" Made by: nikanatenaze", 1);
            Console.ForegroundColor = ConsoleColor.DarkYellow;
            Say.Animate(" STEP project made with love", 1);
            Console.ForegroundColor = ConsoleColor.White;
            Costom.Natenadze();
            while(true)
            {
                try
                {
                    if (Signed.account == null)
                    {
                        Costom.Line();
                        Console.WriteLine(" Choose option: ");
                        Say.Green("1", "Sign in");
                        Say.Green("2", "Register");
                        Say.Green("3", "Information");
                        Say.Green("4", "Our policy");
                        Say.Red("5", "Exit");
                        Console.Write(" Option: ");
                        int option = int.Parse(Console.ReadLine());
                        if (option != 1 && option != 2 && option != 3 && option != 4 && option != 5)
                            Say.Red("Error", "Enter correct number");
                        else if (option == 1)
                        {
                            if (XmlMethods.Count<Account>(AccountsPath) == 0)
                                Say.Red("Error", "In our base isn't any registred account!");
                            else
                            {
                                Console.Write(" Name: ");
                                string name = Console.ReadLine();
                                Console.Write(" Password: ");
                                string password = Console.ReadLine();
                                if (Auth.SignIn(name, password))
                                {
                                    Say.Green("Auth", "Signed in successfully!");
                                }
                                else
                                {
                                    Say.Red("Auth", "Account not found with same name and password!");
                                }
                            }
                        }
                        else if (option == 2)
                        {
                            Console.Write(" Name: ");
                            string name = Console.ReadLine();
                            Console.Write(" Password: ");
                            string password = Console.ReadLine();
                            Console.Write(" Age: ");
                            int age = int.Parse(Console.ReadLine());
                            Console.Write(" Monthly income: ");
                            int income = int.Parse(Console.ReadLine());
                            Auth.Register(name, password, age, income);
                            Say.Green("Auth", "Account registred successfully!");
                        }
                        else if (option == 3)
                        {
                            Say.Red("Error", "We are sorry. this option don't added yet!");
                        }
                        else if (option == 4)
                        {
                            Say.Red("Error", "We are sorry. this option don't added yet!");
                        }
                        else if (option == 5)
                        {
                            break;
                        }
                    }
                    else if (Signed.account != null && Signed.account.Role == 0)
                    {
                        Costom.Line();

                        Say.Blue("Signed in with:", $"{Signed.account.Name}", true);
                        Say.Yellow("Balance:", $"{Signed.account.Balance}", true);
                        Say.Green("1", "Withdraw");
                        Say.Green("2", "Invest");
                        Say.Green("3", "Transfer");
                        Say.Green("4", "Get loan");
                        Say.Green("5", "Pay loan");
                        Say.Green("6", "Account options");
                        Say.Red("7", "Exit");
                        Console.Write(" Option: ");
                        int option = int.Parse(Console.ReadLine());
                        if (option != 1 && option != 2 && option != 3 && option != 4 && option != 5 && option != 6 && option != 7)
                        {
                            Say.Red("Error", "Not correct number!");
                        }
                        else if (option == 1)
                        {
                            Console.Write(" Ammount: ");
                            int ammount = int.Parse(Console.ReadLine());
                            if (Bank.Withdraw(ammount))
                            {
                                Say.Green("Bank", "Successfully without money!");
                            }
                            else
                            {
                                Say.Red("Bank", "You don't have money on balance!");
                            }
                        }
                        else if (option == 2)
                        {
                            Console.Write(" Ammount: ");
                            int ammount = int.Parse(Console.ReadLine());
                            if (Bank.Invest(ammount))
                            {
                                Say.Green("Bank", "Successfully Invested money!");
                            }
                            else
                            {
                                Say.Red("Bank", "Unknown error can't invest money!");
                            }
                        }
                        else if (option == 3)
                        {
                            Console.Write(" Reciver name: ");
                            string name = Console.ReadLine();
                            Console.Write(" Ammount: ");
                            int ammount = int.Parse(Console.ReadLine());
                            if (Bank.Transfer(name, ammount))
                            {
                                Say.Green("Bank", "Transfer was successful!");
                            }
                            else
                            {
                                Say.Red("Bank", "Not enougth money or account don't found!");
                            }
                        }
                        else if (option == 4)
                        {
                            Console.Write(" Money to loan: ");
                            int money = int.Parse(Console.ReadLine());
                            Console.Write(" Months: ");
                            int months = int.Parse(Console.ReadLine());
                            Console.WriteLine($" Are you sure u want get loan? (pay with taxes: {money + (money * 20) / 100})");
                            Say.Green("1", "Yes");
                            Say.Red("2", "No");
                            Console.Write(" Option: ");
                            int YesNo = int.Parse(Console.ReadLine());
                            if (YesNo != 1 && YesNo != 2)
                            {
                                Say.Red("Error", "Enter correct number!");
                            }
                            else if (YesNo == 1)
                            {
                                if (Bank.RequestLoan(money, months))
                                {
                                    Say.Green("Bank", "Loan requested successfully!");
                                }
                                else
                                {
                                    Say.Red("Bank", "You have loan pay it first!");
                                }
                            }
                        }
                        else if (option == 5)
                        {
                            if(Signed.account.Loan != null)
                            {
                                if(Signed.account.Balance >= Signed.account.Loan.MoneyToPay)
                                {
                                    string pathLoans = Environment.GetFolderPath(Environment.SpecialFolder.Desktop) + "/Bank/BankData/Loans.xml";
                                    Loan loan = Signed.account.Loan;
                                    loan.RemoveFromXML(pathLoans);
                                    Signed.account.Balance -= Signed.account.Loan.MoneyToPay;
                                    Signed.account.Loan = null;
                                    Auth.Save();
                                    Say.Green("Bank", "Loan paid successfully!");
                                }
                                else
                                {
                                    Say.Red("Bank", "You don't have enough money on balance!");
                                }
                            }
                            else
                            {
                                Say.Red("Error", "You don't have loans to pay!");
                            }
                        }
                        else if (option == 6)
                        {
                            AccountOptions();
                        }
                        else if (option == 7)
                        {
                            Auth.SignOut();
                            break;
                        }
                    }
                    else if (Signed.account != null && Signed.account.Role == Roles.Worker)
                    {
                        Costom.Line();
                        Say.Green("Signed in with:", $"{Signed.account.Name}", true);
                        Say.Green("1", "Loans");
                        Say.Green("2", "Quit job");
                        Say.Green("3", "Account options");
                        Say.Red("4", "Exit");
                        Console.Write(" Option: ");
                        int option = int.Parse(Console.ReadLine());
                        if (option != 1 && option != 2 && option != 3 && option != 4)
                            Say.Red("Error", "Enter correct number!");
                        else if (option == 1)
                        {
                            LoanMenu();
                        }
                        else if (option == 2)
                        {
                            Signed.account.Role = 0;
                            Auth.Save();
                        }
                        else if (option == 3)
                        {
                            AccountOptions();
                        }
                        else if (option == 4)
                        {
                            Auth.Save();
                            break;
                        }
                    }
                    else if (Signed.account.Role == Roles.Meneger)
                    {
                        Costom.Line();
                        Say.Green("Signed in with:", $"{Signed.account.Name}", true);
                        Say.Green("1", "Workers");
                        Say.Green("2", "Edit salaries");
                        Say.Green("3", "Relese worker");
                        Say.Green("4", "Account options");
                        Say.Red("5", "Exit");
                        Console.Write(" Option: ");
                        int option = int.Parse(Console.ReadLine());
                        if (option != 1 && option != 2 && option != 3 && option != 4 && option != 5)
                        {
                            Say.Red("Error", "Enter correct number please!");
                        }
                        else if (option == 1)
                        {
                            List<Account> Worker = Bank.GetWorkers();
                            if (Worker != null && Worker.Count != 0)
                            {
                                foreach (Account i in Worker)
                                {
                                    int id = 1;
                                    Console.WriteLine($" ID {id}: [Name: {i.Name}] - [Salary: {i.Income}] - [Age: {i.Age}]");
                                    id++;
                                }
                            }
                            else
                            {
                                Say.Red("Error", "Workers don't found in a base!");
                            }
                        }
                        else if (option == 2)
                        {
                            Console.Write(" Name: ");
                            string name = Console.ReadLine();
                            Console.Write(" New salary: ");
                            int newSalary = int.Parse(Console.ReadLine());
                            Account a = XmlMethods.FindInXML(name, AccountsPath);
                            if (a.Role == Roles.Worker)
                            {
                                XmlMethods.RemoveFromXML(a, AccountsPath);
                                a.Income = newSalary;
                                a.Serialize(AccountsPath);
                            }
                            else
                                Say.Red("Error", "This person isn't worker");
                        }
                        else if (option == 3)
                        {
                            Console.Write(" Name: ");
                            string name = Console.ReadLine();
                            Account a = XmlMethods.FindInXML(name, AccountsPath);
                            if (a.Role == Roles.Worker)
                            {
                                XmlMethods.RemoveFromXML(a, AccountsPath);
                                a.Role = Roles.User;
                                a.Serialize(AccountsPath);
                            }
                            else
                                Say.Red("Error", "This person isn't worker");
                        }
                        else if (option == 4)
                        {
                            AccountOptions();
                        }
                        else if (option == 5)
                        {
                            Auth.Save();
                            break;
                        }

                    }
                }
                catch (Exception ex)
                {
                    Say.Red("Error", $"Message: {ex.Message}");
                }
            }
        }
    }
}
