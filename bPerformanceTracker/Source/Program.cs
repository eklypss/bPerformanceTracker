using bPerformanceTracker.Helpers;
using bPerformanceTracker.Source.Config;
using bPerformanceTracker.Source.Enum;
using bPerformanceTracker.Source.Helpers;
using System;
using System.Timers;

namespace bPerformanceTracker
{
    internal class Program
    {
        private Logger logger;
        private PerformanceManager performanceManager;
        private Timer mainTimer;

        private static void Main(string[] args) => new Program().Start();

        public void Start()
        {
            logger = new Logger();
            performanceManager = new PerformanceManager();
            mainTimer = new Timer()
            {
                AutoReset = true,
                Interval = Config.UpdateInterval,
                Enabled = false
            };
            mainTimer.Elapsed += _mainTimer_Elapsed;
            mainTimer.Start();
            logger.LogMessage(LogType.Normal, "Starting..");

            Console.ReadKey();
        }

        private void _mainTimer_Elapsed(object s, ElapsedEventArgs e)
        {
            GetPerformanceData();
        }

        private void GetPerformanceData()
        {

        }
    }
}