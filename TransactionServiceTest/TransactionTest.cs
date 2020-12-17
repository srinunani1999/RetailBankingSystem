using Microsoft.AspNetCore.Mvc;
using Moq;
using NUnit.Framework;
using TransactionService.Controllers;
using TransactionService.Models;
using TransactionService.Providers;
using TransactionService.Repositories;

namespace TransactionServiceTest
{
    public class Tests
    {
        private ITransactionRepo transactionRepo;
      
        private Mock<IProvider> _provider;
        private TransactionController _controller;

        [SetUp]
        public void Setup()
        {
            _provider = new Mock<IProvider>();
            _controller = new TransactionController(_provider.Object);
        }

        [Test]
        public void TransactionRepo_AddToTransactionHistorymethod_Passtest()
        {
            transactionRepo = new TransactionRepo();
            Account account = new Account()
            {

                AccountId = 5,
                AccountType = "Savings2",
                Balance = 100,
                CustomerId = 6,
                minBalance = 1000

            };
            TransactionStatus transactionStatus = new TransactionStatus()
            {
                destination_balance = 2000,
                message = "amount credited",
                source_balance = 1900
            };
            var response = transactionRepo.addToTransactionHistory(transactionStatus,account);
            Assert.IsTrue(response);
        }
        [Test]
        public void TransactionRepo_AddToTransactionHistorymethod_Failtest()
        {
            transactionRepo = new TransactionRepo();
      
          
            var response = transactionRepo.addToTransactionHistory(null, null);
            Assert.IsFalse(response);
        }


        [Test]

        public void TransactionController_DepositMethod_FailTest()
        {

            var result = _controller.deposit(new DepositandWithdrawModel{ AccountId = 0, amount = 0 }) as ObjectResult ;

            Assert.AreEqual(400, result.StatusCode);
        }

        [Test]

        public void TransactionController_DepositMethod_PassTest()
        {
            _provider.Setup(p => p.getAccount(It.IsAny<int>())).Returns(new Account() { AccountId=1});
            _provider.Setup(p => p.deposit(It.IsAny<int>(), It.IsAny<int>())).Returns(new TransactionStatus() { message = "you account has deposited" });


            var result = _controller.deposit(new DepositandWithdrawModel { AccountId = 1, amount = 100 }) as ObjectResult ;

            Assert.AreEqual(200, result.StatusCode);
        }

        [Test]

        public void TransactionController_DepositMethod_NotFound_PassTest()
        {
            _provider.Setup(p => p.getAccount(It.IsAny<int>())).Returns(new Account());
            _provider.Setup(p => p.deposit(It.IsAny<int>(), It.IsAny<int>())).Returns(new TransactionStatus() { message = "you account has credited" });


            var result = _controller.deposit(new DepositandWithdrawModel { AccountId = 1, amount = 100 }) as ObjectResult;

            Assert.AreEqual(404, result.StatusCode);
        }
        [Test]

        public void TransactionController_WithdrawMethod_FailTest()
        {
            
            _provider.Setup(p => p.withdraw(It.IsAny<int>(), It.IsAny<int>())).Returns(new TransactionStatus() { message = "you account has debited" });


            var result = _controller.withdraw(new DepositandWithdrawModel { AccountId = 0, amount = 0 }) as ObjectResult;

            Assert.AreEqual(400, result.StatusCode);
        }

        [Test]

        public void TransactionController_WithdrawMethod_NotFoundTest()
        {
            _provider.Setup(p => p.getAccount(It.IsAny<int>())).Returns(new Account() { AccountId=1});
            _provider.Setup(p => p.rulesStatus(It.IsAny<int>(), It.IsAny<int>(), It.IsAny<Account>())).Returns(new RuleStatus() { status ="allowed"});
            _provider.Setup(p => p.withdraw(It.IsAny<int>(), It.IsAny<int>())).Returns(new TransactionStatus());


            var result = _controller.withdraw(new DepositandWithdrawModel { AccountId = 1, amount = 100 }) as ObjectResult;

            Assert.AreEqual(404, result.StatusCode);
        }

        [Test]

