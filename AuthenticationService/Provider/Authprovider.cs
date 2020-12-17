using AuthenticationService.Models;
using AuthenticationService.Repositories;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Threading.Tasks;

namespace AuthenticationService.Provider
{
	public class Authprovider : IAuthProvider
	{
		private readonly IAuthRepo _authRepository;
		private readonly ITokenProvider _tokenProvider;
		private log4net.ILog _logger = log4net.LogManager.GetLogger(typeof(Authprovider));

		public Authprovider(IAuthRepo authRepository, ITokenProvider tokenProvider)
		{
			this._authRepository = authRepository;
			this._tokenProvider = tokenProvider;
		}

		public List<User> getAllUsers()
		{
			try
			{
				return _authRepository.getAllUsers();	

			}
			catch (Exception)
			{

				throw;
			}
		}

		public string LoginProvider(User model)
		{
			string token = null;
			try
			{
				bool authorizeSucceed = _authRepository.UserLogin(model);
				if (authorizeSucceed)
				{
					_logger.Info(model.Username + " Authenticated successfully");
					token = _tokenProvider.GenerateJWTToken(model);
				}
				else
				{
					_logger.Warn(model.Username + " Attempted to login with invalid credentials");
				}
			}
			catch (Exception ex)
			{
				_logger.Warn("Some exception occured while validating user/generation JWT token as follow " + ex.Message);
			}
			return token;
		}
	}
}
