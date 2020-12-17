using CustomerServices.Controllers;
using CustomerServices.Models;
using CustomerServices.Provider;
using Microsoft.AspNetCore.Mvc;
using Moq;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Text;

namespace CustomerNUnitTestPoj
{
   public  class CustomerTest
    {
        private Mock<IProvider> _config;
        private CustomerController _controller;
        [SetUp]
        public void Setup()
        {
            _config = new Mock<IProvider>();
            _controller = new CustomerController(_config.Object);

        }
        [Test]
        public void Get_WhenCalled_ReturnsListOfCustomerDetails()
        {

            _config.Setup(repo => repo.GetAll()).Returns(new List<Customer> { new Customer { CustomerId = 1, Name = "Nikhitha", Address = "Guntur, AP", DateOfBirth = new DateTime(1999, 5, 12), PanNo = "DLRKQ5423E" } });

            var result = _controller.Get();
            Assert.That(result, Is.InstanceOf<OkObjectResult>());

        }
        [Test]
        public void Called_When_Given_Valid_CustomerId()
        {
            _config.Setup(p => p.GetCustomerdetails(1)).Returns(new Customer { });


            var result = _controller.GetbyId(1);

            Assert.That(result, Is.InstanceOf<OkObjectResult>());
        }
        [Test]
        public void Called_When_Given_CustomerId_Notinthelist()
        {
            var result = _controller.GetbyId(0);

            Assert.That(result, Is.InstanceOf<NotFoundResult>());
        }
        [Test]
        public void returnValidCreateCustomer()
        {
            Customer customer = new Customer();
            _config.Setup(p => p.CreateCustomer(customer)).Returns(true);
            var result = _controller.Creation(customer);
            Assert.That(result, Is.InstanceOf<OkObjectResult>());
        }
        [Test]
        public void Called_When_CreateCustomer_Is_NULL()
        {

            var result = _controller.Creation(null) as StatusCodeResult;

            Assert.AreEqual(409, result.StatusCode);
        }
    }
}
