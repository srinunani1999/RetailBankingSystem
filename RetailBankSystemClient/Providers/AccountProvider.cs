using RetailBankSystemClient.Repositories;
using RetailBankSystemClient.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;

namespace RetailBankSystemClient.Providers
{
    public class AccountProvider : IAccountProvider
    {
        private readonly IAccountRepo _accountRepo;
        public AccountProvider(IAccountRepo accountRepo)
        {
            _accountRepo = accountRepo;
        }
        public Task<HttpResponseMessage> getAccount(int AccountId)
        {
            return _accountRepo.getAccount(AccountId);
           
        }

        public Task<HttpResponseMessage> GetAccountStatement(AccountStatementViewModel model)
        {
            return _accountRepo.GetAccountStatement(model);
        }

        public Task<HttpResponseMessage> getCustomerAccounts(int customerId)
        {
            return _accountRepo.getCustomerAccounts(customerId);
        }
    }
}
