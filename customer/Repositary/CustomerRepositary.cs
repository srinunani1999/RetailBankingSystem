using CustomerServices.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CustomerServices.Repositary
{
    public class CustomerRepositary : ICustomerRepositary
    {
        static int id = 4;

        static readonly log4net.ILog _log4net = log4net.LogManager.GetLogger(typeof(CustomerRepositary));
        public static List<Customer> ListOfCustomers = new List<Customer>()
        {
            new Customer{CustomerId=1,Name="Nikhitha",Address="Guntur, AP", DateOfBirth=new DateTime(1999,5,12),PanNo="DLRKQ5423E"},
            new Customer{CustomerId=2,Name="Priyanka",Address="Vizag, AP", DateOfBirth=new DateTime(1999,5,13),PanNo="DLRUI5445P"},
            new Customer{CustomerId=3,Name="Hema Srinivas",Address="Vizag, AP", DateOfBirth=new DateTime(1999,5,14),PanNo="DLRSF9423L"},
            new Customer{CustomerId=4,Name="Manisha",Address="Vizianagaram, AP", DateOfBirth=new DateTime(1999,5,15),PanNo="DLRAZ5483K"}

        };
        public bool CreateCustomer(Customer Model)
        {
            
            try
            {
                id++;
                Model.CustomerId = id;
                ListOfCustomers.Add(Model);
                _log4net.Info("Customer entered  Successfully");
                return true;
            }
            catch (Exception e)
            {
                _log4net.Error("Error" + e.Message);
            }
            return false;
        }

        public IEnumerable<Customer> GetAll()
        {
            _log4net.Info("Customer details recieved  Successfully");
            return ListOfCustomers.ToList();
        }

        public Customer GetCustomerdetails(int id)
        {
            try
            {
                _log4net.Info("Customer details retrived bys passing Id");
                var obj = ListOfCustomers.Find(a => a.CustomerId == id);
                return obj;
            }
            catch (Exception e)
            {
                _log4net.Error("Error" + e.Message);
                throw e;
            }
        }
    }
}
