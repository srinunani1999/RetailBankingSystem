using CustomerServices.Models;
using CustomerServices.Provider;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CustomerServices.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CustomerController : ControllerBase
    {
        static readonly log4net.ILog _log4net = log4net.LogManager.GetLogger(typeof(CustomerController));
        private IProvider _provider;
        public CustomerController(IProvider provider)
        {
            this._provider = provider;
        }
        [HttpGet("Get")]
        public IActionResult Get()
        {
            try
            {
                _log4net.Info("Getting Customer");
                return Ok(_provider.GetAll());
            }
            catch (Exception e)
            {
                _log4net.Error(e.Message);
                return BadRequest();
            }
        }
        [HttpGet]
        [Route("GetbyId/{id}")]

        public IActionResult GetbyId(int id)
        {
            try
            {

                var list = _provider.GetCustomerdetails(id);
                if (list == null)
                {
                    _log4net.Error("Customer not found with Id:" + id);
                    return NotFound();
                }
                else
                {
                    _log4net.Info("Getting Customer details");
                    return Ok(list);
                }
            }
            catch (Exception e)
            {
                _log4net.Info("exception due to "+e.Message);
                return BadRequest();
            }

        }
        [HttpPost]
        [Route("Creation")]
        public IActionResult Creation([FromBody] Customer cust)
        {
            if (!ModelState.IsValid)
            {
                _log4net.Info("No Customer has been returned");
                return BadRequest();
            }
            try
            {
                bool result = _provider.CreateCustomer(cust);
                if (result)
                {
                    _log4net.Info("Customer has been created  ");
                    CustomerCreationStatus status = new CustomerCreationStatus();
                    status.Message = "Customer Created Successfully";
                    status.CustomerId = cust.CustomerId;
                    return Ok(status);
                }
            
                    return new StatusCodeResult(409);
                
            }
            catch (Exception e)
            {
                return new StatusCodeResult(500);
            }
        }
    }
}
