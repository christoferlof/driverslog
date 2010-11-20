using System;
using Driverslog.Services;

namespace Driverslog.Tests.Unit {
    public class MessageBoxServiceStub : IMessageBoxService {
        public bool ShowMessageWasInvoked;
        
        public void ShowMessage(string message) {
            ShowMessageWasInvoked = true;
        }

        public bool ConfirmReturns = true;
        public bool ConfirmWasInvoked;
        public bool Confirm(string message) {
            ConfirmWasInvoked = true;
            return ConfirmReturns;
        }


    }
}