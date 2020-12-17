using CustomerServices.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CustomerServices.Repositary
{
   public  interface ICustomerRepositary
    {
        public bool CreateCustomer(Customer Model);
        public Customer GetCustomerdetails(int id);
        public IEnumerable<Customer> GetAll();
    }
}
