using System;
using Driverslog.Models;
using Microsoft.Phone.Marketplace;
using Microsoft.Phone.Tasks;

namespace Driverslog.Services {
    public class TrialService : ITrialService {

        private readonly bool _isTrial;

        public TrialService() {
            _isTrial = new LicenseInformation().IsTrial();
        }

        public int Limit {
            get { return 15; }
        }

        public bool LimitReached(){
            return ( (_isTrial) && ((Trip.All.Count + Expense.All.Count) >= Limit));
        }

        public void Buy() {
            new MarketplaceDetailTask().Show();
        }
    }
}