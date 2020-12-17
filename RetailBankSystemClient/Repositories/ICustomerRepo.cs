using RetailBankSystemClient.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;

namespace RetailBankSystemClient.Repositories
{
    public interface ICustomerRepo
    {
        void CreateCustomer(CustomerDbo model);
        Task<HttpResponseMessage> GetCustomersById(int id);
        Task<HttpResponseMessage> GetAllCustomers();
    }
}
