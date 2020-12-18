using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AccountService.Models;
using AccountService.Models.ViewModel;

namespace AccountService.Repository
{
    public class AccountRepository : IAccountRepository
    {
        public static DateTime d;
        public static int refno = 47;
        readonly log4net.ILog _log4net = log4net.LogManager.GetLogger(typeof(AccountRepository));

        public static List<Account> accounts = new List<Account>() {
            new Account(){AccountId=1,CustomerId=1,Balance=1000,AccountType="Savings",minBalance=1000 },
            new Account(){AccountId=2,CustomerId=1,Balance=1000,AccountType="Current",minBalance=0 },
            new Account(){AccountId=3,CustomerId=2,Balance=1000,AccountType="Savings",minBalance=1000 },
            new Account(){AccountId=4,CustomerId=2,Balance=1000,AccountType="Current",minBalance=0 },
            new Account(){AccountId=5,CustomerId=3,Balance=500,AccountType="Savings",minBalance=1000 }
        };

        public static List<Statement> statements = new List<Statement>() {
       new Statement(){ StatementId=1,AccountId=1,date=Convert.ToDateTime("2020-09-3T15:05:15"),refno="Ref44",ValueDate=Convert.ToDateTime("2020-09-3T15:05:15"),Withdrawal=0,Deposit=200,ClosingBalance=1200},
       new Statement(){ StatementId=2,AccountId=1,date=Convert.ToDateTime("2020-10-7T03:00:16"),refno="Ref45",ValueDate=Convert.ToDateTime("2020-10-7T03:00:16"),Withdrawal=100,Deposit=0,ClosingBalance=1100},
       new Statement(){ StatementId=3,AccountId=2,date=Convert.ToDateTime("2020-11-5T10:46:17"),refno="Ref46",ValueDate=Convert.ToDateTime("2020-11-5T10:46:17"),Withdrawal=0,Deposit=600,ClosingBalance=1600},
       new Statement(){ StatementId=4,AccountId=2,date=Convert.ToDateTime("2020-12-2T13:00:25"),refno="Ref47",ValueDate=Convert.ToDateTime("2020-12-2T13:00:25"),Withdrawal=200,Deposit=0,ClosingBalance=1400}
         };


        
        public AccountCreationStatus createAccount(int CustomerId, string AccountType)
        {
            Account acc = new Account();


            acc.AccountId = accounts.Count + 1;
            acc.CustomerId = CustomerId;
            acc.Balance = 1000;
            acc.AccountType = AccountType;
            if (AccountType == "Current")
            {
                acc.minBalance = 0;
            }
            else
            {
                acc.minBalance = 1000;
            }

            accounts.Add(acc);

            return new AccountCreationStatus() { Message = "Account has been created successfully", AccountId = acc.AccountId };
        }


        public IEnumerable<Account> getCustomerAccounts(int CustomerId)
        {

            List<Account> Accounts = new List<Account>();
            try
            {
                foreach (var item in accounts)
                {
                    if (item.CustomerId == CustomerId)
                    {
                        Accounts.Add(item);
                    }
                }
                if (Accounts.Count == 0)
                {
                    throw new System.ArgumentNullException("No Accounts for this customer with id : " + CustomerId);
                }
            }
            catch (Exception e)
            {
                _log4net.Error(e.Message);
                throw e;
            }

            return Accounts;
        }



        

