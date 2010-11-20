using System;
using Caliburn.Micro;
using Driverslog.Services;
using Driverslog.ViewModels;

namespace Driverslog.Tests.Unit.ViewModels.Edit {
    public class TestableEditViewModel : EditViewModel {
        private readonly IMessageBoxService _messageBoxService;

        public TestableEditViewModel(INavigationService navigationService, IMessageBoxService messageBoxService) 
            : base(navigationService,messageBoxService) {
            _messageBoxService = messageBoxService;
        }

        public void Initialize() {
            OnInitialize();
        }
    }
}