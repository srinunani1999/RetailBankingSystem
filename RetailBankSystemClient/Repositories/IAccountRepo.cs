using RetailBankSystemClient.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;

namespace RetailBankSystemClient.Repositories
{
    public interface IAccountRepo
    {
        Task<HttpResponseMessage> getCustomerAccounts(int customerId);
        Task<HttpResponseMessage> getAccount(int AccountId);
        Task<HttpResponseMessage> GetAccountStatement(AccountStatementViewModel model);

    }
}
