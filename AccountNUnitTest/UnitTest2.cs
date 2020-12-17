using System;
using System.Collections.Generic;
using System.Text;
using AccountService.Provider;
using AccountService.Repository;
using Moq;
using NUnit.Framework;
using AccountService.Models;
using AccountService.Models.ViewModel;
using System.Linq;

namespace AccountNUnitTest
{
    [TestFixture]
    class UnitTest2
    {
        private Mock<IAccountRepository> moqRepository;
        private AccountProvider providerObj;

        [SetUp]
        public void Setup()
        {
            moqRepository = new Mock<IAccountRepository>();
            providerObj = new AccountProvider(moqRepository.Object);
        }

        [Test]
        public void createAccountSuccessTest()
        {
            moqRepository.Setup(p => p.createAccount(1, "Savings")).Returns(new AccountCreationStatus
            {
                Message = "Account has been created successfully",
                AccountId = 1
            });

            var result = providerObj.createAccount(1, "Savings");

            Assert.That(result, Is.Not.Null);
        }

        [Test]
        public void createAccountFailTest()
        {
            moqRepository.Setup(p => p.createAccount(0, "Savings")).Returns(() => null);

            var result = providerObj.createAccount(0, "Savings");

            Assert.That(result, Is.Null);
        }

        [Test]
        public void getCutomerAccountsSuccessTest()
        {
            moqRepository.Setup(p => p.getCustomerAccounts(1)).Returns(new List<Account>{
                new Account(){AccountId=1,CustomerId=1,Balance=1000,AccountType="Savings",minBalance=1000 },
                new Account(){AccountId=2,CustomerId=1,Balance=1000,AccountType="Current",minBalance=0 }
            });

            var result = providerObj.getCustomerAccounts(1);

            Assert.That(result, Is.Not.Null);
        }

        [Test]
        public void getCustomerAccountsFailTest()
        {
            moqRepository.Setup(p => p.getCustomerAccounts(0)).Returns(new List<Account> { });

            var result = providerObj.getCustomerAccounts(0);

            Assert.That(result.Count, Is.EqualTo(0));
        }


        [Test]
        public void getAccountSuccessTest()
        {
            moqRepository.Setup(p => p.getAccount(1)).Returns(new Account()
            {
                AccountId = 1,
                CustomerId = 1,
                Balance = 1000,
                AccountType = "Savings",
                minBalance = 1000
            });

            var result = providerObj.getAccount(1);

            Assert.That(result, Is.Not.Null);
        }

        [Test]
        public void getAccountFailTest()
        {
            moqRepository.Setup(p => p.getAccount(0)).Returns(() => null);

            var result = providerObj.getAccount(0);
            Assert.That(result, Is.Null);
        }


        [Test]
        public void getAccountStatementSuccessTest()
        {

            moqRepository.Setup(p => p.getAccountStatement(1, It.IsAny<DateTime>(), It.IsAny<DateTime>())).Returns(new List<Statement>{ new Statement() {} });

            var result = providerObj.getAccountStatement(1, DateTime.Now.AddMonths(-1), DateTime.Now);

            Assert.That(result, Is.Not.Null);
        }

        [Test]
        public void getAccountStatementFailTest()
        {
            moqRepository.Setup(p => p.getAccountStatement(1, DateTime.Now, DateTime.Now)).Returns(new List<Statement> {});

            var result = providerObj.getAccountStatement(1, DateTime.Now, DateTime.Now);

            Assert.That(result.Count, Is.EqualTo(0));
        }



        [Test]
        public void depositSuccessTest()
        {

            moqRepository.Setup(p => p.deposit(1, 200)).Returns(new TransactionStatus()
            {
                message= "Your account has been credited",
                source_balance=1000,
                destination_balance=1200
            });

            var result = providerObj.deposit(1, 200);

            Assert.That(result, Is.Not.Null);
        }

        [Test]
        public void depositFailTest()
        {
            moqRepository.Setup(p => p.deposit(0, 200)).Returns(() => null);
            var result = providerObj.deposit(0, 200);
            Assert.That(result, Is.Null);
        }



        [Test]
        public void withdrawSuccessTest()
        {

            moqRepository.Setup(p => p.withdraw(1, 200)).Returns(new TransactionStatus()
            {
                message = "Your account has been debited",
                source_balance = 1000,
                destination_balance = 800
            });

            var result = providerObj.withdraw(1, 200);

            Assert.That(result, Is.Not.Null);
        }

        [Test]
        public void withdrawFailTest()
        {
            moqRepository.Setup(p => p.withdraw(1, 200)).Returns(() => null);
            var result = providerObj.withdraw(1, 200);

            Assert.That(result, Is.Null);
        }

        
    }
}
