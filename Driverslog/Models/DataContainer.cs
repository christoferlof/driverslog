using System.Collections.Generic;
using System.Runtime.Serialization;

namespace Driverslog.Models {
    [DataContract]
    public class DataContainer<TEntity> {

        public DataContainer() {
            Records = new List<TEntity>();
        }

        [DataMember]
        public List<TEntity> Records { get; set; }
    }
}