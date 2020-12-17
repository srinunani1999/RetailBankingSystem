using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AccountService.Models;
using AccountService.Models.ViewModel;
using AccountService.Repository;
using AccountService.Provider;
using Newtonsoft.Json;

namespace AccountService.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AccountController : ControllerBase
    {
        readonly log4net.ILog _log4net = log4net.LogManager.GetLogger(typeof(AccountController));
        private readonly IAccountProvider _Provider;

        public AccountController(IAccountProvider Provider)
        {
            _Provider = Provider;
        }

        

        [HttpPost("createAccount")]
        public IActionResult createAccount([FromBody] dynamic model)
        {
            if (model.CustomerId == 0)
            {
                _log4net.Warn("Invalid CustomerId " + model.CustomerId);
                return NotFound();
            }
            try
            {
                AccountCreationStatus accountCreationStatus, accountCreationStatus1 = new AccountCreationStatus();


                accountCreationStatus = _Provider.createAccount(Convert.ToInt16( model.CustomerId), "Savings");
                _log4net.Info("Savings account has been created successfully.");
                accountCreationStatus1 = _Provider.createAccount(Convert.ToInt16(model.CustomerId), "Current");
                _log4net.Info("Current account has been created successfully.");
                return Ok(accountCreationStatus1);
            }
            catch (Exception e)
            {
                _log4net.Error(e.Message);
                return StatusCode(500);
            }
        }


        [HttpGet]
        [Route("getCustomerAccounts/{CustomerId}")]
        public IActionResult getCustomerAccounts(int CustomerId)
        {
            if (CustomerId == 0)
            {
                _log4net.Warn("invalid Accountid : " + CustomerId);
                return NotFound();
            }
            try
            {

                var accounts = _Provider.getCustomerAccounts(CustomerId);
                _log4net.Info("Customer accounts returned successfully.");
                return Ok(accounts);

            }
            catch (Exception e)
            {
                _log4net.Error(e.Message);
                return StatusCode(500);
            }
        }



        [HttpGet]
        [Route("getAccount/{AccountId}")]
        public IActionResult getAccount(int AccountId)
        {
            if (AccountId == 0)
            {
                _log4net.Warn("Invalid AccountId : " + AccountId);
                return NotFound();

            }
            try
            {
                Account acc = new Account();
                acc = _Provider.getAccount(AccountId);
                _log4net.Info("Acccount returned successfully.");
                return Ok(acc);
            }
            catch (Exception e)
            {
                _log4net.Error(e.Message);
                return StatusCode(500);

            }

        }


        
        [HttpPost("getAccountStatement")]
        //[Route("getAccountStatement")]
        public IActionResult getAccountStatement([FromBody]dynamic model)
        {
            if (model.Id == 0)
            {
                _log4net.Warn("Invalid AccountId : " + model.Id);
                return NotFound();
            }
            try
            {


                DateTime? temp = null;

                if ((model.from_date == temp) && (model.to_date == temp))
                {
                    var statements = _Provider.getAccountStatement(Convert.ToInt32( model.Id),Convert.ToDateTime( model.from_date),Convert.ToDateTime( model.to_date)).ToList();
                    _log4net.Info("Statement for given AccountId : " + model.Id + " returned successfully.");

                    return Ok(statements);

                }
                else
                {
                    var statements = _Provider.getAccountStatement(Convert.ToInt32(model.Id), Convert.ToDateTime(model.from_date), Convert.ToDateTime(model.to_date));
                    _log4net.Info("Statement for given AccountId : " + Convert.ToInt32(model.Id) + " returned successfully.");

                    return Ok(statements);

                }
                //_log4net.Info("Statement for given AccountId : "+ AccountId + " returned successfully.");

                //return Ok(statements);
            }
            catch (Exception e)
            {
                _log4net.Error(e.Message);
                return StatusCode(500);
            }
        }



        
        [HttpPost("deposit")]
        public IActionResult deposit([FromBody]dynamic model)
        {
           
            if (model.AccountId == 0 || model.amount == 0)
            {
                _log4net.Warn("Invalid AccountId or amount is 0.");
                return NotFound();
            }

            try
            {
                TransactionStatus transactionStatus = new TransactionStatus();
                transactionStatus = _Provider.deposit(Convert.ToInt32(model.AccountId), Convert.ToInt32(model.amount));
                _log4net.Info("Account has been credited successfully.");
                return Ok(transactionStatus);
            }
            catch (Exception e)
            {
                _log4net.Error(e.Message);
                return StatusCode(500);
            }

        }

        

        [HttpPost("withdraw")]
        public IActionResult withdraw([FromBody] dynamic model)
        {

            if (model.AccountId == 0 || model.amount == 0)
            {
                _log4net.Warn("Invalid AccountId or amount is 0");
                return NotFound();
            }
            try
            {
                TransactionStatus transactionStatus = new TransactionStatus();
                transactionStatus = _Provider.withdraw(Convert.ToInt32(model.AccountId), Convert.ToInt32(model.amount));

                _log4net.Info("account has been debited successfully");
                return Ok(transactionStatus);

            }

            catch (Exception e)
            {
                _log4net.Error(e.Message);
                return StatusCode(500);
            }

        }

        [HttpGet]
        [Route("getAllCustomerAccounts")]
        public IActionResult getAllCustomerAccounts()
        {
            List<Account> allAccountsList = new List<Account>();
            try
            {
                allAccountsList = _Provider.getAllCustomerAccounts().ToList();

            }
            catch (Exception e)
            {
                _log4net.Error(e.Message);
                return StatusCode(500);
            }

            _log4net.Info("Customer's account has been successfully returned");
            return Ok(allAccountsList);
        }
    }
}
