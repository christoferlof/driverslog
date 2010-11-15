using System;
using Driverslog.Services;

namespace Driverslog.Tests.Unit {
    public class MessageBoxServiceStub : IMessageBoxService {
        public bool ShowMessageWasInvoked;

        public void ShowMessage(string message) {
            ShowMessageWasInvoked = true;
        }
    }
}