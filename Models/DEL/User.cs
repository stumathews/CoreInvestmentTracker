using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace CoreInvestmentTracker.Models.DEL
{
    /// <summary>
    /// Represents a user 
    /// </summary>
    public class User
    {
        /// <summary>
        /// Id
        /// </summary>
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; } 

        /// <summary>
        /// User's display name
        /// </summary>
        public string DisplayName { get; set; }
        
        /// <summary>
        /// Users displayName
        /// </summary>
        public string UserName { get; set; }

        /// <summary>
        /// Password
        /// </summary>
        public string Password {get; set; }

        /// <summary>
        /// User's timezone in relation to UTC in which  ie +1 or -4 
        /// </summary>
        public int TimeZone { get; set; }
    }
}
