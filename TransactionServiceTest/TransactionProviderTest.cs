using Moq;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using TransactionService.Models;
using TransactionService.Providers;
using TransactionService.Repositories;

namespace TransactionServiceTest
{
    [TestFixture]
    public class TransactionProviderTest
    {
        private Mock<ITransactionRepo> _repo;
        private TransactionProvider _transactionprovider;

        [SetUp]
        public void Setup()
        {
            _repo = new Mock<ITransactionRepo>();
            _transactionprovider = new TransactionProvider(_repo.Object);

        }

        [Test]
        public void TransactionProvider_addtoTransactionHistoryMethod_PassTest()
        {
            _repo.Setup(repo => repo.addToTransactionHistory(It.IsAny<TransactionStatus>(), It.IsAny<Account>())).Returns(true);

            var result = _transactionprovider.addToTransactionHistory(new TransactionStatus(), new Account());

            Assert.IsTrue(result);
        }

        [Test]
        public void TransactionProvider_addtoTransactionHistoryMethod_failTest()
        {
            _repo.Setup(repo => repo.addToTransactionHistory(It.IsAny<TransactionStatus>(), It.IsAny<Account>())).Returns(false);



            Assert.That(() => _transactionprovider.addToTransactionHistory(new TransactionStatus(), new Account()),
                Throws.ArgumentNullException);
          
               
        }


        [Test]
        public void TransactionProvider_getTransactions_PassTest()
        {
            _repo.Setup(repo => repo.getTransactions(It.IsAny<int>())).Returns(new List<TransactionHistory> { new TransactionHistory() });

            var result = _transactionprovider.getTransactions(2);

            Assert.AreEqual(1, result.Count());
        }

        [Test]
        public void TransactionProvider_getTransactions_FailTest()
        {
            _repo.Setup(repo => repo.getTransactions(It.IsAny<int>())).Returns(new List<TransactionHistory> { new TransactionHistory(),new TransactionHistory() });

            var result = _transactionprovider.getTransactions(2);

            Assert.AreNotEqual(1, result.Count());
        }




    }
}
