using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using TransactionService.Models;
using TransactionService.Providers;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace TransactionService.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TransactionController : ControllerBase
    {

        static readonly log4net.ILog _log4net = log4net.LogManager.GetLogger(typeof(TransactionController));
        private IProvider _provider;
        public TransactionController(IProvider provider)
        {
            _provider = provider;
        }




        [HttpPost]
        [Route("deposit")]

        public ActionResult deposit([FromBody]dynamic model )
        {
            int AccountId = Convert.ToInt16( model.AccountId);
            int amount = Convert.ToInt16(model.amount);
            if (model.AccountId<=0||model.amount<0)
            {
                _log4net.Error("Account Id" + AccountId + " or Amount" + amount + " is Invalid");
                return  BadRequest(new TransactionStatus() { message = "Withdraw Not Allowed" });
            }

            try
            {
                _log4net.Info("Calling Account Api......for account id " + AccountId);
                Account account = _provider.getAccount(AccountId);
                if (account.AccountId==AccountId)
                {
                    _log4net.Info("Account Api called");
                    _log4net.Info("Deposit initiated for Account Id" + AccountId);
                    TransactionStatus transactionStatus= _provider.deposit(AccountId, amount);
                    _provider.addToTransactionHistory(transactionStatus, account);
                    _log4net.Info("Deposit Completed successfully for Account Id" + AccountId);
                    return Ok(transactionStatus);


                }

                return NotFound("Requested Account id not found "+AccountId);
                
            }
            catch (ArgumentNullException e)
            {
                _log4net.Error(e.Message);
                return StatusCode(500);
            }
            catch (Exception e)
            {
                _log4net.Error(e.Message + ": In Trasaction Controller");
                return StatusCode(500);
            }


        }

        [HttpPost]
        [Route("withdraw")]
        public ActionResult withdraw([FromBody] dynamic model)
        {
            int AccountId = Convert.ToInt16(model.AccountId);
            int amount = Convert.ToInt16(model.amount);
            
            try
            {
                TransactionStatus withdrawStatus = new TransactionStatus();
                if (AccountId <= 0 || amount < 0)
                {
                    _log4net.Error("Account Id" + AccountId + " or Amount" + amount + " is Invalid");
                    return BadRequest(new TransactionStatus() { message = "Deposit Not Allowed" });
                }
                _log4net.Info("Calling Account Api......for account id " + AccountId);


                Account account = _provider.getAccount(AccountId);
                _log4net.Info("Evaluating Min Balance for " + account.AccountId);
                RuleStatus ruleStatus = _provider.rulesStatus(AccountId, amount, account);

                if (ruleStatus.status=="allowed")
                {
                     withdrawStatus = _provider.withdraw(AccountId, amount);
                    if (withdrawStatus.message==null)
                    {
                        _log4net.Error("Unable to withdraw");
                        return NotFound(new TransactionStatus() { message = "Record Not Found" });

                    }
                    _provider.addToTransactionHistory(withdrawStatus, account);
                    


                }
                else if (ruleStatus.status == "denied")
                {
                    withdrawStatus.message = "Unable to withdraw";
                    withdrawStatus.source_balance = account.Balance;
                    withdrawStatus.destination_balance = account.Balance;

                    _log4net.Error("Unable to withdraw");

                    _provider.addToTransactionHistory(withdrawStatus, account);
                    return Ok(withdrawStatus);
                }


                return Ok(withdrawStatus);
            }
            catch (ArgumentNullException e)
            {
                _log4net.Error(e.Message);
                return StatusCode(500);
            }
            catch (Exception e)
            {
                _log4net.Error(e.Message + ": In Trasaction Controller");
                return StatusCode(500);
            }
        }
        [HttpGet]
        [Route("getTransactions/{CustomerId}")]
        public ActionResult getTransactions(int CustomerId)
        {

            try
            {
                if (CustomerId == 0)
                {
                    _log4net.Info("Invalid Customer Id");
                    return NotFound();
                }
                _log4net.Info("Transaction history initiated for Customer Id: " + CustomerId);

                List<TransactionHistory> transactionHistories = _provider.getTransactions(CustomerId);
                _log4net.Info("Transaction history request complted for Customer Id: " + CustomerId);

                return Ok(transactionHistories);
            }
            catch (ArgumentException e)
            {
                _log4net.Error(e.Message);
                return StatusCode(500);
            }
            catch (Exception e)
            {
                _log4net.Error(e.Message);
                return StatusCode(500);
            }
        }


      


      [HttpPost]
      [Route("transfer")]
        public ActionResult transfer([FromBody]dynamic model)
        {
            int Source_AccountId = Convert.ToInt32(model.Source_AccountId);
            int Target_AccountId = Convert.ToInt32(model.Target_AccountId);
            int amount = Convert.ToInt16(model.amount);
           
            try
            {
                if (Source_AccountId <= 0 || Target_AccountId <= 0 || amount <= 0)
                {
                    _log4net.Error("invalid parameters");
                    return BadRequest(new TransactionStatus() { message = "Transfer Not Allowed" });
                }


                var transactionStatus= _provider.transfer(Source_AccountId, Target_AccountId, amount);

                if (transactionStatus.message == null)
                {
                    return NotFound(new TransactionStatus() { message = "Record Not Found" });
                }
                return Ok(transactionStatus);
                



            }
            catch (ArgumentNullException e)
            {
                _log4net.Error(e.Message);
                return StatusCode(500);
            }
            catch (Exception e)
            {
                _log4net.Error(e.Message + ": In Trasaction Controller");
                return StatusCode(500);
            }
        }
    }
}
