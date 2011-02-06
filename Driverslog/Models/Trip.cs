using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Globalization;
using System.Linq;
using System.Linq.Expressions;
using System.Runtime.Serialization;
using Caliburn.Micro;
using Victoria.Data;

namespace Driverslog.Models {
    [DataContract]
    public class Trip : ActiveRecord<Trip>, IHaveId, ICanHaveValidationErrors, INotifyPropertyChanged {

        [Obsolete]
        public static void AddFirst(Trip trip) {
            All.Insert(0, trip);
        }

        public Trip() {
            Id = Guid.NewGuid();
            Date = DateTime.Now.Date;
            EnsureValidationMessagesCollection();
        }

        [OnDeserializing]
        public void OnDeserializing(StreamingContext context) {
            EnsureValidationMessagesCollection();
        }

        [DataMember]
        public Guid Id { get; set; }

        private string _from;

        [DataMember]
        public string From {
            get { return _from; }
            set {
                _from = value;
                Notify(() => From);
            }
        }

        private string _to;

        [DataMember]
        public string To {
            get { return _to; }
            set {
                _to = value;
                Notify(() => To);
            }
        }

        private int _odometerStart;

        [DataMember]
        public int OdometerStart {
            get { return _odometerStart; }
            set {
                _odometerStart = value;
                Notify(() => OdometerStart);
                Notify(() => Distance);
            }
        }

        private int _odometerStop;

        [DataMember]
        public int OdometerStop {
            get { return _odometerStop; }
            set {
                _odometerStop = value;
                Notify(() => OdometerStop);
                Notify(() => Distance);
            }
        }

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
            return OdometerStop - OdometerStart > 0 &&
                    OdometerStart > 0 &&
                    OdometerStop > 0;
        }

        private void EnsureValidationMessagesCollection() {
            ValidationMessages = new Dictionary<string, string>();
        }

        public bool IsValid() {
            ValidationMessages.Clear();

            if (string.IsNullOrEmpty(From)) {
                ValidationMessages.Add("From", "You must specify where you're traveling from.");
            }
            return ValidationMessages.Count == 0;
        }

        [IgnoreDataMember]
        public Dictionary<string, string> ValidationMessages { get; private set; }

        private void Notify<TProperty>(Expression<Func<TProperty>> property) {
            var name = property.GetMemberInfo().Name;
            if (PropertyChanged != null) {
                PropertyChanged(this, new PropertyChangedEventArgs(name));
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;
    }
}