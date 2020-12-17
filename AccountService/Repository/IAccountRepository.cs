using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AccountService.Models;
using AccountService.Models.ViewModel;

namespace AccountService.Repository
{
    public interface IAccountRepository
    {
        AccountCreationStatus createAccount(int CustomerId, string AccountType);
        IEnumerable<Account> getCustomerAccounts(int CustomerId);
        Account getAccount(int AccountId);
        IEnumerable<Statement> getAccountStatement(int AccountId, DateTime from_date, DateTime to_date);
        TransactionStatus deposit(int AccountId, int amount);
        TransactionStatus withdraw(int AccountId, int amount);
        IEnumerable<Account> getAllCustomerAccounts();
    }
}
