using AuthenticationService.Controllers;
using AuthenticationService.Models;
using AuthenticationService.Provider;
using AuthenticationService.Repositories;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Moq;
using NUnit.Framework;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;

namespace AuthenticationServiceTest
{
    public class Tests
    {
        private AuthController _AuthController;
        private Mock<IAuthProvider> _AuthProviderMock;
        private string token="token";
        private string tokennull=null;
        //private JwtSecurityToken token = new JwtSecurityToken();
       // private JwtSecurityToken nullToken = null;

        private Mock<IConfiguration> _config;
        private Mock<IAuthRepo> _AuthRepository;
        private IAuthProvider _AuthProvider;
        private Mock<ITokenProvider> _TokenProvider;
        public static List<User> Users;

        [SetUp]
        public void Setup()
        {
            _AuthProviderMock = new Mock<IAuthProvider>();
            _AuthController = new AuthController(_AuthProviderMock.Object);
            _config = new Mock<IConfiguration>();
            _config.Setup(s => s["Jwt:Key"]).Returns("AuthenticationServiceSecretKey");
            _AuthRepository = new Mock<IAuthRepo>();
            _TokenProvider = new Mock<ITokenProvider>();

            _TokenProvider.Setup(s => s.GenerateJWTToken(It.IsAny<User>())).Returns(token);

            _AuthProvider = new Authprovider(_AuthRepository.Object, _TokenProvider.Object);
           Users = new List<User>()
           {
               new User(){UserId=101, Username="srinu1120",Password="nani",Role="Employee"},
               new User(){ UserId=2, Username="srinus",Password="nani",Role="Customer"},
             


           };

    }

        [Test]
        public void AuthRepo_UserLogin_PassTest()
        {
            IAuthRepo authRepo = new AuthRepository();
            User user = new User()
            {
                UserId = 1,
                Username = "srinu",
                Password = "nani",
                Role = "Employee"
            };
            var actualValue= authRepo.UserLogin(user);
            Assert.IsTrue(actualValue);

        }

        [Test]
        public void AuthRepo_UserLogin_FailTest()
        {
            IAuthRepo authRepo = new AuthRepository();
            User user = new User()
            {
                UserId = 2,
                Username = "srinu",
                Password = "nanisdfsd",
                Role = "Employee"
            };
            var actualValue = authRepo.UserLogin(user);
            Assert.IsFalse(actualValue);

        }

        [Test]
        public void AuthProvider_ValidData_LoginProviderMethod_PassTest_ReturnsJwtToken()
        {
            //_AuthProviderMock.Setup(s => s.LoginProvider(It.IsAny<User>())).Returns(token);
            _AuthRepository.Setup(s => s.UserLogin(It.IsAny<User>())).Returns(true);
            User user = new User()
            {
                UserId = 1,
                Username = "srinu",
                Password = "nani",
                Role = "Employee"
            };
            var login= _AuthProvider.LoginProvider(user);
            Assert.IsNotNull(login);
        }
        [Test]
        public void AuthProvider_InValidData_LoginProviderMethod_FailTest_ReturnsNullJwtToken()
        {
            //_AuthProviderMock.Setup(s => s.LoginProvider(It.IsAny<User>())).Returns(tokennull);
            _AuthRepository.Setup(s => s.UserLogin(It.IsAny<User>())).Returns(false);
            User user = new User()
            {
                UserId = 2,
                Username = "srinu",
                Password = "nani",
                Role = "Employee"
            };
            var login = _AuthProvider.LoginProvider(user);
            Assert.IsNull(login);
        }
        [Test]
        public void AuthController_getUserMethod_ValidData_PassTest()
        {
       


            _AuthProviderMock.Setup(s => s.getAllUsers()).Returns(Users);

            User user = new User()
            { UserId = 101, Username = "srinu1120", Password = "nani", Role = "Employee" };
            var respone = _AuthController.getUser(user) as ObjectResult;
            Assert.AreEqual(200, respone.StatusCode);
        }

        [Test]
        public void AuthController_getUserMethod_InValidValidData_FailTest()
        {
          


            _AuthProviderMock.Setup(s => s.getAllUsers()).Returns(Users);

            User user = new User()
            { UserId = 10, Username = "srinu11", Password = "nani", Role = "Employee" };
            var respone = _AuthController.getUser(user) as ObjectResult;
            Assert.AreEqual(404, respone.StatusCode);
        }

        [Test]
        public void AuthController_UserLoginMethid_ValidData_PassTest()
        {

            _AuthProviderMock.Setup(s => s.LoginProvider(It.IsAny<User>())).Returns(token);
            _AuthRepository.Setup(s => s.UserLogin(It.IsAny<User>())).Returns(true);
            User user = new User()
            {
                UserId = 1,
                Username = "srinu",
                Password = "nani",
                Role = "Employee"
            };
            var respone = _AuthController.UserLogin(user) as ObjectResult ;
            Assert.AreEqual(200,respone.StatusCode);
        }

        [Test]
        public void AuthController_UserLoginMethid_InValidData_FailTest()
        {
            _AuthRepository.Setup(s => s.UserLogin(It.IsAny<User>())).Returns(false);

            _AuthProviderMock.Setup(s => s.LoginProvider(It.IsAny<User>())).Returns(tokennull);
           
            User user = new User()
            {
                UserId = 1,
                Username = "srinu",
                Password = "nani",
                Role = "Employee"
            };
            var respone = _AuthController.UserLogin(user) as StatusCodeResult;
            Assert.AreEqual(500, respone.StatusCode);
        }


  



    }
}