namespace CoreInvestmentTracker.Common
{
    /// <summary>
    /// A logging interface
    /// </summary>
    public  interface IMyLogger
    {
        /// <summary>
        /// Log a debug message
        /// </summary>
        /// <param name="message"></param>
        void Debug(string message);
    }
}