using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using TransactionService.Models;
using TransactionService.Repositories;
using TransactionService.ClientAddress;
using System.Net.Http.Headers;
using Newtonsoft.Json;
using Microsoft.AspNetCore.Mvc;
using System.Text;

namespace TransactionService.Providers
{
    public class TransactionProvider : IProvider
    {
        static readonly log4net.ILog _log4net = log4net.LogManager.GetLogger(typeof(TransactionProvider));
        private ITransactionRepo _repo;
        public TransactionProvider(ITransactionRepo repo)
        {
            _repo = repo;
        }
        public bool addToTransactionHistory(TransactionStatus status, Account account)
        {
            try
            {
                bool output = _repo.addToTransactionHistory(status, account);

                if (output == false)
                {
                    throw new System.ArgumentNullException("Exception occured while adding to transaction history for " + account.AccountId);
                }
            }
           
            catch (Exception e)
            {
                _log4net.Error(e.Message + ": In Provider method");
                throw e;
            }

            return true;
        }

        public TransactionStatus deposit(int AccountId, int amount)
        {
            TransactionStatus status = new TransactionStatus();
            try
            {
                Clients obj = new Clients();
                HttpClient accountClient = obj.AccountDetails();
                //var contentType = new MediaTypeWithQualityHeaderValue("application/json");
                //accountClient.DefaultRequestHeaders.Accept.Add(contentType);
                // var res= accountClient.PostAsJsonAsync("api/Account/deposit", new { AccountId = AccountId, amount = amount }).Result;
                StringContent content = new StringContent(JsonConvert.SerializeObject(new { AccountId = AccountId, amount = amount }), Encoding.UTF8, "application/json");
                HttpResponseMessage response = accountClient.PostAsync("api/Account/deposit", content).Result;
                if (response.StatusCode == System.Net.HttpStatusCode.InternalServerError)
                {
                    throw new ArgumentNullException("Error in calling Account API to deposit money");
                }
                var result = response.Content.ReadAsStringAsync().Result;
                status = JsonConvert.DeserializeObject<TransactionStatus>(result);
            }
            catch (ArgumentNullException e)
            {
                _log4net.Error(e.Message);
                throw e;
            }
            catch (Exception e)
            {
                _log4net.Error("Not able to deposit in account with account id " + AccountId + " and amount " + amount);
                throw e;
            }

            return status;
        }

        public Account getAccount(int AccountId)
        {
            try
            {
                Clients client = new Clients();
                HttpClient accountClient = client.AccountDetails();
                var contentType = new MediaTypeWithQualityHeaderValue("application/json");
                accountClient.DefaultRequestHeaders.Accept.Add(contentType);
                var response = accountClient.GetAsync("api/Account/getAccount/" + AccountId).Result;
                if (!response.IsSuccessStatusCode)
                {

                    throw new ArgumentNullException("Unable to fetch the Account from  Account API ");
                }
                var result = response.Content.ReadAsStringAsync().Result;
                var account = JsonConvert.DeserializeObject<Account>(result);
                return account;

            }
            catch (ArgumentNullException e)
            {
                _log4net.Error(e.Message);
                throw e;
            }
            catch (Exception e)
            {
                _log4net.Error("Exception occured while getting Account details for account id-> " + AccountId);
                throw e;
            }
        }

        //public TransactionStatus deposit(int AccountId, int amount)
        //{
        //    try
        //    {
        //        var deposit = _repo.deposit(AccountId, amount);
        //        return deposit;

        //    }
        //    catch (ArgumentNullException e)
        //    {
        //        _log4net.Error(e.Message);
        //        throw e;
        //    }
        //    catch (Exception e)
        //    {
        //        _log4net.Error(e.Message + ": In Provider method");
        //        throw e;
        //    }



        //}

        //public Account getAccount(int AccountId)
        //{
        //    try
        //    {
        //        var account= _repo.getAccount(AccountId);
        //        return account;

        //    }
        //    catch (ArgumentNullException e)
        //    {
        //        _log4net.Error(e.Message);
        //        throw e;
        //    }
        //    catch (Exception e)
        //    {
        //        _log4net.Error("Exception occured while getting Account details for account id-> " + AccountId);
        //        throw e;
        //    }

        //}

        public List<TransactionHistory> getTransactions(int CustomerId)
        {
            try
            {
               var TransactionHistory=  _repo.getTransactions(CustomerId);
                return TransactionHistory;
               
            }
            catch (Exception e)
            {

                throw new ArgumentNullException("Exception occured while getting transactions list for customer "+CustomerId);
            }
        }

