using Microsoft.AspNetCore.Mvc;
using Moq;
using NUnit.Framework;
using RulesService.Controllers;
using RulesService.Models;
using RulesService.Provider;
using RulesService.Repository;

namespace NUnitTest_RulesService
{
    [TestFixture]
    public class Tests
    {
        private RulesController _rulesController;
        private Mock<IRuleProvider> _ruleProviderMock;
        [SetUp]
        public void Setup()
        {
            _ruleProviderMock = new Mock<IRuleProvider>();
            _rulesController = new RulesController(_ruleProviderMock.Object);

            
        }
        [Test]
        public void EvaluateMinBal_TestPass()
        {
            _ruleProviderMock.Setup(p => p.evaluateMinBalance(1, 2000)).Returns(new RuleStatus
            {
                Status = "allowed",
            });

            var result = _rulesController.evaluateMinBal(1,2000);

            Assert.That(result, Is.InstanceOf<OkObjectResult>());

        }
        [Test]
        public void EvaluateMinBal_TestFail()
        {
            _ruleProviderMock.Setup(p => p.evaluateMinBalance(2, 0)).Returns(new RuleStatus
            {
                Status = "denied",
            });

            var result = _rulesController.evaluateMinBal(2, 0);

            Assert.That(result, Is.InstanceOf<OkObjectResult>());

        }


        [Test]
        public void RunMonthlyJobTest()
        {
            var result = _rulesController.MonthlyBatchJob() as OkObjectResult;
            var model = result.Value as string;
            Assert.AreEqual(model, "Services charged applied to accounts");
        }
        [Test]
        public void GetServiceChargesForSavingsAccount()
        {
            _ruleProviderMock.Setup(p => p.getServiceCharge("Savings")).Returns(100);
            var result = _rulesController.GetServiceCharge("Savings") ;
            Assert.That(result, Is.InstanceOf<OkObjectResult>());

        }
        [Test]
        public void GetServiceChargesForCurrentAccount()
        {
            _ruleProviderMock.Setup(p => p.getServiceCharge("Current")).Returns(200);
            var result = _rulesController.GetServiceCharge("Current");
            Assert.That(result, Is.InstanceOf<OkObjectResult>());

        }


    }
}