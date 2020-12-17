using CustomerServices.Models;
using CustomerServices.Repositary;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;

namespace CustomerServices.Provider
{
    public class CustomerProvider : IProvider
    {
        static readonly log4net.ILog _log4net = log4net.LogManager.GetLogger(typeof(CustomerProvider));
        private ICustomerRepositary _repo;
        public CustomerProvider(ICustomerRepositary repo) 
        {
            this._repo = repo;
        }
        public bool CreateCustomer(Customer Model)
        {
            var customer = _repo.GetCustomerdetails(Model.CustomerId);
            if (customer == null)
            {
                if (_repo.CreateCustomer(Model))
                {
                    _log4net.Info("Customer Id created");
                    var client = new HttpClient();
                    client.BaseAddress = new Uri("https://localhost:44338");
                    HttpResponseMessage rep = client.PostAsJsonAsync("api/Account/createAccount", new { CustomerId = Convert.ToInt32(Model.CustomerId) }).Result;
                    if (rep.StatusCode == System.Net.HttpStatusCode.InternalServerError)
                    {
                        throw new NullReferenceException();
                    }
                    var result = rep.Content.ReadAsStringAsync().Result;
                    AccountCreationStatus status = JsonConvert.DeserializeObject<AccountCreationStatus>(result);
                    return true;
                }
            }
            else
            {
                _log4net.Warn("Customer already exist :" + Model.CustomerId);
            }
            return false;
        }


        public IEnumerable<Customer> GetAll()
        {
            try
            {
                var CustomerList = _repo.GetAll().ToList();
                if (CustomerList.Count == 0)
                {
                    _log4net.Info("Empty List");
                    throw new System.ArgumentNullException("Empty List");
                }
                else
                {
                    return CustomerList;
                }
            }
            catch (Exception e)
            {
                _log4net.Error("Error in GetAll");
                throw e;
            }
        }

        public Customer GetCustomerdetails(int id)
        {
            try
            {
                _log4net.Info("Customer Retrived   Successfully");
                return _repo.GetCustomerdetails(id);
            }
            catch (Exception e)
            {
                _log4net.Error("Error  in getting details");
                throw e;
            }
        }
    }
}
