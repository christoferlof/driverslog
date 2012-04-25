namespace Driverslog.Services {
    public class AnalyticsService : IAnalyticsService {
        public void LogEvent(string eventName) {
            FlurryWP7SDK.Api.LogEvent(eventName);
        }
    }
}