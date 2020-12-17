using Microsoft.AspNetCore.Mvc;
using Moq;
using NUnit.Framework;
using RulesService.Models;
using RulesService.Provider;
using RulesService.Repository;
using System;
using System.Collections.Generic;
using System.Text;

namespace NUnitTest_RulesService
{
    [TestFixture]
    class ProviderTest
    {
        private Mock<IRulesRepository> moqRepository;
        private RuleProvider providerObj;
        [SetUp]
        public void Setup()
        {
            moqRepository = new Mock<IRulesRepository>();
            providerObj = new RuleProvider(moqRepository.Object);
        }
        [Test]
        public void evaluateMinBal_TestAllowed()
        {
            moqRepository.Setup(p => p.evaluateMinBal(1, 3000)).Returns(new RuleStatus
            {
                Status = "allowed"
            });
            var result = providerObj.evaluateMinBalance(1, 3000);
            Assert.AreEqual(result.Status, "allowed");
        }
        [Test]
        public void evaluateMinBal_TestDenied()
        {
            moqRepository.Setup(p => p.evaluateMinBal(1, 300)).Returns(new RuleStatus
            {
                Status = "denied"
            });
            var result = providerObj.evaluateMinBalance(1, 300);
            Assert.AreEqual(result.Status, "denied");
        }
        [Test]
        public void GetServiceChargesTest_SavingsAccount()
        {
            moqRepository.Setup(p => p.getServiceCharges("Savings")).Returns(100);
            var result = providerObj.getServiceCharge("Savings");
            Assert.AreEqual(result, 100);
            
        }
        [Test]
        public void GetServiceChargesTest_CurrentAccount()
        {
            moqRepository.Setup(p => p.getServiceCharges("Current")).Returns(200);
            var result = providerObj.getServiceCharge("Current");
            Assert.AreEqual(result, 200);

        }
        [Test]
        public void getCutomerAccountsSuccessTest()
        {
            moqRepository.Setup(p => p.getAccounts()).Returns(new List<Account>{
                new Account(){
                    AccountId=1,
                    CustomerId=1,
                    AccountType="Savings",
                    Balance=10000,
                    minBalance=1000

                },
                new Account(){
                    AccountId=2,
                    CustomerId=1,
                    AccountType="Current",
                    Balance=2000,
                    minBalance=1000

                }
            });

            var result = providerObj.getAccounts();

            Assert.That(result, Is.Not.Null);
        }

    }
    
}
