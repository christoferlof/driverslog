using System.Collections.Generic;
using System.Linq;
using Microsoft.ApplicationInsights.Telemetry.WindowsStore;

namespace Driverslog.Services {
    public class AnalyticsService : IAnalyticsService {
        public void LogEvent(string eventName) {
           ClientAnalyticsChannel.Default.LogEvent(eventName);
        }

        public void LogEvent(string eventName, Dictionary<string,object> parameters) {
            ClientAnalyticsChannel.Default.LogEvent(eventName, parameters);
        }
    }
}