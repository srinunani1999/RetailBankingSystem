using RetailBankSystemClient.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;

namespace RetailBankSystemClient.Providers
{
    public interface ITransactionProvider
    {
        Task<HttpResponseMessage> Withdraw(WithdrawViewModel model);
        Task<HttpResponseMessage> Deposit(DepositViewModel model);
        Task<HttpResponseMessage> Transfer(TransferViewModel model);
        Task<HttpResponseMessage> GetTransactions(int CustomerId);

    }
}
