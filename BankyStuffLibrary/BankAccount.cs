using System;
using System.Collections.Generic;
using System.Text;

namespace BankyStuff
{
    public class BankAccount
    {
        public string Number { get; }
        public string Owner { get; set; }
        public decimal Balance {
            get
            {
                decimal balance = 0;
                foreach (var item in allTransactions)
                {
                    balance += item.Amount;
                }

                return balance;
            }
        }

        private static int accountNumberSeed = 1234567890;

        private List<Transaction> allTransactions = new List<Transaction>();

        public BankAccount(string name, decimal InitialBalance)
        {
            Number = accountNumberSeed.ToString();
            accountNumberSeed++;

            Owner = name;

            //Balance = InitialBalance;  // this bank giving giving free many to people :D
            MakeDeposit(InitialBalance, DateTime.Now, "Initial balance");
        }

        public void MakeDeposit(decimal amount, DateTime date, string note)
        {
            // Gain money

            // it is just a protection
            if(amount <= 0)
                throw new ArgumentOutOfRangeException(nameof(amount), "Amount of deposit must be positive");

            // main process is here
            var deposit = new Transaction(amount, date, note);
            allTransactions.Add(deposit);
        }

        public void MakeWithdrawal(decimal amount, DateTime date, string note)
        {
            // lose money

            // it is just a protection
            if (amount < 0)
                throw new ArgumentOutOfRangeException(nameof(amount), "Amount of withdrawal must be positive");
            if (Balance - amount < 0)
                throw new InvalidOperationException("Not sufficient funds for this withdrawal");

            // main process is here
            var withDrawal = new Transaction(-amount, date, note);
            allTransactions.Add(withDrawal);
        }

        public string GetAccountHistory()
        {
            var report = new StringBuilder();
            report.AppendLine("Date\t\tAmount\tBalance\t\tNote");

            var balance = 0M;
            foreach (var item in allTransactions)
            {
                // ROWS
                balance += item.Amount;  // I want to see total money each time.
                //report.AppendLine($"{item.Date.ToShortDateString()}\t{item.Amount}\t{balance}\t{item.Notes}");
                report.AppendLine($"{item.Date.ToShortDateString()}\t{item.AmountForHumans}\t{balance}\t{item.Notes}");

            }
            return report.ToString();
        }
    }

    public static class BankAccountTest
    {
        public static void BankAccountMain()
        {
            var account = new BankAccount("Mustafa", 10_000);
            Console.WriteLine(@$"Account {account.Number} was created for {account.Owner} with {account.Balance} initial balance");

            // buy something from internet
            account.MakeWithdrawal(120, DateTime.Now, "microphone");
            Console.WriteLine(account.Balance);

            // sell something
            account.MakeDeposit(30, DateTime.Now, "burger");
            Console.WriteLine(account.Balance);


            Console.WriteLine($"\n\n{account.GetAccountHistory()}");


            /*
            // Protections ;)
            // negative initial balance
            try
            {
                var invalidAccount = new BankAccount("invalid", -55);
            }
            catch (ArgumentOutOfRangeException e)
            {
                Console.WriteLine("Exception caught creating account with negative balance");
                Console.WriteLine(e.ToString());
            }



            // overdraw
            // Test for a negative balance.
            try
            {
                account.MakeWithdrawal(75000000, DateTime.Now, "Attempt to overdraw");
            }
            catch (InvalidOperationException e)
            {
                Console.WriteLine("Exception caught trying to overdraw");
                Console.WriteLine(e.ToString());
            }
            */
        }
    }
}

