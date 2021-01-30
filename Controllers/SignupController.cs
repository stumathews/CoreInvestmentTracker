using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CoreInvestmentTracker.Common;
using CoreInvestmentTracker.Models.BOLO;
using CoreInvestmentTracker.Models.DAL;
using CoreInvestmentTracker.Models.DEL;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace CoreInvestmentTracker.Controllers
{
    /// <summary>
    /// Add a new user
    /// </summary>
    [Produces("application/json")]
    [Route("api/Signup")]
    public class SignupController : Controller
    {
        /// <summary>
        /// Data Access layer
        /// </summary>
        public InvestmentDbContext Db { get; }

        /// <inheritdoc />
        public SignupController(InvestmentDbContext db)
        {
            Db = db;
        }
        
        /// <summary>
        /// Creates a new user
        /// </summary>
        /// <param name="signupDetails"></param>
        /// <returns></returns>
        [HttpPost]
        [AllowAnonymous]
        public IActionResult Signup([FromBody]SignupDetails signupDetails)
        {
            var user = new User
            {
                DisplayName = signupDetails.Username,
                Password = "",
                TimeZone = signupDetails.Timezone,
                UserName = signupDetails.Username,
                Email = signupDetails.Email
            };

            Db.Users.Add(user);
            Db.SaveChanges();

            var savedUser = Db.Users.FirstOrDefault(u => u.UserName.Equals(user.UserName));
            var activity = new RecordedActivity(ActivityOperation.Create.ToString(),"Creates an entity", user, "", $"Created new user '{user.UserName}'", DateTimeOffset.UtcNow, savedUser.Id, EntityType.User);
            Db.RecordedActivities.Add(activity);
            Db.SaveChanges();
            return null;
        }
    }
}