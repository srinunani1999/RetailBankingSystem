using Newtonsoft.Json;
using RetailBankSystemClient.Helper;
using RetailBankSystemClient.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;

namespace RetailBankSystemClient.Providers
{
    public class TransactionProvider : ITransactionProvider
    {
        Clients clientAddress = new Clients();

        public async Task<HttpResponseMessage> Deposit(DepositViewModel model)
        {
            using (HttpClient client = clientAddress.getTransactionService())
            {
                var contentType = new MediaTypeWithQualityHeaderValue("application/json");
                client.DefaultRequestHeaders.Accept.Add(contentType);

                StringContent content = new StringContent(JsonConvert.SerializeObject(model), Encoding.UTF8, "application/json");
              
                var response = await client.PostAsync("api/Transaction/deposit", content);
                return response;
            }
           
        }

        public async Task<HttpResponseMessage> GetTransactions(int CustomerId)
        {
            using (HttpClient client = clientAddress.getTransactionService())
            {
                client.DefaultRequestHeaders.Accept.Clear();
                client.DefaultRequestHeaders.Accept.Add(new System.Net.Http.Headers.MediaTypeWithQualityHeaderValue("application/Json"));
                var response = await client.GetAsync("api/Transaction/getTransactions/" + CustomerId);
                return response;
            }
        }

        public async Task<HttpResponseMessage> Transfer(TransferViewModel model)
        {
            using (HttpClient client = clientAddress.getTransactionService())
            {
                var contentType = new MediaTypeWithQualityHeaderValue("application/json");
                client.DefaultRequestHeaders.Accept.Add(contentType);

                StringContent content = new StringContent(JsonConvert.SerializeObject(model), Encoding.UTF8, "application/json");

                var response = await client.PostAsync("api/Transaction/transfer",content);
                return response;
            }
        }

        public async Task<HttpResponseMessage> Withdraw(WithdrawViewModel model)
        {
            using (HttpClient client = clientAddress.getTransactionService())
            {
                var contentType = new MediaTypeWithQualityHeaderValue("application/json");
                client.DefaultRequestHeaders.Accept.Add(contentType);

                StringContent content = new StringContent(JsonConvert.SerializeObject(model), Encoding.UTF8, "application/json");

                var response = await client.PostAsync("api/Transaction/withdraw", content);
                return response;
            }
        }
    }
}