        public void TransactionController_WithdrawMethod_PassTest()
        {
            _provider.Setup(p => p.getAccount(It.IsAny<int>())).Returns(new Account() {AccountId=1 });
            _provider.Setup(p => p.rulesStatus(It.IsAny<int>(), It.IsAny<int>(), It.IsAny<Account>())).Returns(new RuleStatus() { status = "allowed" });

            _provider.Setup(p => p.withdraw(It.IsAny<int>(), It.IsAny<int>())).Returns(new TransactionStatus() { message = "you account has deposited" });


            var result = _controller.withdraw(new DepositandWithdrawModel { AccountId = 1, amount = 100 }) as ObjectResult;

            Assert.AreEqual(200, result.StatusCode);
        }





        [Test]

        public void TransactionController_TransferMethod_FailTest()
        {



            var result = _controller.transfer(new TransferModel() { amount = 0, Source_AccountId = 0, Target_AccountId = 0 }) as ObjectResult;

            Assert.AreEqual(400, result.StatusCode);
        }
        [Test]
        public void TransactionController_TransferMethod_PassTest()
        {
            _provider.Setup(repo => repo.getAccount(It.IsAny<int>())).Returns(new Account());
            _provider.Setup(repo => repo.rulesStatus(It.IsAny<int>(), It.IsAny<int>(), It.IsAny<Account>())).Returns(new RuleStatus() { status = "allowed" });
            _provider.Setup(repo => repo.deposit(It.IsAny<int>(), It.IsAny<int>())).Returns(new TransactionStatus() { message = "credited" });
            _provider.Setup(repo => repo.withdraw(It.IsAny<int>(), It.IsAny<int>())).Returns(new TransactionStatus() { message = "debited" });
            _provider.Setup(p => p.transfer(It.IsAny<int>(), It.IsAny<int>(), It.IsAny<int>())).Returns(new TransactionStatus() { message="tranferred"});

            var result = _controller.transfer(new TransferModel { Source_AccountId = 1, Target_AccountId = 1, amount = 1 }) as ObjectResult;

            Assert.AreEqual(200,result.StatusCode);
        }

        [Test]
        public void TransactionController_TransferMethod_NotFoundTest()
        {
            _provider.Setup(repo => repo.getAccount(It.IsAny<int>())).Returns(new Account() { AccountId = 1 });
            _provider.Setup(repo => repo.rulesStatus(It.IsAny<int>(), It.IsAny<int>(), It.IsAny<Account>())).Returns(new RuleStatus() { status = "deny" });
            _provider.Setup(repo => repo.deposit(It.IsAny<int>(), It.IsAny<int>())).Returns(new TransactionStatus() { message = "credited" });
            _provider.Setup(repo => repo.withdraw(It.IsAny<int>(), It.IsAny<int>())).Returns(new TransactionStatus() { message = "debited" });
            _provider.Setup(p => p.transfer(It.IsAny<int>(), It.IsAny<int>(), It.IsAny<int>())).Returns(new TransactionStatus());

            var result = _controller.transfer(new TransferModel { Source_AccountId = 1, Target_AccountId = 1, amount = 1 }) as ObjectResult;

            Assert.AreEqual(404, result.StatusCode);
        }

        //[Test]

        //public void TransactionController_WithdrawMethod_NotFoundTest()
        //{
        //    _provider.Setup(p => p.getAccount(It.IsAny<int>())).Returns(new Account() { AccountId = 1 });
        //    _provider.Setup(p => p.rulesStatus(It.IsAny<int>(), It.IsAny<int>(), It.IsAny<Account>())).Returns(new RuleStatus() { status = "allowed" });
        //    _provider.Setup(p => p.withdraw(It.IsAny<int>(), It.IsAny<int>())).Returns(new TransactionStatus());


        //    var result = _controller.withdraw(new DepositandWithdrawModel { AccountId = 1, amount = 100 }) as ObjectResult;

        //    Assert.AreEqual(404, result.StatusCode);
        //}

        //[Test]

        //public void TransactionController_WithdrawMethod_PassTest()
        //{
        //    _provider.Setup(p => p.getAccount(It.IsAny<int>())).Returns(new Account() { AccountId = 1 });
        //    _provider.Setup(p => p.rulesStatus(It.IsAny<int>(), It.IsAny<int>(), It.IsAny<Account>())).Returns(new RuleStatus() { status = "allowed" });

        //    _provider.Setup(p => p.withdraw(It.IsAny<int>(), It.IsAny<int>())).Returns(new TransactionStatus() { message = "you account has deposited" });


        //    var result = _controller.withdraw(new DepositandWithdrawModel { AccountId = 1, amount = 100 }) as ObjectResult;

        //    Assert.AreEqual(200, result.StatusCode);
        //}
    }
}