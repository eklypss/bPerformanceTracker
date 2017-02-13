using bPerformanceTracker.Source.Config;
using bPerformanceTracker.Source.Enum;
using System;

namespace bPerformanceTracker.Helpers
{
    internal class Logger
    {
        private static Logger instance;

        public Logger()
        {
        }

        public static Logger Instance
        {
            get
            {
                if (instance == null)
                {
                    instance = new Logger();
                }
                return instance;
            }
        }

        public void LogMessage(LogType logType, string logMessage)
        {
            if (logType == LogType.Debug)
            {
                if (Config.DebugEnabled)
                {
                    Console.WriteLine("[{0}][{1}] {2}", DateTime.Now.ToShortTimeString(), logType.ToString(), logMessage);
                }
            }
            else
            {
                Console.WriteLine("[{0}][{1}] {2}", DateTime.Now.ToShortTimeString(), logType.ToString(), logMessage);
            }
        }
    }
}