        public Account getAccount(int AccountId)
        {

            Account acc = new Account();
            try
            {
                foreach (var item in accounts)
                {
                    if (item.AccountId == AccountId)
                    {

                        acc = item;
                    }
                }

                if (acc.AccountId == 0)
                {
                    throw new System.ArgumentNullException("No account with this id : "+AccountId);
                }
            }
            catch (Exception e)
            {
                _log4net.Error(e.Message);
                throw e;
            }

            return acc;
        }


        
        public IEnumerable<Statement> getAccountStatement(int AccountId, DateTime from_date, DateTime to_date)
        {
            List<Statement> Statements = new List<Statement>();
            try
            {
                DateTime tempDate = Convert.ToDateTime("1/1/0001 12:00:00 AM");

                if (from_date == tempDate)
                {


                    DateTime to = DateTime.Now;
                    int month = DateTime.Now.Month, year = DateTime.Now.Year;
                    string ds = Convert.ToString(month) + "/", ms = "1/", ys = Convert.ToString(year), ts = " 12:00:00 AM", fs = ds + ms + ys + ts;
                    DateTime from = Convert.ToDateTime(fs);
                    foreach (var item in statements)
                    {
                        if (item.AccountId == AccountId && item.date >= from && item.date <= to)
                        {
                            Statements.Add(item);
                        }
                    }
                }
                else
                {
                    foreach (var item in statements)
                    {
                        if (item.AccountId == AccountId && item.date >= from_date && item.date <= to_date)
                        {
                            Statements.Add(item);
                        }
                    }

                }
                if (Statements.Count == 0)
                {
                    throw new System.ArgumentNullException("No statement for account id : " + AccountId);
                }
            }
            catch (Exception e)
            {
                _log4net.Info(e.Message);
                throw e;
            }

            return Statements;
        }


        public TransactionStatus deposit(int AccountId, int amount)
        {
            int trans = 0;
            int sourceBal = 0, destinationBal = 0;
            try
            {
                foreach (var item in accounts)
                {
                    if (item.AccountId == AccountId)
                    {
                        trans = 1;
                        sourceBal = item.Balance;
                        item.Balance = item.Balance + amount;
                        destinationBal = item.Balance;

                        Statement s = new Statement();

                        s.StatementId = statements.Count + 1;
                        s.AccountId = AccountId;
                        d = DateTime.Now;
                        s.date = d;

                        refno = refno + 1;
                        s.refno = "Ref" + refno;
                        s.ValueDate = d;
                        s.Withdrawal = 0;
                        s.Deposit = amount;
                        s.ClosingBalance = destinationBal;
                        statements.Add(s);

                        d = s.date;
                        break;
                    }
                }

                if (trans == 1)
                {
                    return new TransactionStatus() { message = "Your account has been credited", source_balance = sourceBal, destination_balance = destinationBal };
                }
                else
                {
                    throw new System.ArgumentNullException("Account id : " + AccountId + " is invalid.");

                }
            }
            catch (Exception e)
            {
                _log4net.Error(e.Message);
                throw e;
            }


        }



        public TransactionStatus withdraw(int AccountId, int amount)
        {

            int trans = 0;
            int sourceBal = 0, destinationBal = 0;
            try
            {
                foreach (var item in accounts)
                {
                    if (item.AccountId == AccountId)
                    {
                        trans = 1;
                        sourceBal = item.Balance;
                        item.Balance = item.Balance - amount;
                        destinationBal = item.Balance;

                        Statement s = new Statement();

                        s.StatementId = statements.Count + 1;
                        s.AccountId = AccountId;
                        d = DateTime.Now;
                        s.date = d;
                        refno = refno + 1;
                        s.refno = "Ref" + refno;
                        s.ValueDate = d;
                        s.Withdrawal = amount;
                        s.Deposit = 0;
                        s.ClosingBalance = destinationBal;
                        statements.Add(s);

                        d = s.date;
                        break;
                    }
                }

                if (trans == 1)
                {
                    return new TransactionStatus() { message = "Your account has been debited", source_balance = sourceBal, destination_balance = destinationBal };

                }
                else
                {

                    throw new System.ArgumentNullException("Account id : " + AccountId + " is invalid.");
                }
            }

            catch (Exception e)
            {
                _log4net.Error(e.Message);
                throw e;
            }
        }


        public IEnumerable<Account> getAllCustomerAccounts()
        {
            return accounts;
        }

    }
}
