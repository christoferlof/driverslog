using System.Runtime.Serialization;

namespace Driverslog.Models {
    [DataContract]
    public class Trip {
        
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
    }
}