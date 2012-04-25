using System;
using Caliburn.Micro;
using Driverslog.Services;
using Driverslog.ViewModels;

namespace Driverslog.Tests.Unit.ViewModels.Edit {
    public class TestableEditViewModel : EditViewModel {
        private readonly IMessageBoxService _messageBoxService;

        public TestableEditViewModel() : 
            base(new NavigationServiceStub(), new MessageBoxServiceStub(), new AnalyticsServiceStub()) {}

        public TestableEditViewModel(INavigationService navigationService, IMessageBoxService messageBoxService, IAnalyticsService analyticsService) 
            : base(navigationService,messageBoxService, analyticsService) {
            _messageBoxService = messageBoxService;
        }

        public void Initialize() {
            OnInitialize();
        }
    }
}