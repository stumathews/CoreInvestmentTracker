using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CoreInvestmentTracker.Models.BOLO
{
    public class TokenResponse
    {
        public string Token;

        /// <summary>Initializes a new instance of the <see cref="T:System.Object"></see> class.</summary>
        public TokenResponse(string token)
        {
            Token = token;
        }
    }
}
