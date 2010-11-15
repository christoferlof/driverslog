using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Runtime.Serialization;
using Victoria.Data;

namespace Driverslog.Models {
    [DataContract]
    public class Trip : ActiveRecord<Trip> {

        public static void AddFirst(Trip trip) {
            All.Insert(0,trip);
        }

        //public static IEnumerable<Trip> ByDateDescending() {
        //    return All.OrderByDescending(x => x.Date);
        //}

        public Trip() {
            Id = Guid.NewGuid();
            Date = DateTime.Now.Date;
        }

        [DataMember]
        public Guid Id { get; set; }

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
        
        [DataMember]
        public DateTime Date { get; set; }

        private string FormatDistance() {
            return (OdometerStop - OdometerStart).ToString(CultureInfo.InvariantCulture);
        }

        private bool HasValidDistance() {
            return  OdometerStop - OdometerStart > 0 && 
                    OdometerStart > 0 && 
                    OdometerStop > 0;
        }

        public bool IsValid() {
            return !string.IsNullOrEmpty(From);
        }
    }
}