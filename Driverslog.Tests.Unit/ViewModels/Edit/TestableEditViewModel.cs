using System;
using Caliburn.Micro;
using Driverslog.ViewModels;

namespace Driverslog.Tests.Unit.ViewModels.Edit {
    public class TestableEditViewModel : EditViewModel {
        public TestableEditViewModel(INavigationService navigationService) : base(navigationService) {
        }

        public void Initialize() {
            OnInitialize();
        }
    }
}