using System.Linq;
using System.Runtime.Serialization;

namespace Driverslog.Models {
    [DataContract]
    public class Setting : ActiveRecord<Setting>{

        private static Setting _current;

        public static Setting Current { 
            get {
                if(_current == null) {
                    Load();
                    _current = All.FirstOrDefault();
                }
                return _current;
            }
        }

        [DataMember]
        public string Email { get; set; }

    }
}