using bPerformanceTracker.Source.Models;
using System.Collections.Generic;
using System.Diagnostics;

namespace bPerformanceTracker.Source.Helpers
{
    internal class PerformanceManager
    {
        private static PerformanceManager instance;
        private PerformanceCounter performanceMonitorRAM;
        private PerformanceCounter performanceMonitorCPU;

        public PerformanceManager()
        {
            performanceMonitorRAM = new PerformanceCounter("Memory", "Available MBytes", true);
            performanceMonitorCPU = new PerformanceCounter("Processor", "% Processor Time", "_Total", true);
        }

        private PerformanceManager Instance
        {
            get
            {
                if (instance == null)
                {
                    instance = new PerformanceManager();
                }
                return instance;
            }
        }

        public List<PerformanceCounterCategory> GetAvailableCategories()
        {
            List<PerformanceCounterCategory> availableCategoriesList = new List<PerformanceCounterCategory>();
            var availableCategories = PerformanceCounterCategory.GetCategories();
            availableCategoriesList.AddRange(availableCategories);
            return availableCategoriesList;
        }

        public List<PerformanceCounter> GetCounterTypesForCategory(PerformanceCounterCategory category)
        {
            List<PerformanceCounter> availableCountersList = new List<PerformanceCounter>();
            var availableCounters = category.GetCounters();
            availableCountersList.AddRange(availableCounters);
            return availableCountersList;
        }

        public PerformanceDataEntry GetPerformanceData()
        {
            return new PerformanceDataEntry()
            {
                MemoryAvailable = performanceMonitorRAM.NextValue(),
                ProcessorUsage = performanceMonitorCPU.NextValue()
            };
        }
    }
}