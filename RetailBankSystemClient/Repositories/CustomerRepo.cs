using Newtonsoft.Json;
using RetailBankSystemClient.Helper;
using RetailBankSystemClient.Models;
using RetailBankSystemClient.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;

namespace RetailBankSystemClient.Repositories
{
	public class CustomerRepo : ICustomerRepo
	{
		private readonly MFPEDataBase _context;
		Clients clients = new Clients();
		public CustomerRepo(MFPEDataBase context)
		{
			this._context = context;
		}
		public void CreateCustomer(CustomerDbo model)
		{
			 _context.Customer.Add(model);
			int res= _context.SaveChanges();
			//return result;
		}

		public async Task<HttpResponseMessage> GetAllCustomers()
		{
			using (HttpClient client = clients.getCustomerService())
			{
				
				var response = await client.GetAsync("api/Customer/Get");
				var res = await response.Content.ReadAsStringAsync();
				var customers = JsonConvert.DeserializeObject<List<Customer>>(res);


				return response;
				
				
			}
		}

		public async Task<HttpResponseMessage> GetCustomersById(int id)
		{
			using (HttpClient client = clients.getCustomerService())
			{

				var response = await client.GetAsync("api/Customer/GetbyId/"+id);

				return response;


			}
		}

		//Task<int> ICustomerRepo.CreateCustomer(CustomerDbo model)
		//{
		//	throw new NotImplementedException();
		//}
	}
}
