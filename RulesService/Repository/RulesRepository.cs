using Newtonsoft.Json;
using RulesService.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;

namespace RulesService.Repository
{
    public class RulesRepository : IRulesRepository
    {

        static readonly log4net.ILog _log4net = log4net.LogManager.GetLogger(typeof(RulesRepository));

        private Client _client;
        

        public TransactionStatus ApplyServiceCharge(int AccountID, int Amount)
        {
            try
            {
                _client = new Client();
                HttpClient client = _client.AccountClient();
                dynamic model = new { AccountId = AccountID, amount = Amount };
                var jsonString = JsonConvert.SerializeObject(model);
                var obj = new StringContent(jsonString, System.Text.Encoding.UTF8, "application/json");
                HttpResponseMessage response = client.PostAsync("api/Account/withdraw", obj).Result;
                if (response.StatusCode == System.Net.HttpStatusCode.InternalServerError)
                {
                    throw new NullReferenceException("Issue in connecting with Transaction API");
                }
                var result = response.Content.ReadAsStringAsync().Result;
                TransactionStatus status = JsonConvert.DeserializeObject<TransactionStatus>(result);

                return status;
            }
            catch (NullReferenceException e)
            {
                _log4net.Error(e.Message);
                throw e;
            }
            catch (Exception e)
            {
                _log4net.Error("Exception thrown while withdrawing Charge from transaction API");
                throw e;
            }
        }

        public RuleStatus evaluateMinBal(int balance, int AccountId)
        {

            try
            {
                _client = new Client();
                HttpClient client = _client.AccountClient();
                HttpResponseMessage response = client.GetAsync("api/Account/getAccount/" + AccountId).Result;
                if (response.StatusCode == System.Net.HttpStatusCode.InternalServerError)
                {
                    throw new NullReferenceException("issue in connecting with Account API");
                }
                var result = response.Content.ReadAsStringAsync().Result;
                Account account = JsonConvert.DeserializeObject<Account>(result);

                if (balance >= account.minBalance)
                    return new RuleStatus { Status = "allowed" };
                else if (balance < account.minBalance)
                    return new RuleStatus { Status = "denied" };
                else
                    return new RuleStatus { Status = "NA" };
            }
            catch (NullReferenceException e)
            {
                _log4net.Error("The account is not returned from Account API.", e);
                throw e;
            }
            catch (Exception e)
            {
               _log4net.Error("Exception occured while getting Account by ID");
                throw e;
            }
        }
        public List<Account> getAccounts()
        {

            try
            {
                List<Account> acc = null;
                _client = new Client();

                HttpClient client = _client.AccountClient();
                HttpResponseMessage response = client.GetAsync("api/Account/getAllCustomerAccounts").Result;
                if (response.StatusCode == System.Net.HttpStatusCode.InternalServerError)
                {
                    throw new NullReferenceException("Issue in getting accounts from Account API");
                }
                var result = response.Content.ReadAsStringAsync().Result;
                acc = JsonConvert.DeserializeObject<List<Account>>(result);

                return acc;
            }
            catch (NullReferenceException e)
            {
                _log4net.Error(e.Message);
                throw e;
            }
            catch (Exception e)
            {
                _log4net.Error("Exception in getting accounts from Account API");
                throw e;
            }

        }
        public float getServiceCharges(string AccountType)
        {
            if (String.Equals(AccountType, "Savings"))
            {
                return 100;
            }
            else if (String.Equals(AccountType, "Current"))
            {
                return 200;
            }
            else
            {
                return 0;
            }
        }
        public void runMontlyJob()
        {
            List<Account> AllAcc = getAccounts();
            foreach (var x in AllAcc)
            {
                if (x.Balance < x.minBalance)
                {
                    float ServiceCharge = getServiceCharges(x.AccountType);
                    var status = ApplyServiceCharge(x.AccountId, (int)ServiceCharge);
                    if (status.Message == "Your account has been debited")
                    {
                        _log4net.Info("Service charge deducted for the AccountID = " + x.AccountId);
                    }
                    else
                    {
                        _log4net.Info("Some Issue occured while deducting service charge for the AccountID = " + x.AccountId);
                    }
                }
            }
        }

    }



        
}

