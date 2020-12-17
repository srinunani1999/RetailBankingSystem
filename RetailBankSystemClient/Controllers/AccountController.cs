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
	public class AccountController : Controller
	{
		private log4net.ILog _logger = log4net.LogManager.GetLogger(typeof(AccountController));

		private readonly IAccountProvider _accountProvider;
		private readonly ITransactionProvider _transactionProvider;

		public AccountController(IAccountProvider accountProvider,ITransactionProvider transactionProvider)
		{
			_accountProvider = accountProvider;
			_transactionProvider = transactionProvider;

		}
		[HttpGet]
		public async Task<IActionResult> GetCustomerAccount(int id)
		{
			if (HttpContext.Session.GetString("UserRole") == null)
			{
				return RedirectToAction("Login", "Login");
			}
			else
			{
				List<AccountViewModel> accountViews = new List<AccountViewModel>();
				try
				{
					var response = await _accountProvider.getCustomerAccounts(id);
					if (response.StatusCode == System.Net.HttpStatusCode.OK)
					{
						var JsonContent = await response.Content.ReadAsStringAsync();
						accountViews = JsonConvert.DeserializeObject<List<AccountViewModel>>(JsonContent);
						return View(accountViews);
					}
					else if (response.StatusCode == System.Net.HttpStatusCode.NotFound)
					{
						ViewBag.Message = "Invalid Customer ID";
						return View(accountViews);
					}
					else if (response.StatusCode == System.Net.HttpStatusCode.InternalServerError)
					{
						ViewBag.Message = "Internal Server Error! Please try again later";
						return View(accountViews);
					}
				}
				catch (Exception ex)
				{
				_logger.Warn("Exceptions Occured due to " + ex.Message);
				}
				return View(accountViews);
			}
		}

		[HttpGet]
		public async Task<IActionResult> GetAccount(int id)
		{
			if (HttpContext.Session.GetString("UserRole") == null)
			{
				return RedirectToAction("Login", "Login");
			}
			else
			{
				GetAccountViewModel model = new GetAccountViewModel();
				try
				{
					var response = await _accountProvider.getAccount(id);
					if (response.StatusCode == System.Net.HttpStatusCode.OK)
					{
						var JsonContent = await response.Content.ReadAsStringAsync();
						model = JsonConvert.DeserializeObject<GetAccountViewModel>(JsonContent);
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
					_logger.Error("Exceptions Occured as " + ex.Message);
				}
				return View(model);
			}
		}

		[HttpGet]
		public IActionResult GetAccountStatements(int Id)
		{
			if (HttpContext.Session.GetString("UserRole") == null)
			{
				return RedirectToAction("Login", "Login");
			}
			else
			{
				AccountStatementViewModel model = new AccountStatementViewModel() { Id = Id };
				return View(model);
			}
		

		}
		[HttpPost]
		[ValidateAntiForgeryToken]
		public async Task<IActionResult> GetAccountStatements(AccountStatementViewModel accountStatementViewModel)
		{
			List<StatementViewModel> statementViews = new List<StatementViewModel>();
			try
			{
				var response = await _accountProvider.GetAccountStatement(accountStatementViewModel);
				if (response.StatusCode == System.Net.HttpStatusCode.OK)
				{
					var JsonContent = await response.Content.ReadAsStringAsync();
					statementViews = JsonConvert.DeserializeObject<List<StatementViewModel>>(JsonContent);
					return View("Statements", statementViews);
				}
				else if (response.StatusCode == System.Net.HttpStatusCode.NotFound)
				{
					ViewBag.Message = "Having server issue while adding record";
					return View("Statements", statementViews);
				}
				else if (response.StatusCode == System.Net.HttpStatusCode.InternalServerError)
				{
					ViewBag.Message = "Internal Server Error! Please try again later";
					return View("Statements", statementViews);
				}
			}
			catch (Exception ex)
			{
				//_logger.Error("Exceptions Occured as " + ex.Message);
			}
			return View("Statements", statementViews);
			
		}

		[HttpGet]
		public IActionResult Statements()
		{
			if (HttpContext.Session.GetString("UserRole") == null)
			{
				return RedirectToAction("Login", "Authentication");
			}
			else
			{
				List<StatementViewModel> statementViewModels = new List<StatementViewModel>();
				return View(statementViewModels);
			}
		}


		[HttpGet]
		public IActionResult Deposit()
		{
			if (HttpContext.Session.GetString("UserRole") == null)
			{
				return RedirectToAction("Login", "Login");
			}

			return View();
		}
		[HttpPost]
		public async Task<IActionResult> Deposit(DepositViewModel model)
		{

			TransactionStatusViewModel transactionStatus = new TransactionStatusViewModel();
			try
			{
				var response = await _transactionProvider.Deposit(model);
				if (response.StatusCode == System.Net.HttpStatusCode.OK)
				{
					var jsoncontent = await response.Content.ReadAsStringAsync();
					transactionStatus = JsonConvert.DeserializeObject<TransactionStatusViewModel>(jsoncontent);
					return View("TransactionStatus", transactionStatus);
				}
				else if (response.StatusCode == System.Net.HttpStatusCode.NotFound)
				{
					ModelState.AddModelError("", "Having server issue while adding record");
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
			ModelState.AddModelError("", "Having some unexpected error while processing transaction");
			return View(model);
		}

		[HttpGet]
		public IActionResult TransactionStatus()
		{
			TransactionStatusViewModel model = new TransactionStatusViewModel();
			return View(model);
		}

		[HttpGet]
		public async Task<IActionResult> AccountsForWithdrawAndTransfer(int id)
		{
			if (HttpContext.Session.GetString("UserRole") == null)
			{
				return RedirectToAction("Login", "Login");
			}
			//else
			//{
			List<AccountViewModel> accountViews = new List<AccountViewModel>();
			try
			{
				var response = await _accountProvider.getCustomerAccounts(id);
				if (response.StatusCode == System.Net.HttpStatusCode.OK)
				{
					var JsonContent = await response.Content.ReadAsStringAsync();
					accountViews = JsonConvert.DeserializeObject<List<AccountViewModel>>(JsonContent);
					return View(accountViews);
				}
				else if (response.StatusCode == System.Net.HttpStatusCode.NotFound)
				{
					ViewBag.Message = "Invalid Customer ID";
					return View(accountViews);
				}
				else if (response.StatusCode == System.Net.HttpStatusCode.InternalServerError)
				{
					ViewBag.Message = "Internal Server Error! Please try again later";
					return View(accountViews);
				}
			}
			catch (Exception ex)
			{
					_logger.Warn("Exceptions Occured due to  " + ex.Message);
			}
			return View(accountViews);
			
		}
		[HttpGet]
		public IActionResult Withdraw(int id)
		{
			if (HttpContext.Session.GetString("UserRole") == null)
			{
				return RedirectToAction("Login", "Login");
			}
			WithdrawViewModel withdrawViewModel = new WithdrawViewModel() { AccountId = id };
			return View(withdrawViewModel);
		}
		[HttpPost]
		public async Task<IActionResult> Withdraw(WithdrawViewModel model)
		{

			TransactionStatusViewModel transactionStatus = new TransactionStatusViewModel();
			try
			{
				var response = await _transactionProvider.Withdraw(model);
				if (response.StatusCode == System.Net.HttpStatusCode.OK)
				{
					var jsoncontent = await response.Content.ReadAsStringAsync();
					transactionStatus = JsonConvert.DeserializeObject<TransactionStatusViewModel>(jsoncontent);
					return View("TransactionStatus", transactionStatus);
				}
				else if (response.StatusCode == System.Net.HttpStatusCode.NotFound)
				{
					ModelState.AddModelError("", "Having server issue while adding record");
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
			ModelState.AddModelError("", "Having some unexpected error while processing transaction");
			return View(model);
		}


		[HttpGet]
		public IActionResult transfer(int id)
		{
			if (HttpContext.Session.GetString("UserRole") == null)
			{
				return RedirectToAction("Login", "Login");
			}

			TransferViewModel model = new TransferViewModel() { Source_AccountId = id };
			return View(model);
		}
		[HttpPost]
		public async Task<IActionResult> transfer(TransferViewModel model)
		{

			TransactionStatusViewModel transactionStatus = new TransactionStatusViewModel();
			try
			{
				var response = await _transactionProvider.Transfer(model);
				if (response.StatusCode == System.Net.HttpStatusCode.OK)
				{
					var jsoncontent = await response.Content.ReadAsStringAsync();
					transactionStatus = JsonConvert.DeserializeObject<TransactionStatusViewModel>(jsoncontent);
					return View("TransactionStatus", transactionStatus);
				}
				else if (response.StatusCode == System.Net.HttpStatusCode.NotFound)
				{
					ModelState.AddModelError("", "Having server issue while adding record");
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
				_logger.Error("Exceptions Occured as " + ex.Message);
			}
			ModelState.AddModelError("", "Having some unexpected error while processing transaction");
			return View(model);
		}

	}
}
