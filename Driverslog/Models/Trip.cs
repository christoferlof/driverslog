using System;
using System.Collections.ObjectModel;
using System.Globalization;
using System.IO;
using System.IO.IsolatedStorage;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Json;
using Victoria.Data;

namespace Driverslog.Models {
    [DataContract]
    public class Trip : ActiveRecord<Trip> {

        [DataMember]
        public string From { get; set; }

        [DataMember]
        public string To { get; set; }

        [DataMember]
        public int OdometerStart { get; set; }

        [DataMember]
        public int OdometerStop { get; set; }

        [DataMember]
        public string Notes { get; set; }

        [DataMember]
        public string Car { get; set; }

        [IgnoreDataMember]
        public string Distance { 
            get {
                return (HasValidDistance()) ? FormatDistance() : "Unknown distance";
            }
        }

        private string FormatDistance() {
            return (OdometerStop - OdometerStart).ToString(CultureInfo.InvariantCulture);
        }

        private bool HasValidDistance() {
            return  OdometerStop - OdometerStart > 0 && 
                    OdometerStart > 0 && 
                    OdometerStop > 0;
        }
    }
}