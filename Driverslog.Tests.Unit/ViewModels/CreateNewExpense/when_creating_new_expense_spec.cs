using Driverslog.ViewModels;
using Victoria.Test;

namespace Driverslog.Tests.Unit.ViewModels.CreateNewExpense {
    public class when_creating_new_expense_spec : ContextSpecification {

        protected MainPageViewModel     PageViewModel;
        protected NavigationServiceStub NavigationService;

        public override void Context() {
            NavigationService   = new NavigationServiceStub();
            PageViewModel       = new MainPageViewModel(NavigationService,null, new AnalyticsServiceStub());
        }

        public override void Because() {
            PageViewModel.CreateNewExpense();
        }

        [Fact]
        public void should_navigate_to_create_page() {
            Assert.True(NavigationService.NavigateUri.OriginalString.Contains("CreateExpenseView.xaml"));
        }

    }
}