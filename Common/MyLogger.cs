//using log4net;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CoreInvestmentTracker.Common
{
    /// <summary>
    /// 
    /// </summary>
    public class MyLogger : IMyLogger
    {
        /// <summary>
        /// 
        /// </summary>
        public readonly ILogger logger;
        /// <summary>
        /// 
        /// </summary>
        public readonly ILoggerFactory Factory;
        /// <summary>
        /// 
        /// </summary>
        /// <param name="factory"></param>
        public MyLogger(ILoggerFactory factory)
        {
            Factory = factory;
            logger = Factory.CreateLogger("Global");
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="message"></param>
        public void Debug(string message)
        {
            logger.LogInformation(message);            
        }
    }
}