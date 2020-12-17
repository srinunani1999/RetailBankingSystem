using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TransactionService.Models;

namespace TransactionService.Providers
{
    public interface IProvider
    {
        bool addToTransactionHistory(TransactionStatus status, Account account);
        List<TransactionHistory> getTransactions(int CustomerId);
        Account getAccount(int AccountId);
        TransactionStatus deposit(int AccountId, int amount);
        TransactionStatus withdraw(int AccountId, int amount);
          RuleStatus rulesStatus(int AccountId, int amount, Account account);
        TransactionStatus transfer(int Source_AccountId, int Target_AccountId, int amount);
    }
}
