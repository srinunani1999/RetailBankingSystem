using RulesService.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
//using System.Transactions;

namespace RulesService.Repository
{
    public interface IRulesRepository
    {
        public List<Account> getAccounts();
        public RuleStatus  evaluateMinBal(int balance, int AccountId);
        public float getServiceCharges(string AccountType);
        TransactionStatus ApplyServiceCharge(int AccountID, int Amount);
        public void runMontlyJob();

    }
}
