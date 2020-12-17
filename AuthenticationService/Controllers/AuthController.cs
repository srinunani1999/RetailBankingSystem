using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AuthenticationService.Models;
using AuthenticationService.Provider;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace AuthenticationService.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private IAuthProvider _authProvider;
        private log4net.ILog _logger = log4net.LogManager.GetLogger(typeof(AuthController));

        public AuthController(IAuthProvider authProvider)
        {
            this._authProvider = authProvider;
        }

        [HttpPost]
        [Route("User")]
        public ActionResult getUser(User user)
        {
            _logger.Info(user.Username + " requested to getuser");
            if (!ModelState.IsValid)
            {
                return BadRequest(user);
            }

            var users = _authProvider.getAllUsers();
            foreach (var item in users)
            {
                if (item.Username == user.Username && item.Password==user.Password && item.Role==user.Role )
                {
                    return Ok(item);

                }

            }
            _logger.Error("user not found with username "+user.Username);
            return NotFound("User not found");

        }

        [HttpPost]
        [Route("Login")]
        public IActionResult UserLogin([FromBody] User model)
        {
            _logger.Info(model.Username + " requested to login");
            if (!ModelState.IsValid)
            {
                return BadRequest(model);
            }
            try
            {
                var token = _authProvider.LoginProvider(model);
                if (token == null)
                {
                    _logger.Warn(model.Username + "Failed to login");
                    return new StatusCodeResult(500);
                }
                else
                {
                    _logger.Info(model.Username + " logged in successfully");
                    return Ok(token);
                }
            }
            catch (Exception ex)
            {
                _logger.Warn("Exception occured in AuthController while calling authProvider as :" + ex.Message);
                return new StatusCodeResult(500);
            }
        }
       
    }
}
