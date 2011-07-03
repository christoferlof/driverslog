using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Globalization;
using System.Linq;
using System.Linq.Expressions;
using System.Runtime.Serialization;
using Caliburn.Micro;
using Victoria.Data;

namespace Driverslog.Models {
    [DataContract]
    public class Trip : LogItem<Trip> {

        [Obsolete]
        public static void AddFirst(Trip trip) {
            All.Insert(0, trip);
        }

        private string _from;

        [DataMember]
        public string From {
            get { return _from; }
            set {
                _from = value;
                Notify(() => From);
            }
        }

        private string _to;

        [DataMember]
        public string To {
            get { return _to; }
            set {
                _to = value;
                Notify(() => To);
            }
        }

        private int _odometerStart;

        [DataMember]
        public int OdometerStart {
            get { return _odometerStart; }
            set {
                _odometerStart = value;
                Notify(() => OdometerStart);
                Notify(() => Distance);
            }
        }

        private int _odometerStop;

        [DataMember]
        public int OdometerStop {
            get { return _odometerStop; }
            set {
                _odometerStop = value;
                Notify(() => OdometerStop);
                Notify(() => Distance);
            }
        }

        
        [IgnoreDataMember]
        public string Distance {
            get {
                return (HasValidDistance()) ? FormatDistance() : "Unknown distance";
            }
        }

        private int _milage;

        [DataMember(IsRequired=false, Order=1)]
        public int Mileage {
            get { return _milage; }
            set {
                _milage = value;
                Notify(() => Mileage);
                Notify(() => Distance);
            }
        }

        private string FormatDistance() {
            int distance = (Mileage > 0) ? Mileage : OdometerStop - OdometerStart;
            return string.Format("{0} {1}",distance,Setting.Current.DistanceUnit);
        }

        private bool HasValidDistance() {
            return 
                    (Mileage > 0) || (
                    OdometerStop - OdometerStart > 0 &&
                    OdometerStart > 0 &&
                    OdometerStop > 0);
        }

        protected override void OnValidating() {
            if (string.IsNullOrEmpty(From)) {
                ValidationMessages.Add("From", "You must specify where you're traveling from.");
            }
        }
    }
}