        public RuleStatus rulesStatus(int AccountId, int amount, Account account)
        {
            try
            {
                Clients ruleClient = new Clients();
                HttpClient client = ruleClient.RuleApi();

                int balance = account.Balance - amount;
                HttpResponseMessage response = client.GetAsync("api/Rules/EvaluateMinBal/" + AccountId + "/" + balance).Result;
                if (response.StatusCode == System.Net.HttpStatusCode.InternalServerError)
                {
                    return new RuleStatus { status = "denied" };
                }
                var result = response.Content.ReadAsStringAsync().Result;
                RuleStatus rulestatus = JsonConvert.DeserializeObject<RuleStatus>(result);
                return rulestatus;
            }
            catch (Exception e)
            {
                _log4net.Error("Insufficient Balance for account Id: " + AccountId);
                throw e;
            }
        }

        public TransactionStatus transfer(int Source_AccountId, int Target_AccountId, int amount)
        {
            try
            {
                TransactionStatus transferstatus = new TransactionStatus();
                //if (Source_AccountId < 0 || Target_AccountId < 0 || amount < 0)
                //{
                //    _log4net.Error("invalid parameters");
                //    return new TransactionStatus() { message = "Transfer Not Allowed" };
                //}
                Account account = getAccount(Source_AccountId);
                RuleStatus ruleStatus = rulesStatus(Source_AccountId, amount, account);
                if (ruleStatus.status == "allowed")
                {
                    TransactionStatus status = withdraw(Source_AccountId, amount);
                    if (status.message == null)
                    {
                        return new TransactionStatus() { message = "Unable to withdraw from source id" + Source_AccountId };
                    }
                    addToTransactionHistory(status, account);
                    transferstatus.source_balance = status.destination_balance;


                    Account targetAccount = getAccount(Target_AccountId);
                    TransactionStatus targetStatus = deposit(Target_AccountId, amount);

                    if (status.message == null)
                    {
                        return new TransactionStatus() { message = "Unable to deposit into target account " + Target_AccountId };
                    }
                    addToTransactionHistory(targetStatus, targetAccount);
                    transferstatus.destination_balance = targetStatus.destination_balance;
                    transferstatus.message = "Tranferred " + amount + " from account " + Source_AccountId + " to Account " + Target_AccountId;
                    _log4net.Info("Transfer  from Account Id: " + Source_AccountId + "to Account Id" + Target_AccountId + " Completed Successfully");
                    return transferstatus;

                }
                return new TransactionStatus() { message = "Unable to transfer into target account " + Target_AccountId };






            }
            catch (ArgumentNullException e)
            {
                _log4net.Error(e.Message);
                throw e;
            }
            catch (Exception e)
            {
                _log4net.Error(e.Message);
                throw e;
            }
        }

        public TransactionStatus withdraw(int AccountId, int amount)
        {
            try
            {
                Clients client = new Clients();
                HttpClient AccountClinet = client.AccountDetails();
                var contentType = new MediaTypeWithQualityHeaderValue("application/json");
                AccountClinet.DefaultRequestHeaders.Accept.Add(contentType);
                StringContent content = new StringContent(JsonConvert.SerializeObject(new { AccountId = AccountId, amount = amount }), Encoding.UTF8, "application/json");

                HttpResponseMessage response = AccountClinet.PostAsync("api/Account/withdraw", content).Result;
                var result = response.Content.ReadAsStringAsync().Result;
                TransactionStatus status = JsonConvert.DeserializeObject<TransactionStatus>(result);
                return status;
            }
            catch (Exception e)
            {
                _log4net.Error("unable to withdraw from account with account id " + AccountId + " and amount " + amount);
                throw e;
            }
        }



        //public RuleStatus rulesStatus(int AccountId, int amount, Account account)
        //{
        //    try
        //    {
        //        var status = _repo.rulesStatus(AccountId, amount, account);
        //        return status;
        //    }
        //    catch (Exception e)
        //    {
        //        _log4net.Error("Exception occures while evaluating Minimum balance for account Id: " + AccountId);
        //        throw e;
        //    }


        //}

        //public TransactionStatus transfer(int Source_AccountId, int Target_AccountId, int amount)
        //{
        //    try
        //    {
        //        var transferStatus = _repo.transfer(Source_AccountId,Target_AccountId,amount);
        //        return transferStatus;
        //    }
        //    catch (Exception e)
        //    {
        //        _log4net.Error("unable to tranfer from account with account id " + Source_AccountId + " to target " + Target_AccountId);
        //        throw e;
        //    }
        //}

        //public TransactionStatus withdraw(int AccountId, int amount)
        //{
        //    try
        //    {
        //        var withdrawStatus = _repo.withdraw(AccountId, amount);
        //        return withdrawStatus;
        //    }
        //    catch (Exception e)
        //    {
        //        _log4net.Error("unable to withdraw from account with account id " + AccountId + " and amount " + amount);
        //        throw e;
        //    }

        //}
    }
}
