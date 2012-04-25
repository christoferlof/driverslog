using Caliburn.Micro;
using Driverslog.Services;
using Driverslog.ViewModels;

namespace Driverslog.Tests.Unit.ViewModels.Edit {
    public class TestableEditExpenseViewModel : EditExpenseViewModel {
        
        public TestableEditExpenseViewModel() : 
            base(new NavigationServiceStub(), new MessageBoxServiceStub(), new AnalyticsServiceStub()){}
        
        public TestableEditExpenseViewModel(INavigationService navigationService, IMessageBoxService messageBoxSerice, IAnalyticsService analyticsService) :
            base(navigationService,messageBoxSerice, analyticsService){
        }

        public void Initialize() {
            OnInitialize();
        }
    }
}