using Caliburn.Micro;
using Driverslog.Services;
using Driverslog.ViewModels;

namespace Driverslog.Tests.Unit.ViewModels.Edit {
    public class TestableEditExpenseViewModel : EditExpenseViewModel {
        public TestableEditExpenseViewModel(INavigationService navigationService, IMessageBoxService messageBoxSerice) :
            base(navigationService,messageBoxSerice){
        }

        public void Initialize() {
            OnInitialize();
        }
    }
}