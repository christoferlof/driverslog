using System.Collections.Generic;
using System.Runtime.Serialization;

namespace Driverslog.Models {
    [DataContract]
    public class TripList {

        public TripList() {
            Trips = new List<Trip>();
        }

        [DataMember]
        public List<Trip> Trips { get; set; }
    }
}