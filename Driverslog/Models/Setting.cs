using System;
using System.Linq;
using System.Runtime.Serialization;
using System.Threading;
using Victoria.Data;

namespace Driverslog.Models {
    [DataContract]
    public class Setting : ActiveRecord<Setting> {

        public static void Kill() {
            Clear();
            SaveChanges();
        }

        public static Setting Current {
            get {
                EnsureCurrent();
                return All.Single();
            }
        }

        private static void EnsureCurrent() {

            LoadIfEmpty();
            
            if (All.Count > 0) return;
            
            All.Add(new Setting() { DistanceUnit = GetDefaultDistanceUnit() });
            SaveChanges();
        }

        private static void LoadIfEmpty() {
            if (All == null || All.Count == 0) Load();
        }


        private static string GetDefaultDistanceUnit() {
            switch (Thread.CurrentThread.CurrentCulture.Name) {
                case "en-US":
                    return "miles";
                case "en-GB":
                    return "miles";
                default:
                    return "km";
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