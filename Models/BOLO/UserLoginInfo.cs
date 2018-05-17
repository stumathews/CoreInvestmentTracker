using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CoreInvestmentTracker.Models.BOLO
{
    /// <summary>
    /// Represents User login information
    /// </summary>
    public class UserLoginInfo : IBasicUserDetails
    {
        /// <summary>
        /// user name 
        /// </summary>
        public string Username { get; set; }

        /// <summary>
        /// Token 
        /// </summary>
        public string Password { get; set; }
    }

    public interface IBasicUserDetails
    {
        
        /// <summary>
        /// user name 
        /// </summary>
        string Username { get; set; }

        /// <summary>
        /// Token 
        /// </summary>
        string Password { get; set; }
    }

    /// <summary>
    /// Signup details for a new user
    /// </summary>
    public class SignupDetails : IBasicUserDetails
    {
        /// <summary>
        /// user name 
        /// </summary>
        public string Username { get; set; }

        /// <summary>
        /// Token 
        /// </summary>
        public string Password { get; set; }

        /// <summary>
        /// Users Email
        /// </summary>
        public string Email { get; set; }

        /// <summary>
        /// User's timezone
        /// </summary>
        public int Timezone { get; set; }
    }
}
