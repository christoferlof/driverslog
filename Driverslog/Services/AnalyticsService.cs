using System.Collections.Generic;
using System.Linq;
using FlurryWP7SDK.Models;

namespace Driverslog.Services {
    public class AnalyticsService : IAnalyticsService {
        public void LogEvent(string eventName) {
            FlurryWP7SDK.Api.LogEvent(eventName);
        }

        public void LogEvent(string eventName, Dictionary<string,string> parameters) {
            var eventParameters = parameters.Select(param => 
                new Parameter(param.Key, param.Value)
            ).ToList();
            FlurryWP7SDK.Api.LogEvent(eventName, eventParameters);
        }
    }
}