using System;
using Driverslog.Services;

namespace Driverslog.Tests.Unit {
    public class AnalyticsServiceStub : IAnalyticsService {
        public bool LogEventWasInvoked;
        public void LogEvent(string eventName) {
            LogEventWasInvoked = true;
        }
    }
}