using AuthenticationService.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AuthenticationService.Repositories
{
    public interface IAuthRepo
    {
        public bool UserLogin(User user);
        public List<User> getAllUsers();
    }
}
