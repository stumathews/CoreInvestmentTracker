//using log4net;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CoreInvestmentTracker.Common
{
    public class MyLogger : IMyLogger
    {
        public readonly ILogger logger;
        public readonly ILoggerFactory Factory;
        public MyLogger(ILoggerFactory factory)
        {
            Factory = factory;
            logger = Factory.CreateLogger("Global");
        }
        public void Debug(string message)
        {
            logger.LogInformation(message);            
        }
    }
}