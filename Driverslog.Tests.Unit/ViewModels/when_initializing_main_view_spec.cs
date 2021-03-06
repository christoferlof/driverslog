using System;
using System.Linq;
using Driverslog.Models;
using Driverslog.ViewModels;
using Victoria.Test;

namespace Driverslog.Tests.Unit.ViewModels {
    public class when_initializing_main_view_spec : ContextSpecification {

        protected MainPageViewModel PageViewModel;
        protected NavigationServiceStub NavigationService;

        public override void Context() {
            NavigationService = new NavigationServiceStub();

            Trip.All.Clear();
            Trip.AddFirst(new Trip { From = "first", Date = DateTime.Now.AddDays(-2).Date }); //3
            Trip.AddFirst(new Trip { From = "second", Date = DateTime.Now.Date }); //1
            Trip.SaveChanges();

            Expense.Clear();
            Expense.Add(new Expense { Title = "first", Date = DateTime.Now.AddDays(-1).Date }); //2
            Expense.Add(new Expense { Title = "second", Date = DateTime.Now.AddDays(-4).Date }); //4
            Expense.SaveChanges();
        }

        public override void Because() {
            PageViewModel = new MainPageViewModel(NavigationService,null,new AnalyticsServiceStub());
        }

        [Fact]
        public void should_list_all_existing_trips() {
            Assert.Equal(2, PageViewModel.TripList.Count());
        }

        [Fact]
        public void should_set_selected_index_to_none() {
            Assert.Equal(-1, PageViewModel.SelectedIndex);
        }

        [Fact]
        public void should_display_newest_trip_first() {
            Assert.Equal("second", PageViewModel.TripList.First().From);
        }

        [Fact]
        public void should_list_all_existing_expenses() {
            Assert.Equal(2, PageViewModel.ExpenseList.Count());
        }


    }
}