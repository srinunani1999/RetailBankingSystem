using NUnit.Framework;
using System;
using System.Collections.Generic;
using AccountService.Controllers;
using AccountService.Models;
using AccountService.Models.ViewModel;
using AccountService.Provider;
using Microsoft.AspNetCore.Mvc;
using Moq;
using AccountNUnitTest.Models;


namespace AccountNUnitTest
{
    [TestFixture]
    public class Tests
    {
        private Mock<IAccountProvider> moqProvider;
        private AccountController controllerObj;
        [SetUp]
        public void Setup()
        {
            moqProvider = new Mock<IAccountProvider>();
            controllerObj = new AccountController(moqProvider.Object);
        }

        [Test]
        public void createAccountSuccessTest()
        {
            moqProvider.Setup(p => p.createAccount(1, "Current")).Returns(new AccountCreationStatus
            {
                Message = "Account has been created successfully",
                AccountId = 2
            });
            var result = controllerObj.createAccount(new Customer { CustomerId = 1 });

            Assert.That(result, Is.InstanceOf<OkObjectResult>());
        }

        [Test]
        public void createAccountFailTest()
        {
            moqProvider.Setup(p => p.createAccount(1, "Savings")).Returns(new AccountCreationStatus
            {
            });

            var result = controllerObj.createAccount(new Customer { CustomerId = 0 });

            Assert.That(result, Is.InstanceOf<NotFoundResult>());
        }

        [Test]
        public void getCutomerAccountsSuccessTest()
        {
            moqProvider.Setup(p => p.getCustomerAccounts(1)).Returns(new List<AccountView>{
                new AccountView(){
                    Id = 1,
                    Balance = 1000,
                },
                new AccountView(){
                    Id = 2,
                    Balance = 1000,
                } 
            });

            var result = controllerObj.getCustomerAccounts(1);

            Assert.That(result, Is.InstanceOf<OkObjectResult>());
        }

        [Test]
        public void getCutomerAccountsFailTest()
        {
            moqProvider.Setup(p => p.getCustomerAccounts(0)).Returns(new List<AccountView>{});

            var result = controllerObj.getCustomerAccounts(0);

            Assert.That(result, Is.InstanceOf<NotFoundResult>());
        }

        [Test]
        public void getAccountSuccessTest()
        {

            moqProvider.Setup(p => p.getAccount(1)).Returns(new Account() {
                AccountId = 1,
                CustomerId = 1,
                Balance = 1000,
                AccountType = "Savings",
                minBalance = 1000
            });

            var result = controllerObj.getAccount(1);

            Assert.That(result, Is.InstanceOf<OkObjectResult>());
        }

        [Test]
        public void getAccountFailTest()
        {
            moqProvider.Setup(p => p.getCustomerAccounts(1)).Returns(new List<AccountView>() { });

            var result = controllerObj.getAccount(0);

            Assert.That(result, Is.InstanceOf<NotFoundResult>());
        }

        [Test]
        public void getAccountStatementSuccessTest()
        {

            moqProvider.Setup(p => p.getAccountStatement(1, DateTime.Now, DateTime.Now)).Returns(new List<Statement>{});

            var result = controllerObj.getAccountStatement(new StatementView() { Id = 1, from_date = DateTime.Now, to_date = DateTime.Now });

            Assert.That(result, Is.InstanceOf<OkObjectResult>());
        }


        [Test]
        public void getAccountStatementFailTest()
        {

            var result = controllerObj.getAccountStatement(new StatementView() { Id = 0, to_date = DateTime.Now, from_date = DateTime.Now });

            Assert.That(result, Is.InstanceOf<NotFoundResult>());
        }

        [Test]
        public void depositSuccessTest()
        {

            moqProvider.Setup(p => p.deposit(1, 200)).Returns(new TransactionStatus()
            {
                message = "Your account has been credited",
                source_balance = 1000,
                destination_balance = 1200
            });

            var result = controllerObj.deposit(new DepositAndWithdraw { AccountId = 1, amount = 200 });

            Assert.That(result, Is.InstanceOf<OkObjectResult>());
        }

        [Test]
        public void depositFailTest()
        {

            moqProvider.Setup(p => p.deposit(1, 200)).Returns(new TransactionStatus(){});
            var result = controllerObj.deposit(new DepositAndWithdraw { AccountId = 0, amount = 200 });

            Assert.That(result, Is.InstanceOf<NotFoundResult>());
        }




        [Test]
        public void withdrawSuccessTest()
        {

            moqProvider.Setup(p => p.withdraw(1, 200)).Returns(new TransactionStatus(){});

            var result = controllerObj.withdraw(new DepositAndWithdraw { AccountId = 1, amount = 200 });

            Assert.That(result, Is.InstanceOf<OkObjectResult>());
        }

        [Test]
        public void withdrawFailTest()
        {

            moqProvider.Setup(p => p.withdraw(1, 200)).Returns(new TransactionStatus()
            {
                message = "Your account has been debited",
                source_balance = 1000,
                destination_balance = 800
            });

            var result = controllerObj.withdraw(new DepositAndWithdraw { AccountId = 0, amount = 200 });

            Assert.That(result, Is.InstanceOf<NotFoundResult>());
        }

    }
}