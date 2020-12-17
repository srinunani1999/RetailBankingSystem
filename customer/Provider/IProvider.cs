using CustomerServices.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CustomerServices.Provider
{
    public interface IProvider
    {
        public bool CreateCustomer(Customer Model);
        public Customer GetCustomerdetails(int id);
        public IEnumerable<Customer> GetAll();
    }
}
