using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CoreInvestmentTracker.Models.BOLO
{
    /// <summary>
    /// Represents User login information
    /// </summary>
    public class UserLoginInfo
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
}
