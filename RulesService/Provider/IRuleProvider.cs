using RulesService.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RulesService.Provider
{
    public interface IRuleProvider
    {
        public RuleStatus evaluateMinBalance(int balance, int AccountId);
        public void RunMonthlyJob();
        public float getServiceCharge(string AccountType);
        public List<Account> getAccounts();
       // public int getMinBalance(int AccountId);

    }
}
