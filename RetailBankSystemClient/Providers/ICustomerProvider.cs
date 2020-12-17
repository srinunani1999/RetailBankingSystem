using RetailBankSystemClient.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;

namespace RetailBankSystemClient.Providers
{
    public interface ICustomerProvider
    {
        Task<HttpResponseMessage> Createcus(CreateCustomer model);
        Task<HttpResponseMessage> GetCustomerDetails(int id);
        Task<HttpResponseMessage> GetCustomers();
    }
}
