//using log4net;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CoreInvestmentTracker.Common
{
    public class MyLogger : IMyLogger
    {
        //private readonly ILog logger;
        //public MyLogger(ILog logger)
        //{
        //    this.logger = logger;
        //}
        public void Debug(string message)
        {
            //logger.Debug(message);
        }
    }
}