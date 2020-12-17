using Newtonsoft.Json;
using RetailBankSystemClient.Helper;
using RetailBankSystemClient.Models;
using RetailBankSystemClient.Repositories;
using RetailBankSystemClient.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;

namespace RetailBankSystemClient.Providers
{
    public class CustomerProvider : ICustomerProvider
    {
        Clients clientAddress = new Clients();
        private readonly ICustomerRepo _customerRepo;
        public CustomerProvider(ICustomerRepo customerRepo)
        {
            _customerRepo = customerRepo;
        }
        public async Task<HttpResponseMessage> Createcus(CreateCustomer model)
        {

            using (HttpClient clients=clientAddress.getCustomerService())
            {
                var jsonstring = JsonConvert.SerializeObject(model);
                var obj = new StringContent(jsonstring, System.Text.Encoding.UTF8, "application/json");
                var response = await clients.PostAsync("api/Customer/Creation", obj);
                if (response.StatusCode == System.Net.HttpStatusCode.OK)
                {
                    CustomerDbo dbo = new CustomerDbo()
                    {
                        CustomerId = model.CustomerId,
                        Name = model.Name,
                        PANno = model.PANno,
                        Address = model.Address,
                        DOB = model.DateOfBirth
                    };
                     _customerRepo.CreateCustomer(dbo);
                }
                return response;


            }

        }

        public Task<HttpResponseMessage> GetCustomerDetails(int id)
        {
            return _customerRepo.GetCustomersById(id);
        }

        public Task<HttpResponseMessage> GetCustomers()
        {
            return _customerRepo.GetAllCustomers();
        }
    }
}
