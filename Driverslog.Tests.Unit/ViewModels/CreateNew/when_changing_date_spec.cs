using System;
using Driverslog.Services;
using Driverslog.ViewModels;
using Microsoft.Phone.Controls;
using Telerik.Windows.Controls;
using Victoria.Test;

namespace Driverslog.Tests.Unit.ViewModels.CreateNew {
    public class when_changing_date_spec {
        
        [Fact]
        public void should_update_date() {
            var viewModel = new CreateViewModel(new NavigationServiceStub(), new MessageBoxServiceStub(), null, new AnalyticsServiceStub());
            var date = new DateTime(1979,1,3);

            viewModel.ChangeDate(new ValueChangedEventArgs<object>(null, date));

            Assert.Equal(date,viewModel.Date);
        }
    }
}