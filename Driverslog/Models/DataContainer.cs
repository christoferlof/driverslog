using System.Collections.Generic;
using System.Runtime.Serialization;

namespace Driverslog.Models {
    [DataContract]
    public class DataContainer<TRecord> {

        public DataContainer() {
            Records = new List<TRecord>();
        }

        [DataMember]
        public List<TRecord> Records { get; set; }
    }
}