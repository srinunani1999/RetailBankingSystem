using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using RetailBankSystemClient.Models;

namespace RetailBankSystemClient.Controllers
{
    public class LoginController : Controller
    {
        private log4net.ILog _logger = log4net.LogManager.GetLogger(typeof(LoginController));

        // GET: 
        [HttpGet]
        public IActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Login(User user)
        {
            _logger.Info("User Login");
            User Item = new User();
            using (var httpClient = new HttpClient())
            {

                StringContent content = new StringContent(JsonConvert.SerializeObject(user), Encoding.UTF8, "application/json");
                //https ://authenticationservicepod1.azurewebsites.net/
                //http ://40.88.230.141/
                var response = await httpClient.PostAsync("https://localhost:44359/api/Auth/User", content);
                if (!response.IsSuccessStatusCode)
                {
                    ViewBag.message = "Invalid Login Credentials for Id " + user.UserId;
                    return View("Login");

                }

                string apiResponse = await response.Content.ReadAsStringAsync();
                Item = JsonConvert.DeserializeObject<User>(apiResponse);
                StringContent content1 = new StringContent(JsonConvert.SerializeObject(user), Encoding.UTF8, "application/json");
                using (var response1 = await httpClient.PostAsync("https://localhost:44359/api/Auth/Login", content1))
                {
                    if (!response1.IsSuccessStatusCode)
                    {
                        ViewBag.message = "Invalid Login Credentials for Id "+user.UserId;
                        return View("Login");
                       // return RedirectToAction("Login");
                        
                    }

                    string apiResponse1 = await response1.Content.ReadAsStringAsync();



                    string stringJWT = response1.Content.ReadAsStringAsync().Result;

                    HttpContext.Session.SetString("token", stringJWT);
                    HttpContext.Session.SetString("user", JsonConvert.SerializeObject(Item));
                   
                    HttpContext.Session.SetString("Username", Item.Username);
                    HttpContext.Session.SetInt32("UserId", Item.UserId);
                    HttpContext.Session.SetString("UserRole", Item.Role);

                    


                    ViewBag.Message = "User logged in successfully!";

                    return RedirectToAction("Index", "Home");


                }
            }
        }
        public ActionResult Logout()
        {
            _logger.Info("User Log Out");
            HttpContext.Session.Remove("token");
            HttpContext.Session.Clear();
            // HttpContext.Session.SetString("user", null);

            return RedirectToAction("Index", "Home");
        }


        public ActionResult Invalid()
        {


            return View();
        }



    }
}
