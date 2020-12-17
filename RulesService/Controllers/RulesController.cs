using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using RulesService.Models;
using RulesService.Provider;

namespace RulesService.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RulesController : ControllerBase
    {

        static readonly log4net.ILog _log4net = log4net.LogManager.GetLogger(typeof(RulesController));

        IRuleProvider _provider;
        public RulesController(IRuleProvider _provider)
        {
            this._provider = _provider;
        }

        [HttpGet]
        [Route("MonthlyBatchJob")]
        public IActionResult MonthlyBatchJob()
        {
            try
            {
                if (DateTime.Now.Day == 14)
                {
                    _log4net.Info("Monthly Checking Started");
                    _provider.RunMonthlyJob();
                    _log4net.Info("Monthly Service Charge Deduction Completed");
                }
                return Ok("Services charged applied to accounts");
            }
            catch (Exception e)
            {
               _log4net.Error("Monthy Charge Couldn't be applied due to exception");
                _log4net.Error(e.Message);
                return StatusCode(500);
            }
        }
        [HttpGet]
        [Route("api/Rules/GetServiceCharge")]
        public IActionResult GetServiceCharge(string AccountType)
        {
            try
            {
                float value=_provider.getServiceCharge(AccountType);
                return Ok(value);
            }
            catch(NullReferenceException e)
            {
                _log4net.Error("NullReferenceException caught.");
                return StatusCode(500);
            }
            catch(Exception e)
            {
                return StatusCode(500);
            }
        }
        [HttpGet]
        [Route("evaluateMinBal/{AccountID}/{Balance}")]
        public IActionResult evaluateMinBal(int AccountID, int Balance)
        {
            _log4net.Info("Evaluating Minimum Balance");
            try
            {
                var result = _provider.evaluateMinBalance(Balance,AccountID);
                return Ok(result);
                /*if (value==1)
                {
                    return Ok(new RuleStatus { Status = "allowed" });
                }
                else if(value==-1)
                {
                    return Ok(new RuleStatus { Status = "denied" });
                }
                else
                {
                    return Ok(new RuleStatus { Status = "NA" });
                }*/
                
            }
            catch (NullReferenceException e)
            {
                _log4net.Error("NullReferenceException caught. Issue in calling Account API");
                return StatusCode(500);
            }
            catch (Exception e)
            {
                return StatusCode(500);
            }
        }
    }
}