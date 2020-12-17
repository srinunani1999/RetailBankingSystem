using AuthenticationService.Models;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Threading.Tasks;

namespace AuthenticationService.Provider
{
    public interface ITokenProvider
    {
        public string GenerateJWTToken(User user);
    }
}
