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

        private float highestCPUUsage;
        private float highestAvailableRam;

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
            var performanceData = performanceManager.GetPerformanceData();
            if (performanceData.MemoryAvailable >= highestAvailableRam) highestAvailableRam = performanceData.MemoryAvailable;
            if (performanceData.ProcessorUsage >= highestCPUUsage) highestCPUUsage = performanceData.ProcessorUsage;
            logger.LogMessage(LogType.Report, string.Format("Memory available: {0} (most: {1}) CPU usage: {2} (highest: {3})", performanceData.MemoryAvailable, highestAvailableRam, performanceData.ProcessorUsage, highestCPUUsage));
        }
    }
}