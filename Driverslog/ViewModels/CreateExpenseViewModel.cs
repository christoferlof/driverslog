﻿using System;
using System.Linq;
using Caliburn.Micro;
using Driverslog.Models;
using Driverslog.Services;

namespace Driverslog.ViewModels {
    public class CreateExpenseViewModel : ExpenseScreen {
        private readonly INavigationService _navigationService;

        public CreateExpenseViewModel(INavigationService navigationService,
            IMessageBoxService messageBoxService, ITrialService trialService, IAnalyticsService analyticsService) 
            : base(messageBoxService, trialService, analyticsService){
            _navigationService = navigationService;
        }

        public void CreateExpense() {

            if (IsTrialLimitReached()) return;
            
            var expense = new Expense {
                Car     = Car,
                Date    = Date,
                Title   = Title,
                Notes   = Notes,
                Amount  = Amount
            };

            if (!IsValid(expense)) return;

            Expense.Add(expense);
            Expense.SaveChanges();

            AnalyticsService.LogEvent("Expense.Create");
            _navigationService.GoBack();
        }
    }
}