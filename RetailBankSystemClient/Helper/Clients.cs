using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;

namespace RetailBankSystemClient.Helper
{
	public class Clients
	{
		HttpClient client;
		public HttpClient GetAuthAddress()
		{
			client = new HttpClient();
			client.BaseAddress = new Uri("https://localhost:44359");
			return client;
		}
		public HttpClient getAccountService()
		{
			//https ://localhost:44338
			client = new HttpClient();
			client.BaseAddress = new Uri("https://localhost:44338");
			return client;
		}
		public HttpClient getTransactionService()
		{
			client = new HttpClient();

			//https ://localhost:44397
			client.BaseAddress = new Uri("https://localhost:44397");
			return client;
		}
		public HttpClient getCustomerService()
		{
			//https ://localhost:44398/
			client = new HttpClient();
			client.BaseAddress = new Uri("https://localhost:44398");
			return client;
		}

	}
}
