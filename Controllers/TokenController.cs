using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CoreInvestmentTracker.Models.BOLO;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;

namespace CoreInvestmentTracker.Controllers
{
    /// <inheritdoc />
    /// <summary>
    /// Deals with login and authentication 
    /// </summary>
    [Produces("application/json")]
    [Route("api/Token")]
    public class TokenController : Controller
    {
        private readonly IConfiguration _config;

        /// <inheritdoc />
        public TokenController(IConfiguration config)
        {
            _config = config;
        }
        
        /// <summary>
        /// Creates a token once it validates the login details
        /// </summary>
        /// <param name="login">login details</param>
        /// <returns>A token or UnAuthorized</returns>
        [AllowAnonymous]
        [HttpPost]
        public IActionResult CreateToken([FromBody]UserLoginInfo login)
        {
            IActionResult response = Unauthorized();
            var user = Authenticate(login);

            if (user == null) return response;
            var tokenString = BuildToken(user);
            response = Ok(new TokenResponse(tokenString));

            return response;
        }

        private string BuildToken(UserModel user)
        {
            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_config["Jwt:Key"]));
            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

            var token = new JwtSecurityToken(
                            _config["Jwt:Issuer"], _config["Jwt:Issuer"], 
                            expires: DateTime.Now.AddMinutes(30),
                            signingCredentials: creds);

            return new JwtSecurityTokenHandler().WriteToken(token);
        }

        private static UserModel Authenticate(UserLoginInfo login)
        {
            UserModel user = null;

            if (login.Username == "stuart" && login.Password == "secret")
            {
                user = new UserModel { Name = "Stuart Mathews", Email = "stumathews@gmail.com"};
            }
            return user;
        }

        
        private class UserModel
        {
            public string Name { get; set; }
            public string Email { get; set; }
            public DateTime Birthdate { get; set; }
        }
    }
    
}