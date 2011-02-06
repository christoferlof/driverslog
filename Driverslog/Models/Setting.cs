using System.Linq;
using System.Runtime.Serialization;
using System.Threading;
using Victoria.Data;

namespace Driverslog.Models {
    [DataContract]
    public class Setting : ActiveRecord<Setting> {

        public static void Kill() {
            _current = null;
        }

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
                if (_current == null) {
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
            SetDefaultDistanceUnit();
        }

        private static void SetDefaultDistanceUnit() {
            switch (Thread.CurrentThread.CurrentCulture.Name) {
                case "en-US":
                    _current.DistanceUnit = "miles";
                    break;
                case "en-GB":
                    _current.DistanceUnit = "miles";
                    break;
                default:
                    _current.DistanceUnit = "km";
                    break;
            }
        }

        [DataMember]
        public string Email { get; set; }

        [DataMember]
        public string DistanceUnit { get; set; }

        [DataMember]
        public string DefaultCar { get; set; }

    }
}