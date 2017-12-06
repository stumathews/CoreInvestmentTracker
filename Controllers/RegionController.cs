using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Web;
//using System.Web.Mvc;
using CoreInvestmentTracker.Common;
using CoreInvestmentTracker.Models;
using CoreInvestmentTracker.Models.DAL;
using CoreInvestmentTracker.Models.DAL.Interfaces;

namespace CoreInvestmentTracker.Controllers
{
    [GlobalLogging]
    /// <summary>
    /// This automaticall maps /controller/method such as /region/xvz
    /// </summary>
    public class RegionController : EntityManagedController<Models.Region>
    {
        public RegionController(IEntityApplicationDbContext<Region> entityApplicationDbContext) : base(entityApplicationDbContext)
        {
        }
    }
}