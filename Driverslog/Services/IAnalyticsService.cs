using System.Collections.Generic;

namespace Driverslog.Services {
    public interface IAnalyticsService {
        void LogEvent(string eventName);
        void LogEvent(string eventName, Dictionary<string,object> parameters);
    }
}