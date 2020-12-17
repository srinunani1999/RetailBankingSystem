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

namespace RetailBankSystemClient.Repositories
{
	public class AccountRepo : IAccountRepo
	{
	
		Clients clientAddress = new Clients();

		public async Task<HttpResponseMessage> getCustomerAccounts(int customerId)
		{
			using (HttpClient client = clientAddress.getAccountService())
			{
				client.DefaultRequestHeaders.Accept.Clear();
				client.DefaultRequestHeaders.Accept.Add(new System.Net.Http.Headers.MediaTypeWithQualityHeaderValue("application/Json"));
				var response = await client.GetAsync("api/Account/getCustomerAccounts/" + customerId);
				return response;
			}
		}

	
		public async Task<HttpResponseMessage> getAccount(int AccountId)
		{
			using (HttpClient client = clientAddress.getAccountService())
			{
				client.DefaultRequestHeaders.Accept.Clear();
				client.DefaultRequestHeaders.Accept.Add(new System.Net.Http.Headers.MediaTypeWithQualityHeaderValue("application/Json"));
				var response = await client.GetAsync("api/Account/getAccount/" + AccountId);
				return response;
			}
		}

		public async Task<HttpResponseMessage> GetAccountStatement(AccountStatementViewModel model)
		{
			using (HttpClient client = clientAddress.getAccountService())
			{
				var contentType = new MediaTypeWithQualityHeaderValue("application/json");
				client.DefaultRequestHeaders.Accept.Add(contentType);
				
				StringContent content = new StringContent(JsonConvert.SerializeObject(model), Encoding.UTF8, "application/json");
				var response =await client.PostAsync("api/Account/getAccountStatement", content);
				//var response = await client.PostAsJsonAsync("api/Account/getAccountStatement", new { AccountId = model.Id, from_date = model.StartDate, to_date = model.EndDate });
				return response;
			}
		}
	}
}
