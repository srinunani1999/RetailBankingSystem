using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AccountService.Models;
using AccountService.Models.ViewModel;
using AccountService.Repository;

namespace AccountService.Provider
{
    public class AccountProvider : IAccountProvider
    {
        readonly log4net.ILog _log4net = log4net.LogManager.GetLogger(typeof(AccountProvider));
        IAccountRepository _Repository;
        public AccountProvider(IAccountRepository Repository)
        {
            _Repository = Repository;
        }

        
        public AccountCreationStatus createAccount(int CustomerId, string AccountType)
        {
            AccountCreationStatus accountCreationStatus = new AccountCreationStatus();

            accountCreationStatus = _Repository.createAccount(CustomerId, AccountType);
            return accountCreationStatus;
        }


        
        public List<AccountView> getCustomerAccounts(int CustomerId)
        {
            List<Account> accounts = new List<Account>();
            List<AccountView> accountViews = new List<AccountView>();
            try
            {
                accounts = _Repository.getCustomerAccounts(CustomerId).ToList();

                foreach (Account acc in accounts)
                {
                    AccountView model = new AccountView();
                    model.Id = acc.AccountId;
                    model.Balance = acc.Balance;
                    accountViews.Add(model);
                }

            }
            catch (Exception e)
            {
                _log4net.Error(e.Message);
                throw e;
            }

            return accountViews;
        }


        public Account getAccount(int AccountId)
        {
            Account acc = new Account();
            try
            {
                acc = _Repository.getAccount(AccountId);

            }
            catch (Exception e)
            {
                throw e;
            }

            return acc;
        }

        
        public IEnumerable<Statement> getAccountStatement(int AccountId, DateTime from_date, DateTime to_date)
        {
            List<Statement> statements = new List<Statement>();
            try
            {
                statements = _Repository.getAccountStatement(AccountId, from_date, to_date).ToList();

            }
            catch (Exception e)
            {
                _log4net.Error(e.Message);
                throw e;
            }

            return statements;
        }


        
        public TransactionStatus deposit(int AccountId, int amount)
        {
            TransactionStatus transactionStatus = new TransactionStatus();
            try
            {
                transactionStatus = _Repository.deposit(AccountId, amount);

            }

            catch (Exception e)
            {
                _log4net.Error(e.Message);
                throw e;
            }
            return transactionStatus;
        }

        
        public TransactionStatus withdraw(int AccountId, int amount)
        {
            TransactionStatus transactionStatus = new TransactionStatus();
            try
            {
                transactionStatus = _Repository.withdraw(AccountId, amount);
            }
            catch (Exception e)
            {
                _log4net.Error(e.Message);
                throw e;
            }

            return transactionStatus;
        }


        public IEnumerable<Account> getAllCustomerAccounts()
        {
            List<Account> allAccountsList = new List<Account>();
            allAccountsList = _Repository.getAllCustomerAccounts().ToList();
            try
            {
                if (allAccountsList.Count == 0)
                {
                    throw new System.ArgumentNullException("nothing in the list");
                }
            }
            catch (Exception e)
            {
                _log4net.Error(e.Message);
                throw e;
            }

            return allAccountsList;
        }
    }
}
