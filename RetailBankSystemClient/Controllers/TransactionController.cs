using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using RetailBankSystemClient.Providers;
using RetailBankSystemClient.ViewModels;

namespace RetailBankSystemClient.Controllers
{
    public class TransactionController : Controller
    {
        private log4net.ILog _logger = log4net.LogManager.GetLogger(typeof(TransactionController));
        // GET: TransactionController
        private ITransactionProvider _transactionProvider;
        public TransactionController(ITransactionProvider transactionProvider)
        {
            _transactionProvider = transactionProvider;

        }
        [HttpGet]
        public async Task<ActionResult> TransactionHistory(int id)
        {
            if (HttpContext.Session.GetString("UserRole") == null)
            {
                return RedirectToAction("Login", "Login");
            }
            //else
            //{
            List<TransactionHistoryViewModel> model = new List<TransactionHistoryViewModel>();
                try
                {
                    var response = await _transactionProvider.GetTransactions(id);
                    if (response.StatusCode == System.Net.HttpStatusCode.OK)
                    {
                        var JsonContent = await response.Content.ReadAsStringAsync();
                        model = JsonConvert.DeserializeObject<List<TransactionHistoryViewModel>>(JsonContent);
                        return View(model);
                    }
                    else if (response.StatusCode == System.Net.HttpStatusCode.NotFound)
                    {
                        ViewBag.Message = "Having server issue while adding record";
                        return View(model);
                    }
                    else if (response.StatusCode == System.Net.HttpStatusCode.InternalServerError)
                    {
                        ViewBag.Message = "Internal Server Error! Please try again later";
                        return View(model);
                    }
                }
                catch (Exception ex)
                {
                   _logger.Error("Exceptions occured due to " + ex.Message);
                }
                return View(model);
            //}
           
        }

      
    }
}
