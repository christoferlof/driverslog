using System;
using Driverslog.Models;
using Driverslog.Services;

namespace Driverslog.Tests.Unit {
    public class TrialServiceStub : ITrialService{
        
        public int Limit {
            get;
            set;
        }

        public bool LimitReached() {
            return ((Trip.All.Count + Expense.All.Count) >= Limit);
        }

        public void Buy(){}
    }
}