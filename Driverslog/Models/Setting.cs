using System.Linq;
using System.Runtime.Serialization;

namespace Driverslog.Models {
    [DataContract]
    public class Setting : ActiveRecord<Setting>{

        private static Setting _current;

        public static Setting Current { 
            get {
                EnsureCurrent();
                return _current;
            }
        }

        private static void EnsureCurrent() {
            if (_current == null) {
                LoadCurrent();
                if(_current == null) {
                    CreateCurrent();
                }
            }
        }

        private static void LoadCurrent() {
            Load();
            _current = All.FirstOrDefault();
        }

        private static void CreateCurrent() {
            _current = new Setting();
        }

        [DataMember]
        public string Email { get; set; }

    }
}