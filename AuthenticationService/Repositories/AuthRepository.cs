using AuthenticationService.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AuthenticationService.Repositories
{
    public class AuthRepository : IAuthRepo
    {
        private readonly log4net.ILog _logger = log4net.LogManager.GetLogger(typeof(AuthRepository));
        private static List<User> Users = new List<User>()
        {
            new User(){UserId=1, Username="srinu",Password="nani",Role="Employee"},
            new User(){ UserId=2, Username="srinus",Password="nani",Role="Customer"},
            new User(){UserId=3, Username="Nikitha",Password="nikitha",Role="Customer"},
            new User(){UserId=4, Username="priyanka",Password="priyanka",Role="Customer"},


        };

        
        public bool UserLogin(User user)
        {
            
            try
            {
                foreach (var model in Users.ToList())
                {
                    if (model.UserId == user.UserId && model.Username == user.Username && model.Password == user.Password && model.Role == user.Role)
                    {
                        return true;

                    }
                
                 
                }
            }
            catch (Exception e)
            {
                _logger.Error("User Authentication Failed due to " + e.Message);
            }
            return false;
        }


        public List<User> getAllUsers()
        {
            try
            {
                return Users;

            }
            catch (Exception e)
            {
                _logger.Error("Unable to fetch the users" + e.Message);
                throw;
            }
        }
    }
}
