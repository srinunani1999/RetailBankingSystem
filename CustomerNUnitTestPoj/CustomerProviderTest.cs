using CustomerServices.Models;
using CustomerServices.Provider;
using CustomerServices.Repositary;
using Moq;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;

namespace CustomerNUnitTestPoj
{
    public class CustomerProviderTest
    {
        private Mock<ICustomerRepositary> _repo;
        private CustomerProvider prov;
        [SetUp]
        public void Setup()
        {
            _repo = new Mock<ICustomerRepositary>();
            prov = new CustomerProvider(_repo.Object);

        }

        [Test]
        public void GetCustomer_called_With_CustomerId()
        {
            _repo.Setup(p => p.GetCustomerdetails(1)).Returns(new Customer { });

            var result = prov.GetCustomerdetails(1);

            Assert.That(result, Is.Not.Null);

        }
        [Test]
        public void Get_WhenCalled_ReturnsCustomerList()
        {
            _repo.Setup(r => r.GetAll()).Returns(new List<Customer> { new Customer { CustomerId = 1, Name = "Nikhitha", Address = "Guntur, AP", DateOfBirth = new DateTime(1999, 5, 12), PanNo = "DLRKQ5423E" } });

            var result = prov.GetAll();
            Assert.That(result.Count, Is.EqualTo(1));

        }
        [Test]
        public void GetAll_Called_When_Throws_Exception()
        {
            _repo.Setup(repo => repo.GetAll()).Returns((new List<Customer> { }));
            Assert.That(() => prov.GetAll(), Throws.ArgumentNullException);
        }
    }
}