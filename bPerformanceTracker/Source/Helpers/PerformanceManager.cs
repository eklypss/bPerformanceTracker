using bPerformanceTracker.Helpers;
using bPerformanceTracker.Source.Enum;
using bPerformanceTracker.Source.Models;
using System.Collections.Generic;
using System.Diagnostics;

namespace bPerformanceTracker.Source.Helpers
{
    internal class PerformanceManager
    {
        private static PerformanceManager instance;
        private List<PerformanceCounter> performanceMonitors;

        public PerformanceManager()
        {
            performanceMonitors = new List<PerformanceCounter>();
            var availableCategories = GetAvailableCategories();
            foreach (var category in availableCategories)
            {
                var availableCounters = GetCounterTypesForCategory(category);
                foreach (var counter in availableCounters)
                {
                    performanceMonitors.Add(new PerformanceCounter(category.CategoryName, counter.CounterName));
                    Logger.Instance.LogMessage(LogType.Debug, string.Format("Added performance monitor, category: {0}, counter: {1}", category.CategoryName, counter.CounterName));
                }
            }
            Logger.Instance.LogMessage(LogType.Debug, string.Format("{0} performance monitors were added.", performanceMonitors.Count));
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
            var instanceNames = category.GetInstanceNames();
            var availableCounters = new List<PerformanceCounterCategory>();
            foreach (var instanceName in instanceNames)
            {
                availableCountersList.AddRange(category.GetCounters(instanceName));
            }
            return availableCountersList;
        }

        public PerformanceDataEntry GetPerformanceData()
        {
            return new PerformanceDataEntry()
            {
            };
        }

        public List<PerformanceCounter> FindPerformanceCounters(string searchTerm)
        {
            return performanceMonitors.FindAll(x => x.CounterName.Contains(searchTerm));
        }
    }
}