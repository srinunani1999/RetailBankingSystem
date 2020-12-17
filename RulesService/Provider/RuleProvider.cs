using RulesService.Models;
using RulesService.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RulesService.Provider
{
    public class RuleProvider : IRuleProvider
    {
        static readonly log4net.ILog _log4net = log4net.LogManager.GetLogger(typeof(RuleProvider));
        IRulesRepository _repos;
        public RuleProvider(IRulesRepository _repos)
        {
            this._repos = _repos;
        }
        public RuleStatus evaluateMinBalance(int balance, int AccountId)
        {
            try
            {
                return _repos.evaluateMinBal(balance ,AccountId);
            }
            catch (NullReferenceException e)
            {
                throw e;
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        public List<Account> getAccounts()
        {
            try
            {
                return _repos.getAccounts();
            }
            catch (NullReferenceException e)
            {
                throw e;
            }
            catch (Exception e)
            {
                throw e;
            }

        }

        public float getServiceCharge(string AccountType)
        {
            try
            {
                return _repos.getServiceCharges(AccountType);
            }
            catch (NullReferenceException e)
            {
                throw e;
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        public void RunMonthlyJob()
        {
            try
            {
                _repos.runMontlyJob();
            }
            catch (NullReferenceException e)
            {
                throw e;
            }
            catch (Exception e)
            {
                _log4net.Error("Exception in RunMonthlyJob() in MonthlyJobProvider");
                _log4net.Error(e.Message);
                throw e;
            }
        }
        
    }
}
