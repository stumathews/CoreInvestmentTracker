using CoreInvestmentTracker.Common;
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