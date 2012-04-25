using System;
using Driverslog.Models;
using Microsoft.Phone.Marketplace;
using Microsoft.Phone.Tasks;

namespace Driverslog.Services {
    public class TrialService : ITrialService {

        private readonly IAnalyticsService _analyticsService;
        private readonly bool _isTrial;

        public TrialService(IAnalyticsService analyticsService) {
            _analyticsService = analyticsService;
            _isTrial = new LicenseInformation().IsTrial();
        }

        public bool IsTrial {
            get{ return _isTrial;}
        }

        public int Limit {
            get { return 15; }
        }

        public bool LimitReached(){
            bool limitReached = ( (_isTrial) && ((Trip.All.Count + Expense.All.Count) >= Limit));
            if(limitReached) {
                _analyticsService.LogEvent("Trial.LimitReached");        
            }
            return limitReached;
        }

        public void Buy() {
            _analyticsService.LogEvent("Trial.Buy");
            new MarketplaceDetailTask().Show();
        }
    }
}