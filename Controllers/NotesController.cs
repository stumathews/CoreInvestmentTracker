using CoreInvestmentTracker.Common;
using CoreInvestmentTracker.Models;
using CoreInvestmentTracker.Models.DAL.Interfaces;
using CoreInvestmentTracker.Models.DEL;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Linq;
using CoreInvestmentTracker.Common.ActionFilters;

namespace CoreInvestmentTracker.Controllers
{
    /// <inheritdoc />
    /// <summary>
    /// Controller for Notes
    /// </summary>
    [Route("api/[controller]")]
    [GlobalControllerLogging]
    public class NotesController : RefersToAnEntityControllerFunctionality<InvestmentNote>
    {
        /// <inheritdoc />
        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="db"></param>
        /// <param name="logger"></param>
        public NotesController(IEntityApplicationDbContext<InvestmentNote> db, IMyLogger logger) 
            : base(db)
        {

        }
    }
}