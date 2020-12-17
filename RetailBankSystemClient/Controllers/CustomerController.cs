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
	public class CustomerController : Controller
	{
		private log4net.ILog _logger = log4net.LogManager.GetLogger(typeof(CustomerController));

		private readonly ICustomerProvider _provider;
		public CustomerController(ICustomerProvider provider)
		{
			_provider = provider;

		}
		// GET: CustomerController
		public ActionResult Index()
		{
			if (HttpContext.Session.GetString("UserRole") != "Employee")
			{
				return RedirectToAction("Login", "Login");
			}
			return View();
		}


		[HttpGet]
		public ActionResult createCustomer()
		{
			if (HttpContext.Session.GetString("UserRole") != "Employee")
			{
				return RedirectToAction("Login", "Login");
			}
			return View();
		}
		[HttpPost]
		[ValidateAntiForgeryToken]

		public async Task<ActionResult> createCustomer(CreateCustomer model)
		{
			if (!ModelState.IsValid)
				return View(model);

		   

			CustomerCreationStatus createSuccess = new CustomerCreationStatus();
			try
			{
				var currentdate = DateTime.Now;
				var date = DateTime.Now;
				var k = currentdate.Year - model.DateOfBirth.Year;
				if (k<=18)
				{
					ViewBag.datevalidation = "Age should be greater than 18";
					return View(model);

				}

				var response = await _provider.Createcus(model);
				if (response.StatusCode == System.Net.HttpStatusCode.OK)
				{
					var jsoncontent = await response.Content.ReadAsStringAsync();
					createSuccess = JsonConvert.DeserializeObject<CustomerCreationStatus>(jsoncontent);
					//return RedirectToAction("GetCustomer","Customer");
					return View("CreateSuccess", createSuccess);
				}
				else if (response.StatusCode == System.Net.HttpStatusCode.InternalServerError)
				{
					ModelState.AddModelError("", "Having server issue while adding record");
					return View(model);
				}
				else if (response.StatusCode == System.Net.HttpStatusCode.Conflict)
				{
					ModelState.AddModelError("", "Username already present with ID :" + model.CustomerId);
					return View(model);
				}
				else if (response.StatusCode == System.Net.HttpStatusCode.BadRequest)
				{
					ModelState.AddModelError("", "Invalid model states");
					return View(model);
				}
			}
			catch (Exception ex)
			{
				_logger.Error("Exception occured due to  " + ex.Message);
			}
			return View(model);


		}
		[HttpGet]
		public ActionResult CreateSuccess()
		{
			CustomerCreationStatus model = new CustomerCreationStatus();
			return View(model);
		}

		[HttpGet]
		public async Task<IActionResult> GetCustomerDetails(int id)
		{
			if (HttpContext.Session.GetString("UserRole") != "Employee")
			{
				return RedirectToAction("Login", "Login");
			}
			else
			{
			CustomerViewModel customer = new CustomerViewModel();
				try
				{
					var response = await _provider.GetCustomerDetails(id);
					if (response.StatusCode == System.Net.HttpStatusCode.OK)
					{
						var JsonContent = await response.Content.ReadAsStringAsync();
						customer = JsonConvert.DeserializeObject<CustomerViewModel>(JsonContent);
						return View(customer);
					}
					else if (response.StatusCode == System.Net.HttpStatusCode.BadRequest)
					{
						ViewBag.Message = "No any record Found! Bad Request";
						return View(customer);
					}
					else if (response.StatusCode == System.Net.HttpStatusCode.InternalServerError)
					{
						ViewBag.Message = "Having server issue while adding record";
						return View(customer);
					}
					else if (response.StatusCode == System.Net.HttpStatusCode.NotFound)
					{
						ViewBag.Message = "No record found in DB for ID :" + id;
						return View(customer);
					}
				}
				catch (Exception ex)
				{
					_logger.Error("Exception occured as :" + ex.Message);
				}
				return View(customer);
			}
		}
		/// <summary>
		/// get All customers from the Customer API
		/// </summary>
		/// <returns></returns>
		[HttpGet]
		public async Task<IActionResult> GetCustomer()
		{
			if (HttpContext.Session.GetString("UserRole") != "Employee")
			{
				return RedirectToAction("Login", "Login");
			}
			else
			{
				List<CustomerViewModel> customers = new List<CustomerViewModel>();
				try
				{
					var response = await _provider.GetCustomers();
					if (response.StatusCode == System.Net.HttpStatusCode.OK)
					{
						var JsonContent = await response.Content.ReadAsStringAsync();
						customers = JsonConvert.DeserializeObject<List<CustomerViewModel>>(JsonContent);
						return View(customers);
					}
					else if (response.StatusCode == System.Net.HttpStatusCode.InternalServerError)
					{
						ViewBag.Message = "Having server issue while adding record";
						return View(customers);
					}

				}
				catch (Exception ex)
				{
					_logger.Error("Exceptions occured due to :" + ex.Message);
				}
				return View(customers);
			}
		}
	}
}
