using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
//using System.Web.Mvc;
using CoreInvestmentTracker.Common;
using CoreInvestmentTracker.Models.DAL;
using Microsoft.AspNetCore.Mvc;

namespace CoreInvestmentTracker.Controllers
{
    [GlobalLogging]
    public class HomeController : Controller
    {
        // GET: /home/index
        public IActionResult Index()
        {
            return View(); // look for a view whos name matches matches the method
        }
    }
}