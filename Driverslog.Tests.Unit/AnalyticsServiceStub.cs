using System;
using System.Collections.Generic;
using Driverslog.Services;

namespace Driverslog.Tests.Unit {
    public class AnalyticsServiceStub : IAnalyticsService {
        public bool LogEventWasInvoked;
        public void LogEvent(string eventName) {
            LogEventWasInvoked = true;
        }

        public void LogEvent(string eventName, Dictionary<string,object> parameters) {
            LogEventWasInvoked = true;
        }
    }
}