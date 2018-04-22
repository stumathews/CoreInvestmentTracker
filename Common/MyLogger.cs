//using log4net;
using Microsoft.Extensions.Logging;

namespace CoreInvestmentTracker.Common
{
    /// <inheritdoc />
    /// <summary>
    /// </summary>
    public class MyLogger : IMyLogger
    {
        /// <summary>
        /// Internal access to logger
        /// </summary>
        private readonly ILogger _logger;

        /// <summary>
        /// Construct a logger
        /// </summary>
        /// <param name="factory"></param>
        public MyLogger(ILoggerFactory factory)
        {
            _logger = factory.CreateLogger("Global");
        }
        /// <inheritdoc />
        /// <summary>
        /// Prints debug messages
        /// </summary>
        /// <param name="message"></param>
        public void Debug(string message)
        {
            _logger.LogInformation(message);            
        }
    }
}