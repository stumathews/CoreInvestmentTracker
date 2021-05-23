using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using CoreInvestmentTracker.Models.BOLO;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using CoreInvestmentTracker.Models.DAL;
using CoreInvestmentTracker.Models.DEL;
using CoreInvestmentTracker.Common;
using CoreInvestmentTracker.Models.DAL.Interfaces;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;

namespace CoreInvestmentTracker.Controllers
{
    /// <inheritdoc />
    /// <summary>
    /// Deals with login and authentication 
    /// </summary>
    [Produces("application/json")]
    [Route("api/[controller]")]
    public class TokenController : Controller
    {
        /// <summary>
        /// Data Access
        /// </summary>
        public InvestmentDbContext Db { get; }

        /// <summary>
        /// Configuration
        /// </summary>
        private readonly IConfiguration _config;        
        
        /// <inheritdoc />
        public TokenController(IConfiguration config, InvestmentDbContext db)
        {
            Db = db;
            _config = config;
        }
        
        /// <summary>
        /// Creates a token once it validates the login details
        /// </summary>
        /// <param name="login">login details</param>
        /// <returns>A token or UnAuthorized</returns>
        [AllowAnonymous]
        [HttpPost]
        [ApiConventionMethod(typeof(DefaultApiConventions), 
                     nameof(DefaultApiConventions.Post))]
        public ActionResult<TokenResponse> CreateToken([FromBody]UserLoginInfo login)
        {
            var response = Unauthorized();
            var user = Authenticate(login);

            if (user == null) return response;
            var tokenString = BuildToken(user);

            return new TokenResponse(tokenString);
        }

        private string BuildToken(UserModel user)
        {

            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_config["Jwt:Key"]));
            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);
            var claims = new[]
            {
                new Claim(JwtRegisteredClaimNames.Sub, user.Name),
                new Claim(JwtRegisteredClaimNames.Email, user.Email),
                new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString())
            };

            var token = new JwtSecurityToken(
                            _config["Jwt:Issuer"],
                            _config["Jwt:Issuer"],
                            claims,
                            expires: DateTime.Now.AddMinutes(30),
                            signingCredentials: creds);

            return new JwtSecurityTokenHandler().WriteToken(token);
        }

        private UserModel Authenticate(UserLoginInfo login)
        {
            UserModel user = new UserModel();
            var dbuser = Db.Users.Where(u => u.UserName.Equals(login.Username))
                .FirstOrDefault();
            user.Name = dbuser.UserName;
            user.TimeZone = dbuser.TimeZone;
            user.Email = dbuser.Email;
            return user;
        }
                
        private class UserModel
        {
            public string Name { get; set; }
            public string Email { get; set; }
            public DateTime Birthdate { get; set; }
            public int TimeZone { get;set; }
        }
    }
    
}