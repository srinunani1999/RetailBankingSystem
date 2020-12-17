using AuthenticationService.Models;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Threading.Tasks;

namespace AuthenticationService.Provider
{
    public interface IAuthProvider
    {
        public string LoginProvider(User model);

        public List<User> getAllUsers();
    }
}
